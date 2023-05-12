using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AnimatedEnemyAction : MonoBehaviour
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
    public Vector3 temp;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    //public GameObject bullet;
    //public Transform shootPoint;
    //public float bulletSpeed;


    //Animator
    Animator anim;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fov = GetComponent<FieldOfView>();
        agent = GetComponentInChildren<NavMeshAgent>(); //maybe get rid of in children
        anim = GetComponent<Animator>();
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
        else{ 
            anim.SetBool("isWalk", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isDead", false);
            anim.SetBool("isHit", false);
            return;
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

            anim.SetBool("isWalk", true);
            anim.SetBool("isAttack", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isDead", false);
            anim.SetBool("isHit", false);

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
        anim.SetBool("isWalk", true);
        anim.SetBool("isAttack", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isDead", false);
        anim.SetBool("isHit", false);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        temp = new Vector3 (playerTargerPoint.position.x, transform.position.y, playerTargerPoint.position.z);
        transform.LookAt(temp);

        if (!alreadyAttacked)
        {
            // Attack code here
            //GameObject bulletObj = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            //Vector3 shootingDirection = playerTargerPoint.transform.position - shootPoint.position;
            //bulletObj.transform.forward = shootingDirection.normalized;
            //bulletObj.GetComponent<Rigidbody>().AddForce(shootingDirection.normalized * bulletSpeed, ForceMode.Impulse);
            //Destroy(bulletObj, 3f);
            print("attack!");
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        anim.SetBool("isWalk", false);
        anim.SetBool("isAttack", true);
        anim.SetBool("isIdle", false);
        anim.SetBool("isDead", false);
        anim.SetBool("isHit", false);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
