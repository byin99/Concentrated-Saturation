using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : RecycleObject
{
    public float moveSpeed = 1.0f;
    private EnemySpawner spawner;

    public int maxHp = 0;

    private int hp = 1;

    public int HP
    {
        get => hp;
        private set
        {
            hp = value;
            if (hp == 0)
            {
                OnDie();
                moveSpeed = 0.0f;
            }
        }
    }

    Transform player;

    Vector3 direction = Vector3.zero;

    private void Start()
    {
        player = GameManager.Instance.Player.transform;

        spawner = FindAnyObjectByType<EnemySpawner>();
        HP = maxHp;
    }

    private void Update()
    {
        if (player == null)
        {
            return; 
        }
        direction = (player.position - transform.position).normalized;
        transform.Translate(Time.deltaTime * moveSpeed * direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Shot"))
            HP--;
    }

    protected virtual void OnDie()
    {
        gameObject.SetActive(false);
        Factory.Instance.GetExperienceBall(transform.position);
        spawner.killOfMonsters++;
    }
}
