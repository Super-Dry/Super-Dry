using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class WizardAction : MonoBehaviour
{
    private enum State{
        Idle,
        Patroling,
        Walking,
        Chasing,
        Attacking,
        Dead,
    }


    private FieldOfView fov;
    private NavMeshAgent agent;
    private Transform playerTrans;
    private GameObject playerRef;
    private CactusGuy cactusGuy;
    private GameObject playerTargerPoint;
    private Transform playerTargerPointTransform;
    public LayerMask Ground;
    private State state;

    //Look Around
    public float pauseTime;
    private float lastActionDuration;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    
    //Attacking
    public Vector3 temp;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public IAttack enemyAttack;

    public EnemyHealth enemyHealth;
    public BossBattle bossBattle;

    //Animation
    public event EventHandler onWalkAnimation;
    public event EventHandler onAttackAnimation;
    public event EventHandler onIdleAnimation;
    public event EventHandler onDeadAnimation;

    void Awake()
    {
        fov = GetComponent<FieldOfView>();
        agent = GetComponentInChildren<NavMeshAgent>();
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
        state = State.Idle;
        lastActionDuration = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossBattle.stage == BossBattle.Stage.Stage_1){
            Stage1Attack();
            onAttackAnimation?.Invoke(this, EventArgs.Empty);
        }else if(bossBattle.stage == BossBattle.Stage.Stage_2){
            switch (state){
                case State.Idle:
                    onIdleAnimation?.Invoke(this, EventArgs.Empty);
                    break;
                case State.Patroling:
                    Patroling();
                    break;
                case State.Walking:
                    Walking();
                    onWalkAnimation?.Invoke(this, EventArgs.Empty);
                    break;
                case State.Chasing:
                    ChasePlayer();
                    onWalkAnimation?.Invoke(this, EventArgs.Empty);
                    break;
                case State.Attacking:
                    AttackPlayer();
                    onAttackAnimation?.Invoke(this, EventArgs.Empty);
                    break;
                case State.Dead:
                    agent.SetDestination(transform.position);
                    onDeadAnimation?.Invoke(this, EventArgs.Empty);
                    Invoke(nameof(DestroyEnemy), 5f);
                    break;
                default:
                    state = State.Idle;
                    break;    
            }
        }
        if(!enemyHealth.IsDead())
        {
            if(fov.canSeePlayer && !fov.canAttackPlayer)
            {
                state = State.Chasing;
            }
            else if(fov.canSeePlayer && fov.canAttackPlayer)
            {
                state = State.Attacking;
            }
            else if(!fov.canSeePlayer && Time.time > lastActionDuration + pauseTime)
            {
                state = State.Patroling;
            }
            else if(state == State.Walking){
                return;
            }
            else{
                state = State.Idle;
                return;
            }
        }
        else
        {   // Enemy is dead
            state = State.Dead;
        }
        // Debug.Log(state);
    }


    private void Patroling()
    {
        if (walkPointSet){
            Walking();            
        }else{
            SearchWalkPoint();
        }
    }

    private void Walking()
    {
        state = State.Walking;
        // Debug.Log("walking to point");
        agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f){
            // Debug.Log("walk point reached");
            state = State.Idle;
            walkPointSet = false;
        }

        lastActionDuration = Time.time;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            walkPointSet = true;
    }


    private void ChasePlayer()
    {
        agent.SetDestination(playerTrans.position);
        lastActionDuration = Time.time;
    }
    
    private void Stage1Attack()
    {
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

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
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
