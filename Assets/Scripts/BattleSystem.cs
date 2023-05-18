using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private enum State
    {
        Idle,
        Active,
        BossBattle,
        BattleOver,
    }

    [SerializeField] private BattleTrigger battleTrigger;
    [SerializeField] private Wave[] waveArray;

    public event EventHandler StartBossBattle;

    private State state;

    void Awake()
    {
        state = State.Idle;
        battleTrigger = GameObject.FindWithTag("BattleTrigger").GetComponent<BattleTrigger>();
    }

    void Start()
    {        
        battleTrigger.OnPlayerEnterTrigger += BattleTrigger_OnPlayerEnterTrigger;
    }

    private void BattleTrigger_OnPlayerEnterTrigger(object sender, EventArgs e)
    {
        if (state == State.Idle)
        {
            StartBattle();
            battleTrigger.OnPlayerEnterTrigger -= BattleTrigger_OnPlayerEnterTrigger;
        }
    }

    private void StartBattle()
    {
        Debug.Log("Start battle!");
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
                StartBossBattle?.Invoke(this, EventArgs.Empty);
            }
        }
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
                    SpawnEnemies();
                }
            }
        }

        private void SpawnEnemies(){
            foreach(EnemySpawn enemySpawn in enemySpawnArray)
            {
                enemySpawn.Spawn();
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
