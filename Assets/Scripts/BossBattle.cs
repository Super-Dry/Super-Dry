using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class BossBattle : MonoBehaviour
{
    public enum Stage
    {
        WaitingToStart,
        Spawning,
        Transition,
        Stage_1, // normal attack
        Transition1TO2, // 1 to 2 
        Stage_2, // tornado protect
        Transition2TO3, // 2 to 3
        Stage_3, // left rock destoryed and start normal attack
        Transition3TO4, // 3 to 4
        Stage_4, // tornado protect
        Transition4TO5, // 4 to 5
        Stage_5, // right rock destoryed and start normal attack       
    }

    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private EnemySpawn pfEnemyCapsuleSpawn;
    [SerializeField] private EnemySpawn pfEnemyCactusSpawn;
    [SerializeField] private GameObject boss;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private WizardMain wizardMain;
    [SerializeField] private float enemySpawnRate;
    [SerializeField] private float maxEnemyAlive;


    private List<Vector3> spawnPositionList;
    private List<EnemySpawn> enemySpawnList;
    public Stage stage;
    public Stage lastStage;

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
       
        InvokeRepeating("SpawnEnemy", 5f, enemySpawnRate);
    }

    public void StartNextStage() {
        switch (stage) {
            case Stage.WaitingToStart:
                stage = Stage.Spawning;
                wizardMain.Spawn();
                break;
            case Stage.Spawning:            // call next from wizardMain
                stage = Stage.Stage_1;
                break;
            case Stage.Transition:
                if(lastStage == Stage.Stage_1){
                    stage = Stage.Stage_2;
                }else if(lastStage == Stage.Stage_3){
                    stage = Stage.Stage_4;
                }else if(lastStage == Stage.Stage_4){
                    stage = Stage.Stage_5;
                }
                break;
            case Stage.Stage_1:             // call from bossBattle
                lastStage = stage;
                stage = Stage.Transition;
                wizardMain.Stage2Start();
                break;  
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                break;
            case Stage.Stage_3:             // call from bossBattle
                lastStage = stage;
                stage = Stage.Transition;
                wizardMain.Stage4Start();
                break;
            case Stage.Stage_4:             
                stage = Stage.Stage_5;
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
                    StartNextStage();       // Start Transitioning 1 to 2
                }
                break;
            case Stage.Stage_3:
                if (enemyHealth.GetHealthNormalized() <= .4f){
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
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("NeedToDestroy")){
            Destroy(o);
        }
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

        // EnemySpawn enemySpawn = Instantiate(pfEnemySpawn, spawnPosition, Quaternion.identity) as EnemySpawn;
        // enemySpawn.Spawn();
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
