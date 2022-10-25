using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            animator.SetTrigger("die");
            //Destroy(gameObject);
        }

        else
        {
            animator.SetTrigger("takeHit");
        }
    }
}
