using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 3f;
    public ParticleSystem muzzleFlash;
    public AudioSource gunSound;
    public Projectile projectile;
    public Transform projectileSpawnPoint;
    [SerializeField] float shootForce = 5f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float walkSpeed = 2.5f;
    [SerializeField] float angleRange = 10f;
    public float spread = 3f;

    public float fireRange = 10f;
    public float chaseRange = 25f;

    protected NavMeshAgent navMeshAgent;
    Animator animator;
    protected Transform player;
    protected Vector3 midsectionOffset = new Vector3(0f, 1.5f, 0f);
    protected LayerMask shootableLayers;
    protected int shootableLayer;
    protected LayerMask shieldLayers;

    int currentWaypointIndex = 0;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    bool isProvoked = false;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        shootableLayers = LayerMask.GetMask("Player");

        navMeshAgent.speed = walkSpeed;
    }

    protected virtual void Initialize()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        shootableLayer = LayerMask.NameToLayer("Player");

        navMeshAgent.speed = walkSpeed;
    }


    void Update()
    {
        // for debugging only
        //Patrol();
        //UpdateTimers();
        //UpdateAnimator();
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

    public virtual void FireRound()
    {
        // play audio and visual effects
        muzzleFlash.Play();
        gunSound.Play();

        // generate spread vector
        float xSpread = Random.Range(-spread, spread);
        float ySpread = Random.Range(-spread, spread);
        float zSpread = Random.Range(-spread, spread);
        Vector3 spreadVector = new Vector3(xSpread, ySpread, zSpread);

        // calculate direction of projectile
        Vector3 startPoint = transform.position + midsectionOffset; //projectileSpawnPoint.position;
        Vector3 endPoint = player.transform.position + spreadVector;

        Vector3 dir = (endPoint - startPoint).normalized;

        // instantiate projectile
        Projectile firedProjectile = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);

        // add force to projectile so it moves
        firedProjectile.GetComponent<Rigidbody>().AddForce(dir * shootForce, ForceMode.Impulse);
    }

    public virtual bool PlayerInSights()
    {
        // this is a problem, need to fix
        // player is automatically in sights if closer than stopping distance
        if (Vector3.Distance(transform.position, player.transform.position) <= navMeshAgent.stoppingDistance) { return true; }

        Vector3 startPoint = transform.position + midsectionOffset;
        Vector3 endPoint = player.transform.position;

        Vector3 dir = (endPoint - startPoint).normalized;

        RaycastHit hit;
        if (Physics.Raycast(startPoint, dir, out hit, fireRange, shootableLayers) && WithinAngle(dir))
        {
            //Debug.DrawRay(startPoint, dir * hit.distance, Color.green);
            return true;
        }
        else
        {
            //Debug.DrawRay(startPoint, dir * fireRange, Color.yellow);
            return false;
        }

    }

    // for debugging only
    void ShowForwardDirection()
    {
        Debug.DrawRay(transform.position + midsectionOffset, Vector3.forward * fireRange, Color.blue);
    }

    protected bool WithinAngle(Vector3 direction)
    {
        float angle = Vector3.Angle(direction, transform.forward);
        //Debug.Log(angle + " degrees");
        return angle <= angleRange;
    }

    

    public bool PlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    public void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    public void FaceTarget()
    {
        transform.LookAt(player);
        //Vector3 direction = (player.position - transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navMeshAgent.angularSpeed);
    }

    public void SetRunning(bool isRunning)
    {
        navMeshAgent.speed = isRunning ? runSpeed : walkSpeed;
    }

    public bool IsProvoked()
    {
        return isProvoked;
    }

    public void SetProvoked(bool status)
    {
        isProvoked = status;
    }

}
