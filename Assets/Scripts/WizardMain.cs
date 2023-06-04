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
    [SerializeField] private RockScript leftRock;
    [SerializeField] private RockScript rightRock;

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

    public void Stage4Start()
    {
        StartCoroutine(stage4StartPrologue());
    }

    public void Spawn()
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

    IEnumerator moveWizardDown()
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        Vector3 end = transform.position + new Vector3(0, -2.5f, 0);

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

        // Move up wizard
        StartCoroutine(moveWizardUp());

        // Light up both rock
        leftRock.rockSwitch();
        // rightRock.rockSwitch();

        // Wait for tornado warm up
        yield return new WaitForSeconds(6);

        leftRock.GetComponent<EnemyHealth>().cantBeDamage = false;    
        // rightRock.GetComponent<EnemyHealth>().cantBeDamage = false; 
        leftRock.GetComponent<EnemyHealth>().healthbar.gameObject.SetActive(true);   
        // rightRock.GetComponent<EnemyHealth>().healthbar.gameObject.SetActive(true);
        leftRock.rockDestoryed += RockDestoryed;
        // rightRock.rockDestoryed += RockDestoryed;

        bossBattle.StartNextStage(); // Start stage 2

        yield return null;
    }

    IEnumerator stage4StartPrologue()
    { 
        enemyHealth.cantBeDamage = true;
        enemyHealth.healthbar.gameObject.SetActive(false);
                
        // Start tornado base
        tornado.tornadoSwitch();

        // Move up wizard
        StartCoroutine(moveWizardUp());

        // Light up rock
        // leftRock.rockSwitch();
        rightRock.rockSwitch();

        // Wait for tornado warm up
        yield return new WaitForSeconds(6);

        // leftRock.GetComponent<EnemyHealth>().cantBeDamage = false;    
        rightRock.GetComponent<EnemyHealth>().cantBeDamage = false; 
        // leftRock.GetComponent<EnemyHealth>().healthbar.gameObject.SetActive(true);   
        rightRock.GetComponent<EnemyHealth>().healthbar.gameObject.SetActive(true);
        // leftRock.rockDestoryed += RockDestoryed;
        rightRock.rockDestoryed += RockDestoryed;

        bossBattle.StartNextStage(); // Start stage 4

        yield return null;
    }

    private void RockDestoryed(object sender, EventArgs e)
    {
        leftRock.rockDestoryed -= RockDestoryed;
        StartCoroutine(RockDestoryedNextStage());
    }

    IEnumerator RockDestoryedNextStage()
    { 
        // Stop tornado base
        tornado.tornadoSwitch();

        // Move down wizard
        StartCoroutine(moveWizardDown());
        
        // Wait for tornado stop
        yield return new WaitForSeconds(6);

        enemyHealth.cantBeDamage = false;
        enemyHealth.healthbar.gameObject.SetActive(true);
        bossBattle.StartNextStage();
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
        bossBattle.StartNextStage();    // Start stage 1

        yield return null;
    }
}
