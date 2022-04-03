using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody rb;
    public NavMeshAgent agent;
    public Transform playerTransform;
    public LayerMask groundMask, playerMask;
    public float patrolSpeed = 3.0f, pursuitSpeed = 4.0f, attackSpeed = 8.0f;

    // Patrolling
    public Vector3 patrolPoint;
    public float patrolPointRange;
    public bool patrolPointSet;

    // Stalking
    public float sightRange = 20.0f;
    bool inPlayerPursuit;

    // Attacking
    public Vector3 attackLocation, attackDirection;
    public float attackRange = 5.0f, attackDistance = 10.0f, attackTime = 3.0f, attackTimeElapsed = 0.0f;
    public bool playerInAttackRange, isAttacking;

    // Teleporting
    public Transform[] teleportTransforms;
    public float teleportInterval = 50.0f, teleportIntervalElapsed = 0.0f;
    public bool canTeleport;

    // Called to initialize variables before game start.
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        teleportIntervalElapsed = 0.0f;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = GameObject.Find("First Person Controller").transform;
        inPlayerPursuit = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!canTeleport)
        {
            teleportIntervalElapsed += Time.deltaTime;

            if (teleportIntervalElapsed > teleportInterval)
            {
                canTeleport = true;
                teleportIntervalElapsed = 0.0f;
            }
        }    

        if (playerInAttackRange || isAttacking)
        {
            patrolPointSet = false;
            Attack();
        }
        else if (inPlayerPursuit)
        {
            patrolPointSet = false;
            ChasePlayer();
        }
        else
        {
            if (canTeleport && !patrolPointSet && (teleportTransforms.Length > 0))
                Teleport();
            else
                Patrol();
        }
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

    void Teleport()
    {
        int idx = Random.Range(0, teleportTransforms.Length);
        Vector3 teleportLocation = teleportTransforms[idx].position;
        NavMeshHit hit;

        NavMesh.SamplePosition(teleportLocation, out hit, 20.0f, NavMesh.AllAreas);
        Vector3 teleportPoint = hit.position;

        transform.position = teleportPoint;
        canTeleport = false;
    }

    void Patrol()
    {
        agent.speed = patrolSpeed;
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
        agent.speed = pursuitSpeed;
        agent.SetDestination(playerTransform.position);
    }

    void Attack()
    {
        if (!isAttacking)
        {
            attackDirection = (playerTransform.position - transform.position).normalized;
            attackLocation = transform.position + (attackDirection * attackDistance);
            NavMeshHit hit;

            NavMesh.SamplePosition(attackLocation, out hit, 20.0f, NavMesh.AllAreas);
            attackLocation = hit.position;

            isAttacking = true;
            attackTimeElapsed = 0.0f;
            agent.speed = attackSpeed;
        }

        if (isAttacking)
        {
            if (attackTimeElapsed > attackTime)
            {
                isAttacking = false;
                attackTimeElapsed = 0.0f;
                agent.speed = pursuitSpeed;
            }
            agent.SetDestination(attackLocation);

            attackTimeElapsed += Time.deltaTime;
        }
    }
}
