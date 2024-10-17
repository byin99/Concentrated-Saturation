using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : EnemyBase
{
    protected override void OnDie()
    {
        base.OnDie();
        Factory.Instance.GetWolfDie(transform.position);
    }
}
