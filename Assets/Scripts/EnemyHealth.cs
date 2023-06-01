using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [NonSerialized] public bool cantBeDamage;

    public Healthbar healthbar;

    public event EventHandler onHitAnimation;
    public event EventHandler onDead;
    

    void Awake()
    {
        healthbar = GetComponentInChildren<Healthbar>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth); 
        cantBeDamage = false;
    }

    public void TakeDamage(int damage)
    {
        if(!cantBeDamage){
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
            onHitAnimation?.Invoke(this, EventArgs.Empty);
            if (currentHealth <= 0)
            {
            onDead?.Invoke(this, EventArgs.Empty); 
            }
        }
    }

    public bool IsDead() {
        return currentHealth <= 0;
    }

    public float GetHealthNormalized() {
        return (float)currentHealth / maxHealth;
    }

    public int GetHealthMax() {
        return maxHealth;
    }

}
