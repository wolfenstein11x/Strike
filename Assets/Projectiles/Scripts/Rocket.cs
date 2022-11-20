using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
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

    protected override void OnCollisionEnter(Collision collision)
    {
        HandleImpact(collision);

    }

    protected override void HandleImpact(Collision collision)
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


}
