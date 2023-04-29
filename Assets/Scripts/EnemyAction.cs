using UnityEngine;
using UnityEngine.AI;

public class EnemyAction : MonoBehaviour
{
    public FieldOfView fov;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float pauseTime;
    private float lastActionDuration;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject bullet;

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
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            // GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            // Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
            // bulletRig.AddForce(transform.forward * 32f, ForceMode.Impulse);
            // bulletRig.AddForce(transform.up * 8f, ForceMode.Impulse);
            Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            Destroy(rb.gameObject, 1.5f);
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
