using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private EnemySpawn pfEnemySpawn;

    private List<Vector3> spawnPositionList;
    
    void Awake()
    {
        battleSystem = transform.GetComponentInParent<BattleSystem>();
        spawnPositionList = new List<Vector3>();
        
        foreach (Transform spawnPosition in transform.Find("SpawnPositionList"))
        {
            spawnPositionList.Add(spawnPosition.position);
        }
    }

    void Start()
    {
        battleSystem.StartBossBattle += BattleSystem_StartBossBattle;
    }

    private void BattleSystem_StartBossBattle(object sender, EventArgs e)
    {
        StartBattle();
        battleSystem.StartBossBattle -= BattleSystem_StartBossBattle;
    }

    private void StartBattle()
    {
        Debug.Log("Boss battle started!");
        
        InvokeRepeating("SpawnEnemy", 1f, 2f);
    }

    private void SpawnEnemy()
    {   
        Vector3 spawnPosition = spawnPositionList[UnityEngine.Random.Range(0, spawnPositionList.Count)];

        EnemySpawn enemySpawn = Instantiate(pfEnemySpawn, spawnPosition, Quaternion.identity) as EnemySpawn;
        enemySpawn.Spawn();
    }
}
