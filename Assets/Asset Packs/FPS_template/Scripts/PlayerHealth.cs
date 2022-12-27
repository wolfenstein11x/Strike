using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] HealthBar healthBar;

    DamageCanvas damageCanvas;

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

        damageCanvas = FindObjectOfType<DamageCanvas>();
    }

    public void TakeDamage(int damage)
    {
        damageCanvas.ActivateRandomBloodSplat();

        currentHealth -= damage;

        if (currentHealth < 0) { currentHealth = 0; }

        healthBar.SetHealth(currentHealth);
    }

    public void Heal(int healAmount)
    {
        healthBar.ShowHealAnimation();

        currentHealth += healAmount;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }

        healthBar.SetHealth(currentHealth);
    }
}
