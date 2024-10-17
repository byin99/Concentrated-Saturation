using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyBase
{
    protected override void OnDie()
    {
        base.OnDie();
        Factory.Instance.GetGoblinDie(transform.position);
    }
}
