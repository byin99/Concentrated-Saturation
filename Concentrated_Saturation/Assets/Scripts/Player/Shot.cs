using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : RecycleObject
{
    public float moveSpeed = 7.0f;

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * transform.right, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        Factory.Instance.GetHitEffect(transform.position);
    }
}
