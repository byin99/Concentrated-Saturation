using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : EnemyBase
{
    protected override void OnDie()
    {
        base.OnDie();
        Factory.Instance.GetBeeDie(transform.position);
    }
}
