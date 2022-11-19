using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float maxLifetime = 3f;
    [SerializeField] int damage = 10;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject impactSound;
    [SerializeField] float impactEffectLifetime = 0.5f;
    [SerializeField] float impactSoundLifetime = 1f;


    // Start is called before the first frame update
    void Start()
    {

        // de-child bullet from shooter so it does not move with shooter
        transform.parent = null;

        // destroy bullet at pre-determined time so it doesn't fly until it hits something/forever
        Destroy(gameObject, maxLifetime);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        PlayImpactEffects();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    private void PlayImpactEffects()
    {
        if (impactSound != null)
        {
            GameObject impactSoundInstance = Instantiate(impactSound, transform.position, transform.rotation);
            Destroy(impactSoundInstance, impactSoundLifetime);
        }

        if (impactEffect != null)
        {
            GameObject impactEffectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(impactEffectInstance, impactEffectLifetime);
        }
    }
}
