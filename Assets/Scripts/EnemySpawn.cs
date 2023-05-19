using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    private EnemyAction enemyAction;

    private void Awake() {
        gameObject.SetActive(false);
        enemyAction = GetComponent<EnemyAction>();
    }

    public void Spawn() {
        gameObject.SetActive(true);
        // transform.SetParent(null); // Go to root
    }

    public void KillEnemy()
    {
        enemyAction.enemyHealth.currentHealth = 0;
    }

    public bool IsAlive()
    {
        return !enemyAction.enemyHealth.IsDead();
    }
}
