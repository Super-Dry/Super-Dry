using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem tornado;
    protected bool letPlay = true;

    void Awake()
    {
        tornado = GetComponent<ParticleSystem>();
        // gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            letPlay = !letPlay;
        }
    
        if(letPlay)
        {
            if(!tornado.isPlaying)
            {
                tornado.Play();
            }
        }else{
            if(tornado.isPlaying)
            {
                tornado.Stop();
            }
        }
        
    }
}
