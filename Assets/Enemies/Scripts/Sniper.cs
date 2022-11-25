using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Soldier
{
    [SerializeField] int weaponDamage = 20;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool PlayerInSights()
    {
        Vector3 startPoint = projectileSpawnPoint.position;
        Vector3 endPoint = player.transform.position;

        Vector3 dir = (endPoint - startPoint).normalized;

        RaycastHit hit;

        // raycast hit something:
        if (Physics.Raycast(startPoint, dir, out hit, fireRange))
        {
            // raycast hit player
            if (hit.transform.gameObject.layer == shootableLayer)
            {
                //Debug.DrawRay(startPoint, dir * hit.distance, Color.red);
                return true;
            }

            // raycast hit something blocking player
            else
            {
                //Debug.DrawRay(startPoint, dir * hit.distance, Color.green);
                return false;
            }
        }

        // raycast hit nothing:
        else
        {
            //Debug.DrawRay(startPoint, dir * fireRange, Color.yellow);
            return false;
        }
    }

    public override void FireRound()
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
        Vector3 startPoint = projectileSpawnPoint.position;
        Vector3 endPoint = player.transform.position + spreadVector;

        Vector3 dir = (endPoint - startPoint).normalized;

        RaycastHit hit;
        if (Physics.Raycast(startPoint, dir, out hit, fireRange))
        {
            PlayerHealth player = hit.transform.GetComponent<PlayerHealth>();
            if (player == null) return;
            player.TakeDamage(weaponDamage);
        }
        
    }
}
