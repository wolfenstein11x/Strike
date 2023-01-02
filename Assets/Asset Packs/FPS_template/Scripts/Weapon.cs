using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Weapon : MonoBehaviour
{
    public Camera fpsCamera;
    public float range = 100f;
    public float noiseRadius = 10f;
    [SerializeField] float damage = 30f;
    public float timeBetweenShots = 1f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem zoomedMuzzleFlash;
    public AudioSource gunSound;
    [SerializeField] bool automatic = false;
    [SerializeField] GameObject hitEffect;
    [SerializeField] float provocationRadius = 10f;
    [SerializeField] GameObject reticle;

    [SerializeField] bool hasZoom = false;
    [SerializeField] GameObject zoomReticle;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = 0.5f;
    public AudioSource outOfAmmoSound;

    bool zoomedInToggle = false;
    RigidbodyFirstPersonController fpsController;
    MeshRenderer meshRenderer;
    protected AmmoTracker ammoTracker;

    protected bool readyToShoot;
    protected bool allowInvoke;
    protected FlagTracker flagTracker;

    private void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();
        meshRenderer = GetComponent<MeshRenderer>();
        ammoTracker = GetComponentInChildren<AmmoTracker>();
        readyToShoot = true;
        allowInvoke = true;
        flagTracker = FindObjectOfType<FlagTracker>();
    }

    void Update()
    {
        if (automatic)
        {
            HandleInputAuto();
        }

        else
        {
            HandleInputSemi();
        }
    }

    void HandleInputSemi()
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

    void HandleInputAuto()
    {
        // must have ammo to prevent out-of-ammo sound from playing every frame when out of ammo
        if (Input.GetButton("Fire1") && readyToShoot && ammoTracker.GetAmmoCount() > 0)
        {
            Shoot();
        }

        else if (Input.GetMouseButtonDown(1))
        {
            ToggleZoom();
        }

        // if out of ammo, play out-of-ammo sound once per button press
        else if (Input.GetButtonDown("Fire1") && ammoTracker.GetAmmoCount() <= 0)
        {
            Shoot();
        }
    }

    protected virtual void Shoot()
    {
        // don't shoot if paused
        if (flagTracker.GamePaused()) { return; }

        // don't shoot if player dead
        if (flagTracker.PlayerDead()) { return; }

        // don't shoot if out of ammo
        if (ammoTracker.GetAmmoCount() <= 0)
        {
            outOfAmmoSound.Play();
            return;
        }

        readyToShoot = false;

        PlayMuzzleFlash();
        gunSound.Play();
        CreateNoiseProvocationSphere(noiseRadius);
        ProcessRaycast();
        ammoTracker.DecrementAmmo();

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
        }

    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            CreateProvocationSphere(hit, provocationRadius);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }

        else
        {
            return;
        }
    }

    protected void PlayMuzzleFlash()
    {
        if (!zoomedInToggle) { muzzleFlash.Play(); }
        else { zoomedMuzzleFlash.Play(); }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }

    private void CreateProvocationSphere(RaycastHit hit, float radius)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(hit.point, radius);
        foreach (Collider col in objectsInRange)
        {
            // soldier is provoked if bullet hits near him
            Soldier enemy = col.GetComponent<Soldier>();
            if (enemy != null)
            {
                enemy.SetProvoked(true);
            }

            // NPC gets scared if bullet hits near them
            NPC npc = col.GetComponent<NPC>();
            if (npc != null)
            {
                npc.TriggerScare();
            }
        }
    }

    protected void CreateNoiseProvocationSphere(float radius)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider col in objectsInRange)
        {
            // soldier is provoked if he hears gunshot
            Soldier enemy = col.GetComponent<Soldier>();
            if (enemy != null)
            {
                enemy.SetProvoked(true);
            }

            // NPC gets scared if they hear gunshot
            NPC npc = col.GetComponent<NPC>();
            if (npc != null)
            {
                npc.TriggerScare();
            }
        }
    }

    protected void ToggleZoom()
    {
        if (!hasZoom) return;

        if (zoomedInToggle == false)    // zoom in
        {
            zoomedInToggle = true;
            fpsCamera.fieldOfView = zoomedInFOV;
            fpsController.mouseLook.XSensitivity = zoomInSensitivity;
            fpsController.mouseLook.YSensitivity = zoomInSensitivity;
            ShowWeapon(false);
            reticle.SetActive(false);
            zoomReticle.SetActive(true);

            // set zoom flag so you can't switch weapons while zoomed
            flagTracker.SetZoomedIn(true);
        }

        else
        {   // zoom out
            zoomedInToggle = false;
            fpsCamera.fieldOfView = zoomedOutFOV;
            fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
            fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
            ShowWeapon(true);
            zoomReticle.SetActive(false);
            reticle.SetActive(true);

            // reset zoom flag so you can switch weapons
            flagTracker.SetZoomedIn(false);
        }
    }

    private void ShowWeapon(bool show)
    {
        meshRenderer.enabled = show;
    }

    private void OnEnable()
    {
        if (reticle != null) { reticle.SetActive(true); }
        if (zoomReticle != null) { zoomReticle.SetActive(false); }
    }

    private void OnDisable()
    {
        if (reticle != null) { reticle.SetActive(false); }
        if (zoomReticle != null) { zoomReticle.SetActive(false); }
    }
}
