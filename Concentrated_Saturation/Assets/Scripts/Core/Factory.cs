using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    SlimePool slime;
    GoblinPool goblin;
    BeePool bee;
    WolfPool wolf;

    SlimeDiePool slimeDie;
    GoblinDiePool goblinDie;
    BeeDiePool beeDie;
    WolfDiePool wolfDie;

    HitEffectPool hitEffect;
    ShotPool shot;

    ExperienceBallPool experienceBall;
    ExperienceEffectPool experienceEffect;

    BossPool boss;
    BossSpellPool bossSpell;


    protected override void OnInitialize()
    {
        slime = GetComponentInChildren<SlimePool>();
        if (slime != null)
            slime.Initialize();

        goblin = GetComponentInChildren<GoblinPool>();
        if (goblin != null)
            goblin.Initialize();

        bee = GetComponentInChildren<BeePool>();
        if (bee != null)
            bee.Initialize();

        wolf = GetComponentInChildren<WolfPool>();
        if (wolf != null)
            wolf.Initialize();

        slimeDie = GetComponentInChildren<SlimeDiePool>();
        if (slimeDie != null)
            slimeDie.Initialize();

        goblinDie = GetComponentInChildren<GoblinDiePool>();
        if (goblinDie != null)
            goblinDie.Initialize();

        beeDie = GetComponentInChildren<BeeDiePool>();
        if (beeDie != null)
            beeDie.Initialize();

        wolfDie = GetComponentInChildren<WolfDiePool>();
        if (wolfDie != null) 
            wolfDie.Initialize();

        hitEffect = GetComponentInChildren<HitEffectPool>();
        if (hitEffect != null)
            hitEffect.Initialize();

        shot = GetComponentInChildren<ShotPool>();
        if (shot != null) 
            shot.Initialize();

        experienceBall = GetComponentInChildren<ExperienceBallPool>();
        if (experienceBall != null)
            experienceBall.Initialize();

        experienceEffect = GetComponentInChildren<ExperienceEffectPool>();
        if (experienceEffect != null)
            experienceEffect.Initialize();

        boss = GetComponentInChildren<BossPool>();
        if (boss != null)
            boss.Initialize();

        bossSpell = GetComponentInChildren<BossSpellPool>();
        if (bossSpell != null)
            bossSpell.Initialize();
    }

    // 풀에서 오브젝트 가져오는 함수들 ------------------------------------------------------------------


    public Slime GetSlime(Vector3? position, float angle = 0.0f)
    {
        return slime.GetObject(position, new Vector3(0, 0, angle));
    }

    public Goblin GetGoblin(Vector3? position, float angle = 0.0f)
    {
        return goblin.GetObject(position, new Vector3(0, 0, angle));
    }

    public Bee GetBee(Vector3? position, float angle = 0.0f)
    {
        return bee.GetObject(position, new Vector3(0, 0, angle));
    }

    public Wolf GetWolf(Vector3? position, float angle = 0.0f)
    {
        return wolf.GetObject(position, new Vector3(0, 0, angle));
    }

    public EnemyDie GetSlimeDie(Vector3? position, float angle = 0.0f)
    {
        return slimeDie.GetObject(position, new Vector3(0, 0, angle));
    }

    public EnemyDie GetGoblinDie(Vector3? position, float angle = 0.0f)
    {
        return goblinDie.GetObject(position, new Vector3(0, 0, angle));
    }

    public EnemyDie GetBeeDie(Vector3? position, float angle = 0.0f)
    {
        return beeDie.GetObject(position, new Vector3(0, 0, angle));
    }

    public EnemyDie GetWolfDie(Vector3? position, float angle = 0.0f)
    {
        return wolfDie.GetObject(position, new Vector3(0, 0, angle));
    }

    public EnemyDie GetHitEffect(Vector3? position, float angle = 0.0f)
    {
        return hitEffect.GetObject(position, new Vector3(0, 0, angle));
    }

    public Shot GetShot(Vector3? position, float angle = 0.0f)
    {
        return shot.GetObject(position, new Vector3(0, 0, angle));
    }

    public ExperienceBall GetExperienceBall(Vector3? position, float angle = 0.0f)
    {
        return experienceBall.GetObject(position, new Vector3(0, 0, angle));
    }

    public EnemyDie GetExperienceEffect(Vector3? position, float angle = 0.0f)
    {
        return experienceEffect.GetObject(position, new Vector3(0, 0, angle));
    }

    public Boss GetBoss(Vector3? position, float angle = 0.0f)
    {
        return boss.GetObject(position, new Vector3(0, 0, angle));
    }

    public BossSpell GetBossSpell(Vector3? position, float angle = 0.0f)
    {
        return bossSpell.GetObject(position, new Vector3(0, 0, angle));
    }
}
