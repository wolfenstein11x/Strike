using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 3f;
    [SerializeField] float walkSpeed = 2.5f;

    NavMeshAgent navMeshAgent;
    Animator animator;

    int currentWaypointIndex = 0;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Halt()
    {
        navMeshAgent.SetDestination(transform.position);
        navMeshAgent.isStopped = true;
    }

    public void UnHalt()
    {
        navMeshAgent.isStopped = false;
    }

    public void Patrol()
    {
        Vector3 nextPosition = transform.position;

        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWayPoint();
            }

            nextPosition = GetCurrentWaypoint();
        }

        if (timeSinceArrivedAtWaypoint > waypointDwellTime)
        {
            MoveToPoint(nextPosition);
        }
    }

    public bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void CycleWayPoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private void MoveToPoint(Vector3 pos)
    {
        navMeshAgent.SetDestination(pos);
    }

    public void UpdateTimers()
    {
        timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    public void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("ForwardSpeed", speed);
    }
}
