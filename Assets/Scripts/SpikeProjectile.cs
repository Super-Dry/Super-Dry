using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        EnemyAction enemy = collision.gameObject.GetComponent<EnemyAction>();
        AnimatedEnemyAction animatedEnemy = collision.gameObject.GetComponent<AnimatedEnemyAction>();
        if(enemy != null)
        {
            Destroy(gameObject);
            // print("Enemy got hit by player");
            enemy.TakeDamage(10);
        }else if(animatedEnemy != null)
        {
            Destroy(gameObject);
            // print("Enemy got hit by player");
            animatedEnemy.TakeDamage(10);
        }else if(collision.gameObject.tag == "Ground"){
            Destroy(gameObject);
        }
    }
}