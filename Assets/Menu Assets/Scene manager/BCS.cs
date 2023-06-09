using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCS : MonoBehaviour
{
    private float brightnessMultiplier = 1f;
    private bool brightnessChanged = false;
    void Start()
    {
        // Get all renderers in the scene
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        // Modify the brightness of each renderer's materials
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                // Adjust the material's color by the brightness multiplier
                material.color *= brightnessMultiplier;
            }
        }
    }

    void Update()
    {
        if(brightnessChanged)
        {
            // Get all renderers in the scene
            Renderer[] renderers = FindObjectsOfType<Renderer>();

            // Modify the brightness of each renderer's materials
            foreach (Renderer renderer in renderers)
            {
                foreach (Material material in renderer.materials)
                {
                    // Adjust the material's color by the brightness multiplier
                    material.color *= brightnessMultiplier;
                }
            }
            brightnessChanged = false;
        }
    }


    public void SetBrightness(float bright)
    {
        brightnessMultiplier = bright;
        brightnessChanged = true;
    }



}