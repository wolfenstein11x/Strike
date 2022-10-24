using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 3f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioSource gunSound;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] float fireRange = 20f;
    [SerializeField] float shootForce = 5f;

    NavMeshAgent navMeshAgent;
    Animator animator;

    int currentWaypointIndex = 0;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        DoRaycast();
    }

    public void Halt()
    {
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

    public void FireRound()
    {
        // play audio and visual effects
        muzzleFlash.Play();
        gunSound.Play();

        // calculate direction of projectile
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // instantiate projectile
        Projectile firedProjectile = Instantiate(projectile, projectileSpawnPoint.position, projectile.transform.rotation);

        // add force to projectile so it moves
        firedProjectile.GetComponent<Rigidbody>().AddForce(forward.normalized * shootForce, ForceMode.Impulse);
    }

    private void DoRaycast()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * fireRange;
        Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), forward, Color.red);
    }


}
