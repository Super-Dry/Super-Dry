using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    CactusGuy playerHealth;

    public int healthBonus = 33;

    void Awake(){
        playerHealth = FindObjectOfType<CactusGuy>();
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            if(playerHealth.currentHealth < 100){
            playerHealth.HealHealth(healthBonus);
            Destroy(gameObject);
            }
        }
    }
}
