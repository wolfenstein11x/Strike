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




}
