using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    float _interval = .1f;
    float _time = 0f;

    public int totalShieldPointsRemaining = 100;

    //Heal player gradually while they are in water
    //Adjust the interval float to change rate of healing
    //(interval until next heal of 1 point occurs)
    private void OnTriggerStay(Collider c){
        if(c.gameObject.name == "CactusGuy"){

           // Debug.Log("please work for the love of god");

            var player = c.gameObject.GetComponent<CactusGuy>();
            _time += Time.deltaTime;

            if(_time >= _interval){
                if(totalShieldPointsRemaining > 0 && player.currentShield < 100){
                    int shieldPointsToHeal = 1;
                    Debug.Log("healing!");
                    Debug.Log("Remaining water in pool: " + totalShieldPointsRemaining);
                    
                    player.HealShield(shieldPointsToHeal);
                    totalShieldPointsRemaining -= shieldPointsToHeal;
                    _time -= _interval;
                }
            }
        }

        // float _time = 0;
        // while(c.gameObject.name == "CactusGuy"){
        //     Debug.Log("please work for the love of god");

        //     _time += Time.deltaTime;
        //     while(_time >= _interval){
        //         HealPlayer(c);
        //         _time -= _interval;
        //     }
        // }
    }

    // void HealPlayer(Collider c){
    //     c.gameObject.GetComponent<CactusGuy>().HealShield(5);
    // }
}
