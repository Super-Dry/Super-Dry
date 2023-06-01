using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    [SerializeField] private Material rockGlowMaterial;
    [SerializeField] private Material rockMaterial;
    [SerializeField] private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
        

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            change();
        }else if(Input.GetKeyDown(KeyCode.E))
        {
            back();
        }
    }

    private void back()
    {
        Material oldMaterial = meshRenderer.material;
        Debug.Log(oldMaterial);
        meshRenderer.material = rockMaterial;
    }

    void change()
    {
        Material oldMaterial = meshRenderer.material;
        Debug.Log(oldMaterial);
        meshRenderer.material = rockGlowMaterial;
    }
}
