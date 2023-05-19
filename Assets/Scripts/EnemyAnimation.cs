using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private EnemyAction enemyAction;
    [SerializeField] private EnemyHealth enemyHealth;

    //Animator
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAction = GetComponent<EnemyAction>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Start()
    {
        enemyAction.isWalk += animatedEnemyAction_isWalk;
        enemyAction.isAttack += animatedEnemyAction_isAttack;
        enemyAction.isIdle += animatedEnemyAction_isIdle;
        enemyAction.isDead += animatedEnemyAction_isDead;
        enemyHealth.isHit += animatedEnemyAction_isHit;
    }

    private void animatedEnemyAction_isHit(object sender, EventArgs e)
    {
        anim.SetBool("isHit", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isAttack", false);
        anim.SetBool("isDead", false);
    }

    private void animatedEnemyAction_isDead(object sender, EventArgs e)
    {
        anim.SetBool("isDead", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isAttack", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isHit", false);
    }

    private void animatedEnemyAction_isIdle(object sender, EventArgs e)
    {
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isAttack", false);
        anim.SetBool("isHit", false);
        anim.SetBool("isDead", false);
    }

    private void animatedEnemyAction_isAttack(object sender, EventArgs e)
    {
        anim.SetBool("isAttack", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isHit", false);
        anim.SetBool("isDead", false);
    }

    private void animatedEnemyAction_isWalk(object sender, EventArgs e)
    {
        anim.SetBool("isWalk", true);
        anim.SetBool("isAttack", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isHit", false);
        anim.SetBool("isDead", false);
    }
}
