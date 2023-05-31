using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour, IAttack
{
    [SerializeField] private int m_damage;
    [SerializeField] private CactusGuy cactusGuy;
    [SerializeField] public int damage
    {
        get{return m_damage;}
        set{damage = damage;}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        GameObject bulletObj = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        Vector3 shootingDirection = playerTargerPoint.transform.position - shootPoint.position;
        bulletObj.transform.forward = shootingDirection.normalized;
        bulletObj.GetComponent<Rigidbody>().AddForce(shootingDirection.normalized * bulletSpeed, ForceMode.Impulse);
    }
}
