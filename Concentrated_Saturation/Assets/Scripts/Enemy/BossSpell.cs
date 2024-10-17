using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpell : RecycleObject
{
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}
