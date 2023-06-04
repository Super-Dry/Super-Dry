using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpikeProjectile : MonoBehaviour
{
    public ThirdPersonShooterController parent;
    
    void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if(enemyHealth != null)
        {
            Destroy(gameObject);
            // print("Enemy got hit by player");
            enemyHealth.TakeDamage(parent.damage);
        }else if(collision.gameObject.tag == "Obstacle"){
            Destroy(gameObject);
        }
    }
}
