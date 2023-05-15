using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAction : MonoBehaviour
{
    public FieldOfView fov;
    public NavMeshAgent agent;
    public Transform player;
    public Transform playerTargerPoint;
    public LayerMask Ground;

    //Look Around
    public float lookAroundAngle;
    public float pauseTime;
    private float lastActionDuration;
    private bool looked = false;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    
    //Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject bullet;
    public Transform shootPoint;
    public float bulletSpeed;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fov = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
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
        else return;
    }


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        lastActionDuration = Time.time;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            walkPointSet = true;
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        lastActionDuration = Time.time;
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(playerTargerPoint);

        if (!alreadyAttacked)
        {
            // Attack code here
            GameObject bulletObj = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            Vector3 shootingDirection = playerTargerPoint.transform.position - shootPoint.position;
            bulletObj.transform.forward = shootingDirection.normalized;
            bulletObj.GetComponent<Rigidbody>().AddForce(shootingDirection.normalized * bulletSpeed, ForceMode.Impulse);
            Destroy(bulletObj, 3f);
            print("attack!");
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
