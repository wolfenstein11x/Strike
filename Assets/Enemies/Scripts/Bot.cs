using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Soldier
{
    [SerializeField] AudioSource walkSound;
    [SerializeField] ParticleSystem[] muzzleFlashes;
    [SerializeField] Transform[] projectileSpawnPoints;
    [SerializeField] float fireRangeMin = 15f;
    [SerializeField] float fireRangeBuffer = 5f;

    private Vector3 heightOffset = new Vector3(0f, 1f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget();
    }

    public void PlayWalkSound()
    {
        walkSound.Play();
    }

    public void FireGun(int gunIndex)
    {
        gunSound.Play();
        muzzleFlashes[gunIndex].Play();
        
        // generate spread vector
        float xSpread = Random.Range(-spread, spread);
        float ySpread = Random.Range(-spread, spread);
        float zSpread = Random.Range(-spread, spread);
        Vector3 spreadVector = new Vector3(xSpread, ySpread, zSpread);

        // calculate direction of projectiles
        Vector3 startPoint = projectileSpawnPoints[gunIndex].position;
        Vector3 endPoint = player.transform.position + heightOffset + spreadVector;

        Vector3 dir = (endPoint - startPoint).normalized;

        // instantiate projectile
        Projectile firedProjectile = Instantiate(projectile, projectileSpawnPoints[gunIndex].position, transform.rotation);

        // add force to projectile so it moves
        firedProjectile.GetComponent<Rigidbody>().AddForce(dir * shootForce, ForceMode.Impulse);
    }

    public override bool PlayerInSights()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= navMeshAgent.stoppingDistance) { return true; }

        for (int i=0; i<projectileSpawnPoints.Length; i++)
        {
            Vector3 startPoint = projectileSpawnPoints[i].position;
            Vector3 endPoint = player.transform.position + heightOffset;

            Vector3 dir = (endPoint - startPoint).normalized;

            RaycastHit hit;
            if (Physics.Raycast(startPoint, dir, out hit, fireRange, shootableLayers))
            {
                Debug.DrawRay(startPoint, dir * hit.distance, Color.red);
                return true;
            }
            else
            {
                Debug.DrawRay(startPoint, dir * fireRange, Color.green);
            }
        }

        return false;
        
    }

    public bool WithinMaxFiringRange(bool plusBuffer = false)
    {
        if (plusBuffer)
        {
            return (Vector3.Distance(transform.position, player.transform.position) <= (fireRange + fireRangeBuffer));
        }

        else
        {
            return (Vector3.Distance(transform.position, player.transform.position) <= fireRange);
        }
    }

    public bool WithinMinFiringRange(bool minusBuffer = false)
    {
        if (minusBuffer)
        {
            return (Vector3.Distance(transform.position, player.transform.position) >= (fireRangeMin - fireRangeBuffer));
        }

        else
        {
            return (Vector3.Distance(transform.position, player.transform.position) >= fireRangeMin);
        }
    }
}
