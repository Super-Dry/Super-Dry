using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    float _interval = 30f;
    
    private void OnTriggerEnter(Collider c){
        if(c.gameObject.name == "CactusGuy"){
            Debug.Log("please work for the love of god");
            c.gameObject.GetComponent<CactusGuy>().HealShield(5);
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

    // void HealPlayer(Collision c){
    //     c.gameObject.GetComponent<CactusGuy>().HealShield(5);
    // }
}
