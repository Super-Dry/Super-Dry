using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    float _interval = .1f;
    float _time = 0f;
    
    private void OnTriggerStay(Collider c){
        if(c.gameObject.name == "CactusGuy"){

            Debug.Log("please work for the love of god");

            var player = c.gameObject.GetComponent<CactusGuy>();
            _time += Time.deltaTime;
            Debug.Log(_time);
            if(_time >= _interval){
                Debug.Log("healing!");
                player.HealShield(1);
                _time -= _interval;
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
