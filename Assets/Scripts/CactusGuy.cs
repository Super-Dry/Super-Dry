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
    [SerializeField] private EndGame endGame;
    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Cinemachine.CinemachineBrain cinemachineBrain;
    [SerializeField] private GameObject shootingSound;
    [SerializeField] private GameObject footstepSound;
    [SerializeField] private GameObject HUDCanvas;


    void Awake()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        currentShield = maxShield;
        shieldbar.SetMaxShield(maxShield);

        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
        thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
        endGame = GameObject.Find("EndGameCanvas").GetComponent<EndGame>();
        shootingSound = GameObject.Find("ShootingSound");
        footstepSound = GameObject.Find("Footsteps");
        HUDCanvas = GameObject.Find("HUD Canvas");
    }

    public void TakeDamage(int damage)
    {
        hurtSound.Play();
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
        HUDCanvas.SetActive(false);
        backgroundMusic.Stop();
        shootingSound.SetActive(false);
        footstepSound.SetActive(false);
        thirdPersonShooterController.readyToShoot = false;
        thirdPersonMovement.allowToMove = false;
        thirdPersonMovement.enabled = false;
        gameObject.transform.rotation = (Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation);
        cinemachineBrain.m_DefaultBlend.m_Time = 1f;
        cameraManager.EnableKillCam();
        endGame.GameEnded();
    }
}
