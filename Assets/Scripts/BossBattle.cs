using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossBattle : MonoBehaviour
{
    public enum Stage
    {
        WaitingToStart,
        Stage_1,
        Stage_2,
        Stage_3,
    }

    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private EnemySpawn pfEnemySpawn;
    [SerializeField] private GameObject boss;
    [SerializeField] private EnemyHealth enemyHealth;

    private List<Vector3> spawnPositionList;
    private Stage stage;
    
    void Awake()
    {
        battleSystem = transform.GetComponentInParent<BattleSystem>();
        enemyHealth = boss.GetComponent<EnemyHealth>();
        spawnPositionList = new List<Vector3>();
        
        foreach (Transform spawnPosition in transform.Find("SpawnPositionList"))
        {
            spawnPositionList.Add(spawnPosition.position);
        }
        stage = Stage.WaitingToStart;
    }

    void Start()
    {
        battleSystem.StartBossBattle += BattleSystem_StartBossBattle;
        enemyHealth.isHit += EnemyHealth_isHit;
    }

    private void EnemyHealth_isHit(object sender, EventArgs e)
    {
        Debug.Log(stage);
        // Boss took damage
        switch(stage){
            case Stage.Stage_1:
                if (enemyHealth.GetHealthNormalized() <= .7f){
                    stage = Stage.Stage_2;
                }
                break;
            case Stage.Stage_2:
                if (enemyHealth.GetHealthNormalized() <= .5f){
                    stage = Stage.Stage_3;
                }
                break;    
        }
    }

    private void BattleSystem_StartBossBattle(object sender, EventArgs e)
    {
        StartBattle();
        battleSystem.StartBossBattle -= BattleSystem_StartBossBattle;
    }

    private void StartBattle()
    {
        Debug.Log("Boss battle started!");
        stage = Stage.Stage_1;
        boss.GetComponent<EnemySpawn>().Spawn();
       
        InvokeRepeating("SpawnEnemy", 0.5f, 5f);
    }

    private void SpawnEnemy()
    {   
        Vector3 spawnPosition = spawnPositionList[UnityEngine.Random.Range(0, spawnPositionList.Count)];

        EnemySpawn enemySpawn = Instantiate(pfEnemySpawn, spawnPosition, Quaternion.identity) as EnemySpawn;
        enemySpawn.Spawn();
    }
}
