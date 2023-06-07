using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    CactusGuy playerShield;

    public int shieldBonus = 33;

    void Awake(){
        playerShield = FindObjectOfType<CactusGuy>();
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            if(playerShield.currentShield < 100){
            playerShield.HealShield(shieldBonus);
            Destroy(gameObject);
            }
        }
    }
}
