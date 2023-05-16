using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [NonSerialized] public bool isDead = false;

    public Healthbar healthbar;

    public event EventHandler isHit;

    void Awake()
    {
        healthbar = GetComponentInChildren<Healthbar>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth); 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        isHit?.Invoke(this, EventArgs.Empty);
        if (currentHealth <= 0)
        {
           isDead = true; 
        }
    }

}
