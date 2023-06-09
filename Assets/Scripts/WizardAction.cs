using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class WizardAction : MonoBehaviour
{

    private FieldOfView fov;
    // private NavMeshAgent agent;
    private Transform playerTrans;
    private GameObject playerRef;
    private CactusGuy cactusGuy;
    private GameObject playerTargerPoint;
    private Transform playerTargerPointTransform;
    public LayerMask Ground;

    //Look Around
    // public float pauseTime;
    private float lastActionDuration;

    //Patroling
    // public Vector3 walkPoint;
    // bool walkPointSet;
    // public float walkPointRange;
    
    //Attacking
    public Vector3 temp;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public IAttack enemyAttack;

    public EnemyHealth enemyHealth;
    public WizardBossBattle wizardBossBattle;

    //Animation
    public event EventHandler onAttackAnimation;
    public event EventHandler onIdleAnimation;
    public event EventHandler onDeadAnimation;

    void Awake()
    {
        fov = GetComponent<FieldOfView>();
        // agent = GetComponentInChildren<NavMeshAgent>();
        playerTrans = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerRef = GameObject.Find("CactusGuy");
        cactusGuy = playerRef.GetComponent<CactusGuy>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerTargerPoint = GameObject.Find("TargetPoint");
        playerTargerPointTransform = playerTargerPoint.GetComponent<Transform>();
        enemyAttack = gameObject.GetComponent<IAttack>();
    }

    void Start()
    {
        lastActionDuration = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!enemyHealth.IsDead()){
            if(wizardBossBattle.stage == WizardBossBattle.Stage.Spawning || wizardBossBattle.stage == WizardBossBattle.Stage.Transition){
                transform.LookAt(playerTargerPointTransform);
                onAttackAnimation?.Invoke(this, EventArgs.Empty);
            }else if(wizardBossBattle.stage == WizardBossBattle.Stage.Stage_1 || wizardBossBattle.stage == WizardBossBattle.Stage.Stage_3  || wizardBossBattle.stage == WizardBossBattle.Stage.Stage_5){
                AttackPlayer();
            }else if(wizardBossBattle.stage == WizardBossBattle.Stage.Stage_2 || wizardBossBattle.stage == WizardBossBattle.Stage.Stage_4){
                transform.LookAt(playerTargerPointTransform);
                onAttackAnimation?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            onDeadAnimation?.Invoke(this, EventArgs.Empty);
            Invoke(nameof(DestroyEnemy), 5f);
        }
    }

    private void AttackPlayer()
    {
        temp = new Vector3 (playerTargerPointTransform.position.x, transform.position.y, playerTargerPointTransform.position.z);
        transform.LookAt(temp);

        if (!alreadyAttacked)
        {           
            // Attack code here
            onAttackAnimation?.Invoke(this, EventArgs.Empty);
            enemyAttack.Attack();
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }else{
            onIdleAnimation?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
