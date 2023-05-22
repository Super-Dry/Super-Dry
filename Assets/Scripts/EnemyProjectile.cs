using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour, IAttack
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject shootPointObj;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject playerTargerPointObj;
    [SerializeField] private Transform playerTargerPoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int m_damage;
    [SerializeField] public int damage
    {
        get{return m_damage;}
        set{damage = damage;}
    }

    void Awake()
    {
        shootPoint = transform.Find("ShootPoint").GetComponent<Transform>();
        playerTargerPoint = GameObject.FindGameObjectWithTag("PlayerTargetPoint").GetComponent<Transform>();
    }

    public void Attack()
    {
        GameObject bulletObj = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        Vector3 shootingDirection = playerTargerPoint.transform.position - shootPoint.position;
        bulletObj.transform.forward = shootingDirection.normalized;
        bulletObj.GetComponent<Rigidbody>().AddForce(shootingDirection.normalized * bulletSpeed, ForceMode.Impulse);
    }
}
