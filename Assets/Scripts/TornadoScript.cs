using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem tornado;
    [SerializeField] private AudioSource audioSource;
    protected bool letPlay = false;

    void Awake()
    {
        tornado = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        tornado.Stop();
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
