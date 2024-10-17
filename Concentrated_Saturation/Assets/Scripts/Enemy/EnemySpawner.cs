using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public int monsterStage = 0;
    public bool bossDeath = false;

    public enum MonsterType
    {
        Slime = 0,
        Goblin,
        Bee,
        Wolf,
        Boss
    }

    [Serializable]

    public struct MonsterData 
    {
        public MonsterType type;
        public float spawnInterval;
    }

    public MonsterData[] monsterDatas;

    const float MinX = -35.0f;
    const float MaxX = 35.0f;
    const float MinY = -15.0f;
    const float MaxY = 15.0f;

    public int numberOfMonsters = 0;

    public int killOfMonsters = 0;

    StageManager stageManager;
    LoadPanel panel;
    CameraController cam;

    private void Awake()
    {
        stageManager = FindAnyObjectByType<StageManager>();
        panel = FindAnyObjectByType<LoadPanel>();
        cam = FindAnyObjectByType<CameraController>();
    }

    private void Start()
    {   
        if(ClickButton.level == 1) 
        {
            numberOfMonsters = 100;
        }
        else if(ClickButton.level == 2)
        {
            numberOfMonsters = 500;
        }
        else
        {
            numberOfMonsters = 1000;
        }
        StartCoroutine(SpawnCoroutine(monsterDatas[monsterStage]));
        stageManager.ShowStage(monsterStage);
    }

    private void Update()
    {
        if(killOfMonsters == numberOfMonsters)
        {
            StageClear();
            StartCoroutine(StageUI(monsterStage));
            StartCoroutine(SpawnCoroutine(monsterDatas[monsterStage]));
        }

        if (bossDeath)
        {
            StartCoroutine(GameClear());
        }
    }

    //IEnumerator StageCoroutine()
    //{
    //    for(monsterStage = 1; monsterStage < 5; monsterStage++)
    //    {
    //        var data = monsterDatas[monsterStage - 1];
    //        yield return StartCoroutine(SpawnCoroutine(data));
    //    }
    //}

    IEnumerator SpawnCoroutine(MonsterData data)
    {

        switch (data.type)
        {
            case MonsterType.Slime:

                for(int i = 0; i< numberOfMonsters; i++)
                {
                    yield return new WaitForSeconds(data.spawnInterval);
                    Vector3 spawnPosition = GetSpawnPosition();
                    Factory.Instance.GetSlime(spawnPosition);
                }
                break;

            case MonsterType.Goblin:
                for (int i = 0; i < numberOfMonsters; i++)
                {
                    yield return new WaitForSeconds(data.spawnInterval);
                    Vector3 spawnPosition = GetSpawnPosition();
                    Factory.Instance.GetGoblin(spawnPosition);
                }
                break;

            case MonsterType.Bee:
                for (int i = 0; i < numberOfMonsters; i++)
                {
                    yield return new WaitForSeconds(data.spawnInterval);
                    Vector3 spawnPosition = GetSpawnPosition();
                    Factory.Instance.GetBee(spawnPosition);
                }
                break;

            case MonsterType.Wolf:
                for (int i = 0; i < numberOfMonsters; i++)
                {
                    yield return new WaitForSeconds(data.spawnInterval);
                    Vector3 spawnPosition = GetSpawnPosition();
                    Factory.Instance.GetWolf(spawnPosition);
                }
                break;

            case MonsterType.Boss:
                Factory.Instance.GetBoss(new Vector3(30, 3, 0));
                StartCoroutine(cam.CameraMove());
                break;
        }
    }

    IEnumerator GameClear()
    {
        stageManager.GameClear();
        yield return new WaitForSeconds(3.0f);
        panel.TheEnd();
    }

    protected Vector3 GetSpawnPosition()
    {
        int i = Random.Range(0, 4);
        float randomX = Random.Range(MinX, MaxX);
        float randomY = Random.Range(MinY, MaxY);
        Vector3 spawnPosition = Vector3.zero;
        if (i < 1)
        {
            spawnPosition = transform.position + Vector3.right * MinX + Vector3.up * randomY;    
        }
        else if (i < 2)
        {
            spawnPosition = transform.position + Vector3.right * MaxX + Vector3.up * randomY;
        }
        else if (i < 3)
        {
            spawnPosition = transform.position + Vector3.right * randomX + Vector3.up * MinY;
        }
        else
        {
            spawnPosition = transform.position + Vector3.right * randomX + Vector3.up * MaxY;
        }
        return spawnPosition;
    }

    private void StageClear()
    {
        killOfMonsters = 0;
        monsterStage++;
    }

    IEnumerator StageUI(int index)
    {
        stageManager.Clear();
        yield return new WaitForSeconds(2.0f);
        stageManager.ClearOut();
        stageManager.ShowStage(index);
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 p0 = transform.position + Vector3.right * MaxX + Vector3.up * MaxY;
        Vector3 p1 = transform.position + Vector3.right * MaxX + Vector3.up * MinY;
        Vector3 p2 = transform.position + Vector3.right * MinX + Vector3.up * MinY;
        Vector3 p3 = transform.position + Vector3.right * MinX + Vector3.up * MaxY;

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }


}
