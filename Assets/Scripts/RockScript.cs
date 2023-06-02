using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    [SerializeField] private Material rockGlowMaterial;
    [SerializeField] private Material rockMaterial;
    [SerializeField] private MeshRenderer meshRenderer;

    private bool glow = false;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    
    public void rockSwitch()
    {
        glow = !glow;
        rockGlow();
    }

    void rockGlow()
    {
        if(glow)
        {
            meshRenderer.material = rockGlowMaterial; 
        }else{
            meshRenderer.material = rockMaterial;            
        }
    }
}
