using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardAttack : MonoBehaviour, IAttack
{
    [SerializeField] private TornadoScript tornado;
    // [SerializeField] private GameObject shootPointObj;
    [SerializeField] private GameObject playerTargerPointObj;
    [SerializeField] private Transform playerTargerPoint;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private float tornadoSpeed;
    [SerializeField] private int m_damage;
    [SerializeField] public int damage
    {
        get{return m_damage;}
        set{damage = damage;}
    }

    void Awake()
    {
        // shootPoint = transform.Find("ShootPoint").GetComponent<Transform>();
        playerTargerPoint = GameObject.FindGameObjectWithTag("PlayerTargetPoint").GetComponent<Transform>();
        playerTrans = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public void Attack()
    {
        TornadoScript tornadoObj = Instantiate(tornado, transform.position, Quaternion.identity) as TornadoScript;
        tornadoObj.tornado.Play();
        Vector3 shootingDirection = playerTargerPoint.transform.position - transform.position;
        tornadoObj.transform.forward = shootingDirection.normalized;
        tornadoObj.GetComponent<Rigidbody>().AddForce(shootingDirection.normalized * tornadoSpeed, ForceMode.Impulse);
    }
}
