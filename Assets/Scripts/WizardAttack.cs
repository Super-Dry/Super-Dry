using System;
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
    [SerializeField] private BossBattle bossBattle;
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
        if(bossBattle.stage == BossBattle.Stage.Stage_1){
            StartCoroutine(stage1AttackRoutine());
        }else if(bossBattle.stage == BossBattle.Stage.Stage_3){
            StartCoroutine(stage3AttackRoutine());
        }        
    }

    IEnumerator stage3AttackRoutine()
    {
        //TODO: stage 3 attack 
        yield return null;
    }

    IEnumerator stage1AttackRoutine()
    {
        TornadoScript tornadoObj = Instantiate(tornado, transform.position, Quaternion.identity) as TornadoScript;
        tornadoObj.tornado.Play();
        tornadoObj.audioSource.Play();

        Vector3 shootingDirection = playerTrans.transform.position - transform.position;
        tornadoObj.transform.right = shootingDirection.normalized;
        tornadoObj.GetComponent<Rigidbody>().AddForce(shootingDirection.normalized * tornadoSpeed, ForceMode.Impulse);
        tornadoObj.transform.forward = Vector3.up;
        Destroy(tornadoObj, 8f);

        yield return new WaitForSeconds(0.2f);

        TornadoScript tornadoObj2 = Instantiate(tornado, transform.position, Quaternion.identity) as TornadoScript;
        tornadoObj2.tornado.Play();
        tornadoObj2.audioSource.Play();

        Vector3 shootingDirection2 = playerTrans.transform.position - transform.position;
        tornadoObj2.transform.right = shootingDirection2.normalized;
        tornadoObj2.GetComponent<Rigidbody>().AddForce(shootingDirection2.normalized * tornadoSpeed, ForceMode.Impulse);
        tornadoObj2.transform.forward = Vector3.up;
        Destroy(tornadoObj2, 8f);
        yield return null;
    }
}
