using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpikeProjectile : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if(enemyHealth != null)
        {
            Destroy(gameObject);
            // print("Enemy got hit by player");
            enemyHealth.TakeDamage(10);
        }else if(collision.gameObject.tag == "Ground"){
            Destroy(gameObject);
        }
    }
}
