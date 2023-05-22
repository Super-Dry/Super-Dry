using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private enum State{
        atbattle,
        atVillage,
    }

    [SerializeField] private BattleSystem[] battleArray;
    [SerializeField] private BattleTrigger battleTrigger;
    [SerializeField] private VillageTrigger villageTrigger;
    [SerializeField] private Gate gate;

    private BattleSystem currentBattle;
    private int NumOfBattle;
    private int currentBattleNumber;
    private State state;

    void Awake()
    {
        battleTrigger = GameObject.FindWithTag("BattleTrigger").GetComponent<BattleTrigger>();
        villageTrigger = GameObject.FindWithTag("VillageTrigger").GetComponent<VillageTrigger>();
        gate = GameObject.Find("Gates").GetComponent<Gate>();
        NumOfBattle = battleArray.Length - 1;
        currentBattleNumber = 0;
        currentBattle = battleArray[currentBattleNumber];
        state = State.atVillage;
    }

    void Start()
    {
        villageTrigger.OnPlayerEnterTrigger += VillageTrigger_OnPlayerEnterTrigger;
    }

    private void BattleTrigger_OnPlayerEnterTrigger(object sender, EventArgs e)
    {
        battleTrigger.OnPlayerEnterTrigger -= BattleTrigger_OnPlayerEnterTrigger;
        gate.coroutineQueue.Enqueue(gate.Close());

        StartBattle();
    }

    private void StartBattle()
    {
        Debug.Log("Start Battle!");
        state = State.atbattle;
        currentBattle.onBattleOver += CurrentRound_OnBattleOver;
        currentBattle.StartBattle();
    }

    private void CurrentRound_OnBattleOver(object sender, EventArgs e)
    {
        gate.coroutineQueue.Enqueue(gate.Open());

        if(GetNextBattle()){
            // Wait for player to return to village before next battlew
            Debug.Log("Return to village first before starting next battle");
            villageTrigger.OnPlayerEnterTrigger += VillageTrigger_OnPlayerEnterTrigger;
        }else{
            Debug.Log("Game over!");
        }
    }

    private void VillageTrigger_OnPlayerEnterTrigger(object sender, EventArgs e)
    {
        if(state == State.atVillage){
            Debug.Log("Start battle?");
            // if (Input.GetMouseButtonDown(0))
            // {
                gate.coroutineQueue.Enqueue(gate.Open());
                villageTrigger.OnPlayerEnterTrigger -= VillageTrigger_OnPlayerEnterTrigger;
                battleTrigger.OnPlayerEnterTrigger += BattleTrigger_OnPlayerEnterTrigger;
            // }
        }else if (state == State.atbattle){
            Debug.Log("Player returned to village!");
            gate.coroutineQueue.Enqueue(gate.Close());
            villageTrigger.OnPlayerEnterTrigger += VillageTrigger_OnPlayerEnterTrigger;
            // Start next battle
            state = State.atVillage;
        }
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
