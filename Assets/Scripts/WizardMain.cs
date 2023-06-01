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


        wizardAction.enabled = false;
        skinnedMeshRenderer.enabled = false;
        navMeshAgent.enabled = false;
        enemyHealth.cantBeDamage = true;
        enemyHealth.healthbar.gameObject.SetActive(false);
    }

    

    public void Spawn()
    {
        gameObject.SetActive(true);
        StartCoroutine(spawnSequence());
    }

    IEnumerator spawnSequence()
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
        wizardAction.enabled = true;
        enemyHealth.healthbar.gameObject.SetActive(true);
        enemyHealth.cantBeDamage = false;

        yield return null;
    }
}
