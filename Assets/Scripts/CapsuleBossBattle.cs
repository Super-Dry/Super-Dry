using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class CapsuleBossBattle : MonoBehaviour, IBossBattle
{
    public enum Stage
    {
        WaitingToStart,
        Stage_1,
        Stage_2,
        Stage_3,
    }

    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private EnemySpawn pfEnemyCapsuleSpawn;
    [SerializeField] private EnemySpawn pfEnemyCactusSpawn;
    [SerializeField] private GameObject boss;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private float enemySpawnRate;
    [SerializeField] private float maxEnemyAlive;

    private List<Vector3> spawnPositionList;
    private List<EnemySpawn> enemySpawnList;
    private Stage stage;

    public event EventHandler bossBattleOver;
    
    void Awake()
    {
        battleSystem = transform.GetComponentInParent<BattleSystem>();
        enemyHealth = boss.GetComponent<EnemyHealth>();
        spawnPositionList = new List<Vector3>();
        enemySpawnList = new List<EnemySpawn>();
        
        foreach (Transform spawnPosition in transform.Find("SpawnPositionList"))
        {
            spawnPositionList.Add(spawnPosition.position);
        }
        stage = Stage.WaitingToStart;
    }

    void Start()
    {
        battleSystem.StartBossBattle += BattleSystem_StartBossBattle;
        enemyHealth.onHitAnimation += EnemyHealth_onHitAnimation;
        enemyHealth.onDead += EnemyHealth_onDead;
    }

    private void BattleSystem_StartBossBattle(object sender, EventArgs e)
    {
        battleSystem.StartBossBattle -= BattleSystem_StartBossBattle;
        StartBattle();
    }

    private void StartBattle()
    {
        Debug.Log("Boss battle started!");
        StartNextStage();
        boss.GetComponent<EnemySpawn>().Spawn();
       
        InvokeRepeating("SpawnEnemy", 0.5f, enemySpawnRate);
    }

    private void StartNextStage() {
        switch (stage) {
            case Stage.WaitingToStart:
                stage = Stage.Stage_1;
                break;
            case Stage.Stage_1:
                stage = Stage.Stage_2;
                CancelInvoke();
                maxEnemyAlive += 2;
                enemySpawnRate -= 2;
                InvokeRepeating("SpawnEnemy", 5f, enemySpawnRate);
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                maxEnemyAlive += 2;
                enemySpawnRate -= 2;
                InvokeRepeating("SpawnEnemy", 5f, enemySpawnRate);
                break;
        }
        Debug.Log("Starting next stage: " + stage);
    }

    private void EnemyHealth_onHitAnimation(object sender, EventArgs e)
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
        enemyHealth.onHitAnimation -= EnemyHealth_onHitAnimation;
        enemyHealth.onDead -= EnemyHealth_onDead;
        Debug.Log("Boss battle over!");
        CancelInvoke();
        DestroyAllEnemies();
        bossBattleOver?.Invoke(this, EventArgs.Empty);
    }

    private void SpawnEnemy()
    {   
        int aliveCount = 0;
        foreach (EnemySpawn enemySpawned in enemySpawnList) {
            if (enemySpawned.IsAlive()) {
                aliveCount++;
                if (aliveCount >= maxEnemyAlive){
                    // Don't spawn more enemies
                    return;
                }
            }
        }

        Vector3 spawnPosition = spawnPositionList[UnityEngine.Random.Range(0, spawnPositionList.Count)];

        EnemySpawn pfEnemySpawn;
        int rand = UnityEngine.Random.Range(0, 100);
        pfEnemySpawn = pfEnemyCactusSpawn;                      // By default spawn enemy cactus
        if (rand < 45) pfEnemySpawn = pfEnemyCapsuleSpawn;      // 45% chances of spawning enemy capsule

        NavMeshHit closestHit;
        if(NavMesh.SamplePosition(spawnPosition, out closestHit, 3f, NavMesh.AllAreas ) ){
            EnemySpawn enemySpawn = Instantiate(pfEnemySpawn, closestHit.position, Quaternion.identity) as EnemySpawn;
            enemySpawn.GetComponent<NavMeshAgent>().enabled = true;
            enemySpawn.Spawn();
            enemySpawnList.Add(enemySpawn);
        }
    }

    private void DestroyAllEnemies()
    {
        foreach (EnemySpawn enemySpawn in enemySpawnList)
        {
            if (enemySpawn.IsAlive())
            {
                enemySpawn.KillEnemy();
            }
        }
    }
}