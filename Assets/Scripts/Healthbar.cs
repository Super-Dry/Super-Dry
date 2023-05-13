using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    // [SerializeField] private Image _healthbarSprite;
    // [SerializeField] private float _reduceSpeed = 2;
    // private float _target = 1;

    // private Camera _cam;

    // void Start(){
    //     _cam = Camera.main;
    // }

    // public void UpdateHealthBar(float maxHealth, float currentHealth){
    //     _target = currentHealth / maxHealth;
    // }

    // void Update(){
    //     transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    //     _healthbarSprite.fillAmount = Mathf.MoveTowards(_healthbarSprite.fillAmount, _target, _reduceSpeed * Time.deltaTime);
    // }
}
