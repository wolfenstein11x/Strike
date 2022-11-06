using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float maxLifetime = 3f;
    [SerializeField] int damage = 10;


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

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
