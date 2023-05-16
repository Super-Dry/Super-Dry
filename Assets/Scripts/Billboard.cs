using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    
    void Awake()
    {
        Transform cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
