using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject cam;
    public Transform camTransform;
    
    void Awake()
    {
        GameObject cam = GameObject.Find("Main Camera");
        if(cam != null)
        {
            camTransform = cam.GetComponent<Transform>();
        }
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + camTransform.forward);
    }
}
