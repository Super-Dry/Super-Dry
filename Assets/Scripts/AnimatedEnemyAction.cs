using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AnimatedEnemyAction : MonoBehaviour
{
    public FieldOfView fov;
    public NavMeshAgent agent;
    public Transform playerTrans;
    public GameObject playerRef;
    CactusGuy cactusGuy;
    public Transform playerTargerPoint;
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

    //Health
    public int maxHealth = 100;
    public int currentHealth;
    bool isDead = false;

    public Healthbar healthbar;

    //Animator
    Animator anim;

    void Awake()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        cactusGuy = playerRef.GetComponent<CactusGuy>();
        fov = GetComponent<FieldOfView>();
        agent = GetComponentInChildren<NavMeshAgent>(); //maybe get rid of in children
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
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
        else
        {   // Enemy is dead
            anim.SetBool("isWalk", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isDead", true);
            anim.SetBool("isHit", false);
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
        agent.SetDestination(playerTrans.position);
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
            // Destroy(bulletObj, 5f);
            // print("attack!");
            cactusGuy.TakeDamage(damage);
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
           isDead = true; 
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
