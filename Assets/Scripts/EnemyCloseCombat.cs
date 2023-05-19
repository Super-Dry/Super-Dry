using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloseCombat : MonoBehaviour, IAttack
{
    [SerializeField] private int m_damage;
    [SerializeField] private CactusGuy cactusGuy;
    [SerializeField] public int damage
    {
        get{return m_damage;}
        set{damage = damage;}
    }

    void Awake()
    {
        cactusGuy = GameObject.FindGameObjectWithTag("Player").GetComponent<CactusGuy>();
    }

    public void Attack()
    {
        cactusGuy.TakeDamage(damage);
    }

}

