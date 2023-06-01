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
    public BossBattle bossBattle;

    //Animation
    public event EventHandler onAttackAnimation;
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
        if(bossBattle.stage == BossBattle.Stage.Stage_1){
            AttackPlayer();
            onAttackAnimation?.Invoke(this, EventArgs.Empty);
        }else if(bossBattle.stage == BossBattle.Stage.Stage_2){
            onAttackAnimation?.Invoke(this, EventArgs.Empty);
            // switch (state){
            //     case State.Idle:
            //         onIdleAnimation?.Invoke(this, EventArgs.Empty);
            //         break;
            //     case State.Attacking:
            //         AttackPlayer();
            //         onAttackAnimation?.Invoke(this, EventArgs.Empty);
            //         break;
            //     case State.Dead:
            //         // agent.SetDestination(transform.position);
            //         onDeadAnimation?.Invoke(this, EventArgs.Empty);
            //         Invoke(nameof(DestroyEnemy), 5f);
            //         break;
            //     default:
            //         state = State.Idle;
            //         break;    
            // }
        }else if(bossBattle.stage == BossBattle.Stage.Stage_3){
            AttackPlayer();
            onAttackAnimation?.Invoke(this, EventArgs.Empty);
        }
        if(enemyHealth.IsDead())
        {
            onDeadAnimation?.Invoke(this, EventArgs.Empty);
            Invoke(nameof(DestroyEnemy), 5f);
        }
    }

    private void AttackPlayer()
    {
        // agent.SetDestination(transform.position);
        temp = new Vector3 (playerTargerPointTransform.position.x, transform.position.y, playerTargerPointTransform.position.z);
        transform.LookAt(temp);

        if (!alreadyAttacked)
        {
            // Attack code here
            enemyAttack.Attack();
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
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
