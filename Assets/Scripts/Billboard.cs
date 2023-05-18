using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    
    void Awake()
    {
        Transform cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
