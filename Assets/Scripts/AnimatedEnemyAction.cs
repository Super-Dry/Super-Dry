using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class AnimatedEnemyAction : MonoBehaviour
{
    private FieldOfView fov;
    private NavMeshAgent agent;
    private Transform playerTrans;
    private GameObject playerRef;
    CactusGuy cactusGuy;
    private GameObject playerTargerPoint;
    private Transform playerTargerPointTransform;
    public LayerMask Ground;

    //Look Around
    public float lookAroundAngle;
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
    public int damage;
    //public GameObject bullet;
    //public Transform shootPoint;
    //public float bulletSpeed;

    public EnemyHealth enemyHealth;

    //Animation
    public event EventHandler isWalk;
    public event EventHandler isAttack;
    public event EventHandler isIdle;
    public event EventHandler isDead;

    void Awake()
    {
        fov = GetComponent<FieldOfView>();
        agent = GetComponentInChildren<NavMeshAgent>(); //maybe get rid of in children
        playerTrans = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerRef = GameObject.Find("CactusGuy");
        cactusGuy = playerRef.GetComponent<CactusGuy>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerTargerPoint = GameObject.Find("TargetPoint");
        playerTargerPointTransform = playerTargerPoint.GetComponent<Transform>();
    }

    void Start()
    {
        isIdle?.Invoke(this, EventArgs.Empty); 
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
                isIdle?.Invoke(this, EventArgs.Empty); 
                return;
            }
        }
        else
        {   // Enemy is dead
            isDead?.Invoke(this, EventArgs.Empty);
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

        isWalk?.Invoke(this, EventArgs.Empty);
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
        isWalk?.Invoke(this, EventArgs.Empty);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        temp = new Vector3 (playerTargerPointTransform.position.x, transform.position.y, playerTargerPointTransform.position.z);
        transform.LookAt(temp);

        if (!alreadyAttacked)
        {
            // Attack code here
            //GameObject bulletObj = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            //Vector3 shootingDirection = playerTargerPoint.transform.position - shootPoint.position;
            //bulletObj.transform.forward = shootingDirection.normalized;
            //bulletObj.GetComponent<Rigidbody>().AddForce(shootingDirection.normalized * bulletSpeed, ForceMode.Impulse);
            // Destroy(bulletObj, 5f);
            // print("attack!");
            cactusGuy.TakeDamage(damage);
            ///End of attack code

            isAttack?.Invoke(this, EventArgs.Empty);
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
