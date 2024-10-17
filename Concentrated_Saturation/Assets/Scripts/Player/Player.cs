using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float attackInterval = 1.0f;

    public float moveSpeed = 3.0f;

    public int shotLevel = 1;

    public float invincibleDuration = 2.0f;

    public float range = 0.1f;

    public GameObject playerDie;

    PlayerInput playerInput;
    SpriteRenderer spriteRenderer;

    Vector3 inputDirection = Vector3.zero;

    readonly int InputX_String = Animator.StringToHash("InputX");
    readonly int InputY_String = Animator.StringToHash("InputY");
    readonly int PosX_String = Animator.StringToHash("PosX");
    readonly int PosY_String = Animator.StringToHash("PosY");    
    readonly int Dash_String = Animator.StringToHash("Dash");    
    readonly int AttackSpeed_String = Animator.StringToHash("AttackSpeed");    
    readonly int Attack_String = Animator.StringToHash("Attack");
    readonly int Die_String = Animator.StringToHash("Die");

    int invincibleLayer;
    int playerLayer;

    Animator animator;

    float attackSpeed = 0.0f;
    float attackTime = 1.0f;

    public float maxPlayerHP = 1.0f;

    float playerHP;

    float levelExperience = 0.05f;

    public float PlayerHP
    {
        get => playerHP;
        set
        {
            if(playerHP != value)
            {
                playerHP = value;
                if (playerHP > 0 && playerHP < maxPlayerHP)
                {
                    OnHit();
                }
            }

        }
    }

    int playerLevel = 1;

    public int PlayerLevel
    {
        get => playerLevel;
        set
        {
            playerLevel = value;
        }
    }

    float playerExperience = 0.0f;

    public float PlayerExperience
    {
        get => playerExperience;
        set
        {
            playerExperience = value;
        }
    }

    private new Camera camera;

    AudioSource audioSource1;
    AudioSource audioSource2;

    GameOver gameOver;

    private void Awake()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        gameOver = canvas.transform.GetChild(6).GetComponent<GameOver>();
        GameObject main = GameObject.FindGameObjectWithTag("MainCamera");
        audioSource1 = main.GetComponent<AudioSource>();
        audioSource2 = transform.GetChild(0).GetComponent<AudioSource>();
        playerInput = new PlayerInput();
        animator = GetComponent<Animator>();
        attackSpeed = attackTime / attackInterval;
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerExperience = 0.0f;
    }


    private void OnEnable()
    {
        playerInput.Move.Enable();
        playerInput.Move.WASDMove.performed += OnMove;
        playerInput.Move.WASDMove.canceled += OnMove;
        playerInput.Move.Dash.performed += OnDash;
        playerInput.Move.Dash.canceled += OnDash;
    }


    private void OnDisable()
    {
        playerInput.Move.Dash.canceled -= OnDash;
        playerInput.Move.Dash.performed -= OnDash;
        playerInput.Move.WASDMove.canceled -= OnMove;
        playerInput.Move.WASDMove.performed -= OnMove;
        playerInput.Move.Disable();
    }

    private void Start()
    {
        camera = Camera.main;
        PlayerHP = maxPlayerHP;
        StartCoroutine(AutoAttack());
        invincibleLayer = LayerMask.NameToLayer("Invincible");
        playerLayer = LayerMask.NameToLayer("Player");

    }

    private void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * moveSpeed * inputDirection);
        AttackSpeedUpdate();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDirection = (Vector3)input;
        animator.SetFloat(InputX_String, input.x);
        animator.SetFloat(InputY_String, input.y);
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetBool(Dash_String, true);
            moveSpeed *= 1.5f;
        }
        else
        {
            animator.SetBool(Dash_String, false);
            moveSpeed /= 1.5f;
        }
    }

    IEnumerator AutoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            Vector2 mousePos = (Vector2)camera.ScreenToWorldPoint((Vector2)Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
            animator.SetFloat(PosX_String, direction.x);
            animator.SetFloat(PosY_String, direction.y);
            animator.SetBool(Attack_String, true);
            float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
            Projection(angle);
            yield return new WaitForFixedUpdate();
            animator.SetBool(Attack_String, false);
        }
    }

    void AttackSpeedUpdate()
    {
        animator.SetFloat(AttackSpeed_String, attackSpeed);
    }

    void Projection(float angle)
    {
        Factory.Instance.GetShot(transform.position, angle);
        for (int i = 1; i < shotLevel; i++)
        {
            Factory.Instance.GetShot(transform.position, angle +i * 2);
            Factory.Instance.GetShot(transform.position, angle -i * 2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")) 
        {
            PlayerHP -= 0.1f;
            if (PlayerHP < 0)
            {
                audioSource1.Stop();
                Instantiate(playerDie, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }

        }
        else if (collision.gameObject.CompareTag("Item"))
        {
            PlayerExperience += levelExperience;
        }
    }

    public void LevelUp()
    {
        playerLevel++;
        playerExperience = 0.0f;
        attackInterval *= 0.9f;
        levelExperience *= 0.9f;
        range *= 1.5f;
        moveSpeed += 0.1f; 
        shotLevel++;
    }

    void OnHit()
    {
        audioSource2.Play();
        StartCoroutine(InvincibleMode());
    }

    IEnumerator InvincibleMode()
    {
        gameObject.layer = invincibleLayer;

        float timeElapsed = 0.0f;
        while (timeElapsed < invincibleDuration)
        {
            timeElapsed += Time.deltaTime;

            float alpha = (timeElapsed * 0.5f);
            spriteRenderer.color = new Color(1, alpha, alpha, 1);

            yield return null;
        }

        gameObject.layer = playerLayer;
    }


    //private void OnGUI()
    //{
    //    Vector2 mousePos = (Vector2)camera.ScreenToWorldPoint((Vector2)Input.mousePosition);
    //    Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
    //    GUI.color = Color.black;
    //    GUILayout.Label($"{mousePos}");
    //    GUILayout.Label($"{direction}");
    //}
}
