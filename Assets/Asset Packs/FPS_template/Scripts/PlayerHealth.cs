using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] HealthBar healthBar;

    GameOverMenu gameOverMenu;
    DamageCanvas damageCanvas;
    FlagTracker flagTracker;

    int currentHealth;

    void Start()
    {
        gameOverMenu = FindObjectOfType<GameOverMenu>();
        gameOverMenu.gameObject.SetActive(false);

        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

        damageCanvas = FindObjectOfType<DamageCanvas>();
        flagTracker = FindObjectOfType<FlagTracker>();
    }

    public void TakeDamage(int damage)
    {
        damageCanvas.ActivateRandomBloodSplat();

        currentHealth -= damage;

        if (currentHealth < 0) { currentHealth = 0; }

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) { HandlePlayerDeath(); }
    }

    public void Heal(int healAmount)
    {
        healthBar.ShowHealAnimation();

        currentHealth += healAmount;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }

        healthBar.SetHealth(currentHealth);
    }

    void HandlePlayerDeath()
    {
        gameOverMenu.gameObject.SetActive(true);
        flagTracker.SetPlayerDead(true);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
