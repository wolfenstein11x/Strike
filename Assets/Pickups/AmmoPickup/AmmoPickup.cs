using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoTracker ammoTracker;
    [SerializeField] AudioSource ammoPickupSound;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // if someone other than player collides with it, nothing happens
        if (other.gameObject.tag != "Player") { return; }

        // if specific weapon not active, nothing happens
        if (!ammoTracker.isActiveAndEnabled) { return; }

        ammoPickupSound.Play();
        ammoTracker.CollectAmmo();

        Destroy(gameObject, 0.4f);
    }
}
