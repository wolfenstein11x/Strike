using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Soldier
{
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
}
