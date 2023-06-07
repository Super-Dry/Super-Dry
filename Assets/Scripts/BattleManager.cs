using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private enum State{
        atBattle,
        atVillage,
    }

    [SerializeField] private BattleSystem[] battleArray;
    [SerializeField] private BattleTrigger battleTrigger;
    [SerializeField] private VillageTrigger villageTrigger;
    [SerializeField] private OnScreenText onScreenText;
    [SerializeField] private OnScreenText tittle;
    [SerializeField] private Gate gate;

    private BattleSystem currentBattle;
    private int NumOfBattle;
    private int currentBattleNumber;
    private State state;
    private Coroutine waitForEKey;

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
        state = State.atBattle;
        currentBattle.onBattleOver += CurrentRound_OnBattleOver;
        currentBattle.StartBattle();
        tittle.StartFadeOut();
    }

    private void CurrentRound_OnBattleOver(object sender, EventArgs e)
    {
        gate.coroutineQueue.Enqueue(gate.Open());

        if(GetNextBattle()){
            // Wait for player to return to village before next battlew
            onScreenText.setText("Return to the village before the next battle!");
            Debug.Log("Return to village first before starting next battle");
            villageTrigger.OnPlayerEnterTrigger += VillageTrigger_OnPlayerEnterTrigger;
        }else{
            Debug.Log("Game over!");
        }
    }

    private void VillageTrigger_OnPlayerEnterTrigger(object sender, EventArgs e)
    {
        villageTrigger.OnPlayerEnterTrigger -= VillageTrigger_OnPlayerEnterTrigger;
        if(state == State.atVillage){
            villageTrigger.OnPlayerExitTrigger += VillageTrigger_OnPlayerExitTrigger;
            onScreenText.setText("Press [E] to start the battle");
            waitForEKey = StartCoroutine(waitForKeyPress(KeyCode.E));
        }else if (state == State.atBattle){
            onScreenText.clearText();
            Debug.Log("Player returned to village!");
            gate.coroutineQueue.Enqueue(gate.Close());
            villageTrigger.OnPlayerEnterTrigger += VillageTrigger_OnPlayerEnterTrigger;
            // Start next battle
            state = State.atVillage;
        }
    }

    private void VillageTrigger_OnPlayerExitTrigger(object sender, EventArgs e)
    {
        StopCoroutine(waitForEKey);
        villageTrigger.OnPlayerExitTrigger -= VillageTrigger_OnPlayerExitTrigger;
        onScreenText.clearText();
        villageTrigger.OnPlayerEnterTrigger += VillageTrigger_OnPlayerEnterTrigger;
    }

    private IEnumerator waitForKeyPress(KeyCode key)
    {
        bool done = false;
        while(!done) // essentially a "while true", but with a bool to break out naturally
        {
            if(Input.GetKeyDown(key))
            {
                done = true; // breaks the loop
                onScreenText.clearText();
                tittle.Show();
                tittle.setText("Battle " + (currentBattleNumber + 1));
                gate.coroutineQueue.Enqueue(gate.Open());
                villageTrigger.OnPlayerExitTrigger -= VillageTrigger_OnPlayerExitTrigger;
                battleTrigger.OnPlayerEnterTrigger += BattleTrigger_OnPlayerEnterTrigger;
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }
        // now this function returns
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
