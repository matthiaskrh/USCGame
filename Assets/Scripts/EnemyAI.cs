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
    public bool patrolPointSet;

    // Stalking
    public float sightRange;
    bool inPlayerPursuit;

    // Attacking
    public float attackSpeed, attackRange;
    public bool playerInAttackRange;

    // Teleporting
    public bool canTeleport;

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
        inPlayerPursuit = Physics.CheckSphere(transform.position, sightRange, playerMask);

        if (inPlayerPursuit)
        {
            Debug.Log("Chasing Player");
            ChasePlayer();
        }
        else
            Patrol();
    }

    void FindPatrolPoint()
    {
        Vector3 randPositionOffset = Random.insideUnitSphere * patrolPointRange;
        Vector3 point = transform.position + randPositionOffset;
        NavMeshHit hit;

        NavMesh.SamplePosition(point, out hit, patrolPointRange, NavMesh.AllAreas);
        patrolPoint = hit.position;

        if (Physics.Raycast(patrolPoint, -transform.up, 2f, 1))
            patrolPointSet = true;
    }

    void Patrol()
    {
        if (!patrolPointSet)
            FindPatrolPoint();

        if (patrolPointSet)
            agent.SetDestination(patrolPoint);

        float distanceToDestination = (transform.position - patrolPoint).magnitude;

        if (distanceToDestination <= 2f)
            patrolPointSet = false;
    }

    void ChasePlayer()
    {
        agent.SetDestination(playerTransform.position);
    }
}
