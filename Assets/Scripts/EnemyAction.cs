using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class EnemyAction : MonoBehaviour
{
    private enum State {
        Patroling,
        ChasePlayer,
        AttackPlayer,
    }

    private FieldOfView fov;
    private NavMeshAgent agent;
    private Transform playerTrans;
    private GameObject playerRef;
    private CactusGuy cactusGuy;
    private GameObject playerTargerPoint;
    private Transform playerTargerPointTransform;
    public LayerMask Ground;

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
        onIdleAnimation?.Invoke(this, EventArgs.Empty); 
    }

    // Update is called once per frame
    void Update()
    {
        if(!enemyHealth.IsDead())
        {
            if(fov.canSeePlayer && !fov.canAttackPlayer)
            {
                ChasePlayer();
            }
            else if(fov.canSeePlayer && fov.canAttackPlayer)
            {
                AttackPlayer();
            }
            else if(!fov.canSeePlayer && Time.time > lastActionDuration + pauseTime)
            {
                Patroling();            
            }
            else{
                onIdleAnimation?.Invoke(this, EventArgs.Empty); 
                return;
            }
        }
        else
        {   // Enemy is dead
            onDeadAnimation?.Invoke(this, EventArgs.Empty);
            Invoke(nameof(DestroyEnemy), 1f);
        }
    }

    private void Patroling()
    {
        if (!walkPointSet){
            SearchWalkPoint();
        }

        if (walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        lastActionDuration = Time.time;

        onWalkAnimation?.Invoke(this, EventArgs.Empty);
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
        onWalkAnimation?.Invoke(this, EventArgs.Empty);
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

            onAttackAnimation?.Invoke(this, EventArgs.Empty);
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
