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

    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private ThirdPersonMovement thirdPersonMovement;
    [SerializeField] private ThirdPersonShooterController thirdPersonShooterController;


    void Awake()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        currentShield = maxShield;
        shieldbar.SetMaxShield(maxShield);

        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
        thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
    }

    public void TakeDamage(int damage)
    {
        if(shieldbar.slider.value > 0){
            if(shieldbar.slider.value < damage){
                int carryOver = (int)(damage - shieldbar.slider.value);
                currentShield = 0;
                shieldbar.SetSield(currentShield);
                currentHealth -= carryOver;
                healthbar.SetHealth(currentHealth);
            }else{
                currentShield -= damage;
                shieldbar.SetSield(currentShield);
            }
        }
        else
        {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {
            OnDeath();
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

   void OnDeath()
    {
        thirdPersonShooterController.readyToShoot = false;
        thirdPersonMovement.allowToMove = false;
        // cameraManager.EnableKillCam();
    }
}
