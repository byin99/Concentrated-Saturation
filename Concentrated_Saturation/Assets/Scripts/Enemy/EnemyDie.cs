using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : RecycleObject
{
    Animator animator;
    protected float clipLength = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        AnimatorClipInfo info = animator.GetCurrentAnimatorClipInfo(0)[0];
        clipLength = info.clip.length;
    }

    protected override void OnEnable()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(clipLength);
        gameObject.SetActive(false);
    }

}
