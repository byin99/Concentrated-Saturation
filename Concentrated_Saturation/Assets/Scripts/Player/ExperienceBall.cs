using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBall : RecycleObject
{
    public float moveSpeed = 7.0f;
    Player player;

    Vector3 distance;

    private Vector3 velocity;

    private void Awake()
    {
        player = GameManager.Instance.Player;
    }

    private void FixedUpdate()
    {
        distance = transform.position - player.transform.position;
        if(distance.magnitude < player.range)
        {
            Vector3 target = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, transform.position.z);
            target = target.normalized;
            transform.Translate(Time.fixedDeltaTime * moveSpeed * target);        
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Factory.Instance.GetExperienceEffect(transform.position);
            gameObject.SetActive(false);
        }
    }
}
