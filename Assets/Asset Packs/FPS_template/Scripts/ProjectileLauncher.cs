using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : Weapon
{
    [SerializeField] GameObject projectile;
    [SerializeField] float spread = 0.1f;
    [SerializeField] Transform launchPoint;
    [SerializeField] float shootForce = 5f;

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

        // a ray through the middle of the screen
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint = ray.GetPoint(range);

        PlayMuzzleFlash();
        gunSound.Play();

        // generate spread vector
        float xSpread = Random.Range(-spread, spread);
        float ySpread = Random.Range(-spread, spread);
        float zSpread = Random.Range(-spread, spread);
        Vector3 spreadVector = new Vector3(xSpread, ySpread, zSpread);

        // calculate direction of projectile
        Vector3 dir = (targetPoint - launchPoint.position).normalized;

        // instantiate projectile
        GameObject firedProjectile = Instantiate(projectile, launchPoint.position, transform.rotation);

        // add force to projectile so it moves
        firedProjectile.GetComponent<Rigidbody>().AddForce(dir * shootForce, ForceMode.Impulse);

        ammoTracker.DecrementAmmo();

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
        }
    }
}
