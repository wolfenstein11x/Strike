using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem zoomedMuzzleFlash;
    [SerializeField] AudioSource gunSound;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject reticle;

    [SerializeField] bool hasZoom = false;
    [SerializeField] GameObject zoomReticle;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = 0.5f;

    bool zoomedInToggle = false;
    RigidbodyFirstPersonController fpsController;
    MeshRenderer meshRenderer;

    private void Start()
    {
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        else if (Input.GetMouseButtonDown(1))
        {
            ToggleZoom();
        }

    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        gunSound.Play();
        ProcessRaycast();

    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }

        else
        {
            return;
        }
    }

    private void PlayMuzzleFlash()
    {
        if (!zoomedInToggle) { muzzleFlash.Play(); }
        else { zoomedMuzzleFlash.Play(); }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }

    private void ToggleZoom()
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
