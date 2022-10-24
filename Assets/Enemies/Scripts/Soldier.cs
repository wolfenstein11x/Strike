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
    [SerializeField] float shootForce = 5f;

    public float fireRange = 10f;
    public float chaseRange = 25f;

    NavMeshAgent navMeshAgent;
    Animator animator;
    Transform player;

    int currentWaypointIndex = 0;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }

    
    void Update()
    {

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
        Projectile firedProjectile = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);

        // add force to projectile so it moves
        firedProjectile.GetComponent<Rigidbody>().AddForce(forward.normalized * shootForce, ForceMode.Impulse);
    }

    // function for debugging only
    private void DoRaycast()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * fireRange;
        Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), forward, Color.red);
    }

    public bool PlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    public bool PlayerInFireRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= fireRange;
    }

    public void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    public void FaceTarget()
    {
        transform.LookAt(player);
    }

}
