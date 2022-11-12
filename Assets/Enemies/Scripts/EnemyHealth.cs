using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    private bool isDead;

    Animator animator;
    PlayerStatus player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerStatus>();
        isDead = false;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) { return; }

        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Die();
        }

        else
        {
            animator.SetTrigger("takeHit");
        }
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("die");
        player.RecordKill(gameObject.tag);
        //Destroy(gameObject);
    }
}
