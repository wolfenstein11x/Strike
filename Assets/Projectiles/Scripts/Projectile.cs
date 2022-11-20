using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxLifetime = 3f;
    public int damage = 10;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject impactSound;
    [SerializeField] float impactEffectLifetime = 0.5f;
    [SerializeField] float impactSoundLifetime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected void Initialize()
    {
        // de-child bullet from shooter so it does not move with shooter
        transform.parent = null;

        // destroy bullet at pre-determined time so it doesn't fly until it hits something/forever
        Destroy(gameObject, maxLifetime);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        HandleImpact(collision);
    }

    protected void PlayImpactEffects()
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

    protected virtual void HandleImpact(Collision collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

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
}
