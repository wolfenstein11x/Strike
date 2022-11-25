using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    private bool isDead;

    Animator animator;
    PlayerStatus player;
    Soldier soldier;

    List<string> dieAnimations = new List<string>() { "die", "die1", "die2", "die3" };

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerStatus>();
        soldier = GetComponent<Soldier>();
        isDead = false;
    }

    public void TakeDamage(float damage, bool explosionDamage=false)
    {
        if (isDead) { return; }

        soldier.SetProvoked(true);

        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Die(explosionDamage);
        }

        else
        {
            animator.SetTrigger("takeHit");
        }
    }

    public void Die(bool explosionDeath=false)
    {
        isDead = true;
        player.RecordKill(gameObject.tag);
        soldier.Halt();

        if (explosionDeath)
        {
            animator.SetTrigger("dieExplosion");
        }

        else
        {
            // randomly select die animation
            string dieAnimation = dieAnimations[Random.Range(0, dieAnimations.Count)];
            animator.SetTrigger(dieAnimation);
        }
        

        //Destroy(gameObject);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
