using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = 0.5f;

    RigidbodyFirstPersonController fpsController;

    bool zoomedInToggle = false;

    private void Start()
    {
        fpsController = GetComponent<RigidbodyFirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (GetComponentInChildren<Weapon>() == null) { return; }

            if (zoomedInToggle == false)
            {
                zoomedInToggle = true;
                fpsCamera.fieldOfView = zoomedInFOV;
                fpsController.mouseLook.XSensitivity = zoomInSensitivity;
                fpsController.mouseLook.YSensitivity = zoomInSensitivity;
                //ScaleReticleUp();
            }

            else
            {
                zoomedInToggle = false;
                fpsCamera.fieldOfView = zoomedOutFOV;
                fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
                fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
                //ScaleReticleDown();
            }
        }
    }

    /*
    private void ScaleReticleUp()
    {
        foreach(GameObject reticle in zoomableReticles)
        {
            if (reticle.activeInHierarchy)
            {
                reticle.transform.localScale = new Vector3(reticle.transform.localScale.x * zoomReticleScale, 
                                                           reticle.transform.localScale.y * zoomReticleScale, 
                                                           reticle.transform.localScale.z * zoomReticleScale);
            }
        }
    }

    private void ScaleReticleDown()
    {
        foreach (GameObject reticle in zoomableReticles)
        {
            if (reticle.activeInHierarchy)
            {
                reticle.transform.localScale = new Vector3(reticle.transform.localScale.x / zoomReticleScale,
                                                           reticle.transform.localScale.y / zoomReticleScale,
                                                           reticle.transform.localScale.z / zoomReticleScale);
            }
        }
    }
    */
}
