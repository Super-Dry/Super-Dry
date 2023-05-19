using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleSystem[] battleArray;
    [SerializeField] private BattleTrigger battleTrigger;
    [SerializeField] private VillageTrigger villageTrigger;

    private BattleSystem currentBattle;
    private int NumOfBattle;
    private int currentBattleNumber;

    void Awake()
    {
        battleTrigger = GameObject.FindWithTag("BattleTrigger").GetComponent<BattleTrigger>();
        villageTrigger = GameObject.FindWithTag("VillageTrigger").GetComponent<VillageTrigger>();
        NumOfBattle = battleArray.Length - 1;
        currentBattleNumber = 0;
        currentBattle = battleArray[currentBattleNumber];
    }

    void Start()
    {
        battleTrigger.OnPlayerEnterTrigger += BattleTrigger_OnPlayerEnterTrigger;
    }

    private void BattleTrigger_OnPlayerEnterTrigger(object sender, EventArgs e)
    {
        battleTrigger.OnPlayerEnterTrigger -= BattleTrigger_OnPlayerEnterTrigger;

        StartBattle();
    }

    private void StartBattle()
    {
        Debug.Log("Start Battle!");
        currentBattle.onBattleOver += CurrentRound_OnBattleOver;
        currentBattle.StartBattle();
    }

    private void CurrentRound_OnBattleOver(object sender, EventArgs e)
    {
        if(GetNextBattle()){
            // Wait for player to return to village before next battlew
            villageTrigger.OnPlayerEnterTrigger += VillageTrigger_OnPlayerEnterTrigger;
        }else{
            Debug.Log("Game over!");
        }
    }

    private void VillageTrigger_OnPlayerEnterTrigger(object sender, EventArgs e)
    {
        Debug.Log("Player returned to village!");
        villageTrigger.OnPlayerEnterTrigger -= VillageTrigger_OnPlayerEnterTrigger;
        // Start next battle
        battleTrigger.OnPlayerEnterTrigger += BattleTrigger_OnPlayerEnterTrigger;
    }

    private bool GetNextBattle()
    {
        if (currentBattleNumber < NumOfBattle)
        {
            currentBattleNumber++;
            currentBattle = battleArray[currentBattleNumber];
            return true;
        }else{
            // All battles are finished
            return false;
        }
    }
}
