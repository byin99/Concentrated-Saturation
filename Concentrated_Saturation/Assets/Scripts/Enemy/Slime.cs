using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
{
    protected override void OnDie()
    {
        base.OnDie();
        Factory.Instance.GetSlimeDie(transform.position);
    }
}
