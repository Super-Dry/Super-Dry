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
    private List<EnemySpawn> enemySpawnList;
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
        enemyHealth.onDead += EnemyHealth_onDead;
    }

    private void BattleSystem_StartBossBattle(object sender, EventArgs e)
    {
        StartBattle();
        battleSystem.StartBossBattle -= BattleSystem_StartBossBattle;
    }

    private void StartBattle()
    {
        Debug.Log("Boss battle started!");
        StartNextStage();
        boss.GetComponent<EnemySpawn>().Spawn();
       
        InvokeRepeating("SpawnEnemy", 0.5f, 5f);
    }

    private void StartNextStage() {
        switch (stage) {
            case Stage.WaitingToStart:
                stage = Stage.Stage_1;
                break;
            case Stage.Stage_1:
                stage = Stage.Stage_2;
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                break;
        }
        Debug.Log("Starting next stage: " + stage);
    }

    private void EnemyHealth_isHit(object sender, EventArgs e)
    {
        // Boss took damage
        switch(stage){
            case Stage.Stage_1:
                if (enemyHealth.GetHealthNormalized() <= .7f){
                    StartNextStage();
                }
                break;
            case Stage.Stage_2:
                if (enemyHealth.GetHealthNormalized() <= .3f){
                    StartNextStage();
                }
                break;    
        }
    }
    
    private void EnemyHealth_onDead(object sender, EventArgs e)
    {
        // Boss dead! Boss battle is over!
        Debug.Log("Boss battle over!");
    }

    private void SpawnEnemy()
    {   
        Vector3 spawnPosition = spawnPositionList[UnityEngine.Random.Range(0, spawnPositionList.Count)];

        EnemySpawn enemySpawn = Instantiate(pfEnemySpawn, spawnPosition, Quaternion.identity) as EnemySpawn;
        enemySpawn.Spawn();
    }

    private void DestroyAllEnemies()
    {
        
    }
}
