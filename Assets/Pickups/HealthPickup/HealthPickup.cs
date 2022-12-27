using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount = 50;
    [SerializeField] AudioSource healSound;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.Heal(50);
            healSound.Play();
            Destroy(gameObject, 0.25f);
        }
    }


}
