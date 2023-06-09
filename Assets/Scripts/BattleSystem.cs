using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleSystem : MonoBehaviour
{
    private enum State
    {
        Idle,
        Active,
        BossBattle,
        BattleOver,
    }

    // [SerializeField] private BossBattle bossBattle;
    [SerializeField] private IBossBattle bossBattle;
    [SerializeField] private Wave[] waveArray;

    public event EventHandler onBattleOver;
    public event EventHandler StartBossBattle;

    private State state;

    void Awake()
    {
        state = State.Idle;
        bossBattle = GetComponentInChildren<IBossBattle>();
    }

    public void StartBattle()
    {
        state = State.Active;
    }

    private void Update()
    {
        switch (state) 
        {
            case State.Active:
                foreach (Wave wave in waveArray)
                {
                    wave.Update();
                }
                TestBattleOver();
                break;
        }
    }

    private void TestBattleOver()
    {
        if (state == State.Active)
        {
            if (CheckWavesOver())
            {
                // Start boss battle
                Debug.Log("Starting boss battle!");
                state = State.BossBattle;
                bossBattle.bossBattleOver += BossBattle_BossBattleOver;
                StartBossBattle?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void BossBattle_BossBattleOver(object sender, EventArgs e)
    {
        bossBattle.bossBattleOver -= BossBattle_BossBattleOver;
        Debug.Log("Battle over!");
        onBattleOver?.Invoke(this, EventArgs.Empty);
        state = State.BattleOver;
    }

    private bool CheckWavesOver()
    {
        foreach (Wave wave in waveArray)
        {
            if (wave.IsWaveOver())
            {
                // Wave is over
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    [System.Serializable]
    private class Wave
    {
        [SerializeField] private EnemySpawn[] enemySpawnArray;
        [SerializeField] private float timer;

        public void Update()
        {
            if (timer >= 0){
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Debug.Log("Wave spawned!");
                    SpawnEnemies();
                }
            }
        }

        private void SpawnEnemies(){
            foreach(EnemySpawn enemySpawn in enemySpawnArray)
            {
                NavMeshHit closestHit;
                if(NavMesh.SamplePosition(enemySpawn.transform.position, out closestHit, 3f, NavMesh.AllAreas ) ){
                    enemySpawn.gameObject.transform.position = closestHit.position;
                    enemySpawn.GetComponent<NavMeshAgent>().enabled = true;
                    enemySpawn.Spawn();
                }
            }
        }

        public bool IsWaveOver()
        {
            if (timer < 0)
            {
               // Wave spawned
               foreach (EnemySpawn enemySpawn in enemySpawnArray)
               {
                    if (enemySpawn.IsAlive())
                    {
                        return false;
                    }
               }
               return true;
            }else
            {
                // Enemies haven't spawned yet
                return false;
            }
        }
    }
}
