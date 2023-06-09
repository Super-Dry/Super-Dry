using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BrightnessMatch : MonoBehaviour
{
    public static float brightnessValue;
    public  Slider slider; 
    public Volume postProcessingVolume;
    private ColorAdjustments colorGrading;

    // Start is called before the first frame update
    void Start()
    {
        postProcessingVolume = GetComponent<Volume>();
        postProcessingVolume.profile.TryGet(out colorGrading);
        if(slider != null)
        {
            if(brightnessValue != default(float))
            {
                slider.value = brightnessValue + 2.5f;
                colorGrading.postExposure.value = brightnessValue + 2.5f;
            }
            else
            {
                brightnessValue = 0;
                slider.value = 2.5f;
                colorGrading.postExposure.value = 0;
            }
        }
        else{
            if(brightnessValue != default(float))
            {
                colorGrading.postExposure.value = brightnessValue + 2.5f;
            }
            else
            {
                brightnessValue = 0;
                colorGrading.postExposure.value = 0;
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(brightnessValue != default(float))
        {
            colorGrading.postExposure.value = brightnessValue;
        }
    }

    public void SetBrightness(float value)
    {
        if(value != default(float))
        {
            colorGrading.postExposure.value = value - 2.5f;
            brightnessValue = value - 2.5f;
        }
    }
}
