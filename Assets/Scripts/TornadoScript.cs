using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TornadoScript : MonoBehaviour
{
    [SerializeField] public ParticleSystem tornado;
    [SerializeField] private AudioSource audioSource;
    protected bool letPlay = false;

    private NavMeshAgent agent;
    private Transform playerTrans;

    void Awake()
    {
        tornado = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        
        audioSource.Stop();
        // tornado.Stop();

        // agent = GetComponent<NavMeshAgent>();
        playerTrans = GameObject.FindWithTag("Player").GetComponent<Transform>();
        // Vector3 spawnLocation = transform.position;
        // NavMeshHit hit;
        // if(NavMesh.SamplePosition(spawnLocation, out hit, 5.0f, NavMesh.AllAreas)){
        //     agent.updateRotation = false;
        //     agent.Warp(hit.position);
        //     // transform.position = hit.position;
        //     agent.enabled = true;
        // }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            attack();
        }
    }

    void attack()
    {
        // agent.SetDestination(playerTrans.position);
        Vector3 shootingDirection = playerTrans.transform.position - transform.position;
        gameObject.transform.right = shootingDirection.normalized;
        gameObject.GetComponent<Rigidbody>().AddForce(shootingDirection.normalized * 5, ForceMode.Impulse);
        transform.forward = Vector3.up; 
    }

    public void tornadoSwitch()
    {
        letPlay = !letPlay;
        tornadoStartStop();
    }

    void tornadoStartStop()
    {
        if(letPlay)
        {
            if(!tornado.isPlaying)
            {
                audioSource.Play();
                tornado.Play();
            }
        }else{
            if(tornado.isPlaying)
            {   
                audioSource.Stop();
                tornado.Stop();
            }
        }
    }
}
