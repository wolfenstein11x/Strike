using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] float damageRadius = 5f;

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
        PlayImpactEffects();

        AreaDamageEnemies(transform.position, damageRadius, damage);

        Destroy(gameObject);
    }

    // got this code from 'duck' on a Unity forum
    void AreaDamageEnemies(Vector3 location, float radius, float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            // TODO: do the same for Player so player can get killed by blast too (easy)
            EnemyHealth enemy = col.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                // linear falloff of effect
                float proximity = (location - enemy.transform.position).magnitude;
                float effect = 1 - (proximity / radius);

                enemy.TakeDamage(damage * effect, true);
            }
        }

    }


}
