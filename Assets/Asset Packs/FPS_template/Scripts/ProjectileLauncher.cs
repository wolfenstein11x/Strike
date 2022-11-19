using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : Weapon
{
    [SerializeField] GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && readyToShoot)
        {
            Shoot();
        }

        else if (Input.GetMouseButtonDown(1))
        {
            ToggleZoom();
        }
    }

    protected override void Shoot()
    {
        readyToShoot = false;

        PlayMuzzleFlash();
        gunSound.Play();

        ammoTracker.DecrementAmmo();

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
        }
    }
}
