using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : RecycleObject
{
    private EnemySpawner spawner;
    private EnemyUI UI;
    private CameraController cam;

    readonly int Death_Hash = Animator.StringToHash("Death");
    readonly int Attack_Hash = Animator.StringToHash("Attack");
    readonly int Spell_Hash = Animator.StringToHash("Spell");
    readonly int TeleportStart_Hash = Animator.StringToHash("TeleportStart");
    readonly int TeleportEnd_Hash = Animator.StringToHash("TeleportEnd");
    readonly int moveSpeed_Hash = Animator.StringToHash("moveSpeed");
    readonly int Hurt_Hash = Animator.StringToHash("Hurt");

    bool isAlive = true;
    bool isArrive = false;
    bool isAngry = false;

    Transform playerTransform;
    Animator animator;
    AudioSource mainAudioSource;

    float timeElapsed = 0.0f;
    float TeleportDistance;
    public float AttackDistance = 4.0f;
    public float moveSpeed;
    float speedSave;

    Vector2 areaMin = new Vector2(-28, -10);
    Vector2 areaMax = new Vector2(28, 10);

    Vector3 direction = Vector3.zero;
    Vector3 vectorDirection = Vector3.zero;
    Vector3 playerFront = Vector3.zero;

    int maxHP = 0;

    public int hp = 0;

    int HP
    {
        get => hp;

        set
        {
            hp = value;

            if (HP < (maxHP / 2) && !isAngry)
            {
                StartCoroutine(Hurt());
                mainAudioSource.pitch *= 2;
                isAngry = true;
            }

            else if (isAlive && hp < 1)
            {
                StopAllCoroutines();
                mainAudioSource.Stop();
                mainAudioSource.pitch /= 2;
                isAlive = false;
                UI.canvasGroup.alpha = 0;
                moveSpeed = 0;
                animator.SetFloat(moveSpeed_Hash, moveSpeed);
                StartCoroutine(Death());
            }
        }
    }

    public AudioClip[] audioClips;
    AudioSource audioSource1;
    AudioSource audioSource2;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        GameObject main = GameObject.FindGameObjectWithTag("MainCamera");
        mainAudioSource = main.GetComponent<AudioSource>();
        audioSource1 = GetComponent<AudioSource>();
        audioSource2 = transform.GetChild(0).GetComponent<AudioSource>();
        spawner = FindAnyObjectByType<EnemySpawner>();
        animator = GetComponent<Animator>();
        playerTransform = GameManager.Instance.Player.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UI = FindAnyObjectByType<EnemyUI>();
        cam = FindAnyObjectByType<CameraController>();

        if (ClickButton.level == 1)
        {
            maxHP = 10000;
            moveSpeed = 5;
            speedSave = 5;
        }
        else if (ClickButton.level == 2)
        {
            maxHP = 50000;
            moveSpeed = 8;
            speedSave = 8;
        }
        else
        {
            maxHP = 100000;
            moveSpeed = 10;
            speedSave = 10;
        }
        HP = maxHP;
    }

    private void Start()
    {
        StartCoroutine(UIcanvas());
        direction = Vector3.left;
        vectorDirection = new Vector3(20, 3, 0);
        animator.SetFloat(moveSpeed_Hash, moveSpeed);
        StartCoroutine(OnMove());

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shot"))
        {
            HP--;
            UI.bossSlider.value = (float)HP / maxHP;
        }
    }

    IEnumerator UIcanvas()
    {
        while (timeElapsed < 1.0f)
        {
            timeElapsed += Time.deltaTime;
            UI.canvasGroup.alpha = timeElapsed;
            yield return null;
        }
    }

    
    IEnumerator OnMove()
    {
        while (true)
        {
            if(isArrive)
            {
                float posX = Random.Range(areaMin.x, areaMax.x);
                float posY = Random.Range(areaMin.y, areaMax.y);
                vectorDirection = new Vector3(posX, posY, 0);
                direction = (vectorDirection - transform.position).normalized;
                moveSpeed = speedSave;
                animator.SetFloat(moveSpeed_Hash, moveSpeed);
                isArrive = false;
            }

            else if (!isArrive)
            {
                //Debug.Log(moveSpeed);
                transform.Translate(Time.deltaTime * moveSpeed * direction);
                yield return null;
                if (Vector3.Distance(transform.position, vectorDirection) < 0.1f)
                {
                    moveSpeed = 0;
                    animator.SetFloat(moveSpeed_Hash, moveSpeed);
                    if (isAngry)
                    {
                        yield return StartCoroutine(Spell());
                    }
                    playerFront = new Vector3(playerTransform.position.x + 3, playerTransform.position.y + 1, 0);
                    yield return StartCoroutine(Teleport());
                    yield return StartCoroutine(Attack());
                }
            }
            yield return null;
        }
    }

    IEnumerator Teleport()
    {
        audioSource2.clip = audioClips[2];
        teleportStartFinished = false;
        teleportEndFinished = false;
        animator.SetBool(TeleportStart_Hash, true);
        yield return new WaitUntil(() => teleportStartFinished);
        transform.position = playerFront;
        animator.SetBool(TeleportEnd_Hash, true);
        yield return new WaitUntil(() => teleportEndFinished);
        animator.SetBool(TeleportEnd_Hash, false);
        animator.SetBool(TeleportStart_Hash, false);
    }

    IEnumerator Spell()
    {
        audioSource2.clip = audioClips[1];
        animator.SetBool(Spell_Hash, true);
        yield return new WaitUntil(() => spellFinished);
        animator.SetBool(Spell_Hash, false);
        Factory.Instance.GetBossSpell(playerTransform.position);
        spellFinished = false;
        moveSpeed = speedSave;
    }

    IEnumerator Attack()
    {
        audioSource2.clip = audioClips[0];
        attackFinished = false;
        animator.SetBool(Attack_Hash, true);
        yield return new WaitUntil(() => attackFinished);
        animator.SetBool(Attack_Hash, false);
        isArrive = true;
    }

    IEnumerator Death()
    {
        audioSource2.clip = audioClips[4];
        yield return StartCoroutine(cam.BossDie(transform.position));
        animator.SetTrigger(Death_Hash);
        yield return new WaitForSeconds(2);
        spawner.bossDeath = true;
        gameObject.SetActive(false);
    }

    IEnumerator Hurt()
    {
        audioSource2.clip = audioClips[3];
        moveSpeed = 0;
        animator.SetFloat(moveSpeed_Hash, moveSpeed);
        animator.SetBool(Hurt_Hash, true);
        speedSave *= 2;
        float timeElapsed = 0.0f;
        while (timeElapsed < 2.0f)
        {
            timeElapsed += Time.deltaTime;

            float alpha = (timeElapsed * 0.5f);
            spriteRenderer.color = new Color(1, 1 - alpha, 1 - alpha, 1);

            yield return null;
        }
        yield return new WaitUntil(() => hurtFinished);
        animator.SetBool(Hurt_Hash, false);
        moveSpeed = speedSave;
        animator.SetFloat(moveSpeed_Hash, moveSpeed);
    }

    private bool teleportStartFinished = false;
    private bool teleportEndFinished = false;
    private bool attackFinished = false;
    private bool spellFinished = false;
    private bool hurtFinished = false;

    public void OnTeleportStartEnd()
    {
        teleportStartFinished = true;
    }

    public void OnTeleportEndEnd()
    {
        teleportEndFinished = true;
    }

    public void OnAttackEnd()
    {
        attackFinished = true;
    }

    public void OnSpellEnd()
    {
        spellFinished = true;
    }

    public void OnHurtEnd()
    {
        hurtFinished = true;
    }
}

