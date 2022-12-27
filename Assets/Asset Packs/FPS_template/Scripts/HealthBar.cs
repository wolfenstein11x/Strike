using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void ShowHealAnimation()
    {
        animator.SetTrigger("heal");
    }

    
}
