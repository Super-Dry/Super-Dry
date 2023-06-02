using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TornadoScript : MonoBehaviour
{
    [SerializeField] public ParticleSystem tornado;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] private WizardAttack wizardAttack;

    protected bool letPlay = false;

    void Awake()
    {
        tornado = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        wizardAttack = GameObject.FindWithTag("Wizard").GetComponent<WizardAttack>();
        
        audioSource.Stop();
        tornado.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        CactusGuy player = other.gameObject.GetComponent<CactusGuy>();
        if(player != null)
        {
            Destroy(gameObject);
            // print("Player got hit by enemy");
            player.TakeDamage(wizardAttack.damage);
        }else if(other.gameObject.tag == "Obstacle"){
            Destroy(gameObject);
        }

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
