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

    

    


}
