using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    [SerializeField] float damageRadius = 5f;

    void Start()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        // de-child bullet from shooter so it does not move with shooter
        transform.parent = null;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "bombBouncer")
        {
            return;
        }

        HandleImpact(collision);

    }

    protected override void HandleImpact(Collision collision)
    {
        PlayImpactEffects();

        AreaDamageEffects(transform.position, damageRadius, damage);

        Destroy(gameObject);
    }

    // bombs don't kill enemies like other projectiles do
    protected override void AreaDamageEffects(Vector3 location, float radius, float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            //EnemyHealth enemy = col.GetComponent<EnemyHealth>();
            PlayerHealth player = col.GetComponent<PlayerHealth>();
            NPC npc = col.GetComponent<NPC>();

            /*
            if (enemy != null)
            {
                // linear falloff of effect
                float proximity = (location - enemy.transform.position).magnitude;
                float effect = 1 - (proximity / radius);

                enemy.TakeDamage(damage * effect, true);
            }
            */

            if (player != null)
            {
                // linear falloff of effect
                float proximity = (location - player.transform.position).magnitude;
                float effect = 1 - (proximity / radius);

                int damageInt = (int)(damage * effect);

                player.TakeDamage(damageInt);
            }

            if (npc != null)
            {
                npc.TriggerScare();
            }


        }
    }




}
