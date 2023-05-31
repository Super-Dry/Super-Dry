using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WizardAnimation : MonoBehaviour
{
    [SerializeField] private WizardAction wizardAction;
    [SerializeField] private EnemyHealth enemyHealth;

    //Animator
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        wizardAction = GetComponent<WizardAction>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Start()
    {
        wizardAction.onWalkAnimation += animatedEnemyAction_onWalkAnimation;
        wizardAction.onAttackAnimation += animatedEnemyAction_onAttackAnimation;
        wizardAction.onIdleAnimation += animatedEnemyAction_onIdleAnimation;
        wizardAction.onDeadAnimation += animatedEnemyAction_onDeadAnimation;
        enemyHealth.onHitAnimation += animatedEnemyAction_onHitAnimation;
    }

    private void animatedEnemyAction_onHitAnimation(object sender, EventArgs e)
    {
        anim.SetBool("isHit", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isAttack", false);
        anim.SetBool("isDead", false);
    }

    private void animatedEnemyAction_onDeadAnimation(object sender, EventArgs e)
    {
        anim.SetBool("isDead", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isAttack", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isHit", false);
    }

    private void animatedEnemyAction_onIdleAnimation(object sender, EventArgs e)
    {
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isAttack", false);
        anim.SetBool("isHit", false);
        anim.SetBool("isDead", false);
    }

    private void animatedEnemyAction_onAttackAnimation(object sender, EventArgs e)
    {
        anim.SetBool("isAttack", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isHit", false);
        anim.SetBool("isDead", false);
    }

    private void animatedEnemyAction_onWalkAnimation(object sender, EventArgs e)
    {
        anim.SetBool("isWalk", true);
        anim.SetBool("isAttack", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isHit", false);
        anim.SetBool("isDead", false);
    }
}
