using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform playerTransform;
    public LayerMask groundMask, playerMask;

    // Patrolling
    Vector3 patrolPoint;
    public float patrolPointRange;
    bool patrolPointSet;

    // Stalking
    public float sightRange;
    bool playerInRange;

    // Called to initialize variables before game start.
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = GameObject.Find("First Person Controller").transform;
        playerInRange = Physics.CheckSphere(transform.position, sightRange, playerMask);

        if (playerInRange)
        {
            Debug.Log("Chasing Player");
            ChasePlayer();
        }
        else
            Patrol();
    }

    void FindPatrolPoint()
    {
        float randZ = Random.Range(-patrolPointRange, patrolPointRange);
        float randX = Random.Range(-patrolPointRange, patrolPointRange);

        patrolPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);

        if (Physics.Raycast(patrolPoint, -transform.up, 2f, groundMask))
            patrolPointSet = true;
    }

    void Patrol()
    {
        if (!patrolPointSet)
            FindPatrolPoint();

        if (patrolPointSet)
            agent.SetDestination(patrolPoint);

        float distanceToDestination = (transform.position - patrolPoint).magnitude;

        if (distanceToDestination < 1f)
            patrolPointSet = false;

    }

    void ChasePlayer()
    {
        agent.SetDestination(playerTransform.position);
    }
}
