using UnityEngine;
using UnityEngine.AI;

public class EnemyAction : MonoBehaviour
{
    public FieldOfView fov;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fov = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!fov.canSeePlayer) Patroling();
        else ChasePlayer();
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
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
}
