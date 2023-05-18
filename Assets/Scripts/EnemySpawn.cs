using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    private AnimatedEnemyAction animatedEnemyAction;

    private void Awake() {
        gameObject.SetActive(false);
        animatedEnemyAction = GetComponent<AnimatedEnemyAction>();
    }

    public void Spawn() {
        gameObject.SetActive(true);
        Debug.Log("spawn");
        // transform.SetParent(null); // Go to root
    }

    public bool IsAlive()
    {
        return !animatedEnemyAction.enemyHealth.isDead;
    }
}
