using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CactusGuy : MonoBehaviour
{
    // Health Bar
    public Healthbar healthbar;
    public int maxHealth = 100;
    public int currentHealth;

    // Shield Bar
    public Shieldbar shieldbar;
    public int maxShield = 100;
    public int currentShield;

    void Awake()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        currentShield = maxShield;
        shieldbar.SetMaxShield(maxShield);
    }

    public void TakeDamage(int damage)
    {
        if(shieldbar.slider.value > 0){
            currentShield -= damage;
            shieldbar.SetSield(currentShield);
        }
        else
        {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
        }
    }

    public void HealShield(int heal)
    {
        currentShield = Mathf.Min(currentShield + heal, maxShield);
        shieldbar.SetSield(currentShield);
    }

    public void HealHealth(int heal)
    {
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
        healthbar.SetHealth(currentHealth);
    }

    // [SerializeField] private float _maxHealth = 3;
    // [SerializeField] private GameObject _deathEffect, _hitEffect;
    // private float _currentHealth;

    // [SerializeField] private Healthbar _healthbar;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     _currentHealth = _maxHealth;

    //     _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
    // }

    // void OnMouseEnter() {
    //     _currentHealth -= Random.Range(0.5f, 1.5f);

    //     if(_currentHealth <= 0) {
    //         //Instantiate(_deathEffect, transform.position, Quaternion.Euler(-90,0,0));
    //         Destroy(gameObject);
    //         RestartScene();
    //     }
    //     else{
    //         _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
    //         Instantiate(_hitEffect, transform.position, Quaternion.identity);
    //     }
    // }

    // public void RestartScene()
    //  {
    //      Scene thisScene = SceneManager.GetActiveScene();
    //      SceneManager.LoadScene(thisScene.name);
    //  }
}
