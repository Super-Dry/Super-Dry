using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardMain : MonoBehaviour
{
    [SerializeField] private TornadoScript tornado;
    [SerializeField] private Animator animator;
    [SerializeField] private WizardAction wizardAction;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private BossBattle bossBattle;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(false);
        tornado = GameObject.Find("WizardTornadoBase").GetComponent<TornadoScript>();
        animator = GetComponent<Animator>();
        wizardAction = GetComponent<WizardAction>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();

        skinnedMeshRenderer.enabled = false;
        navMeshAgent.enabled = false;
        enemyHealth.cantBeDamage = true;
        enemyHealth.healthbar.gameObject.SetActive(false);
    }

    public void Stage2Start()
    {
        StartCoroutine(stage2StartPrologue());
    }

    public void Stage1Start()
    {
        gameObject.SetActive(true);
        StartCoroutine(stage1StartPrologue());
    }

    IEnumerator moveWizardUp()
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        Vector3 end = transform.position + new Vector3(0, 2.5f, 0);

        while (elapsedTime < 5)
        {
            transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / 5));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    IEnumerator stage2StartPrologue()
    { 
        enemyHealth.cantBeDamage = true;
        enemyHealth.healthbar.gameObject.SetActive(false);
                
        // Start tornado base
        tornado.tornadoSwitch();
        StartCoroutine(moveWizardUp());    

        // Wait for tornado warm up
        yield return new WaitForSeconds(6);

        bossBattle.StartNextStage();

        yield return null;
    }


    IEnumerator stage1StartPrologue()
    {
        // Start tornado base
        tornado.tornadoSwitch();

        // Wait for tornado warm up
        yield return new WaitForSeconds(6);

        skinnedMeshRenderer.enabled = true;
        animator.SetBool("isAttack", true);
        animator.SetBool("isWalk", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isHit", false);
        animator.SetBool("isDead", false);
        
        // Stop tornado base
        tornado.tornadoSwitch();

        // Wait for tornado clear up
        yield return new WaitForSeconds(4);

        // Start wizard action
        enemyHealth.healthbar.gameObject.SetActive(true);
        enemyHealth.cantBeDamage = false;
        bossBattle.StartNextStage();

        yield return null;
    }
}
