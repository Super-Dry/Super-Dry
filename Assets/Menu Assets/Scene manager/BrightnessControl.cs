using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static BrightnessMatch;
public class BrightnessControl : MonoBehaviour
{
    // private float brightnessValue = 1f;
    public Volume postProcessingVolume;
    private ColorAdjustments colorGrading;
    public Slider slider;
    
    void Start()
    {
        postProcessingVolume = GetComponent<Volume>();
        postProcessingVolume.profile.TryGet(out colorGrading);
        if(brightnessValue == default(float))
        {
            slider.value = 2.5f;
            colorGrading.postExposure.value = 0;
        }
        else
        {
            slider.value = brightnessValue + 2.5f;    
            colorGrading.postExposure.value = brightnessValue;
        }       
    }


    // Update is called once per frame
    // void Update()
    // {

    //      // Adjust the brightness of all materials in the scene based on the current value of the slider
    //     foreach (Renderer renderer in FindObjectsOfType<Renderer>())
    //     {
    //         foreach (Material material in renderer.materials)
    //         {
    //             material.SetColor("_EmissionColor", new Color(brightnessValue, brightnessValue, brightnessValue, 1));
    //         }
    //     }
    //     if(brightnessValue < 0.5)
    //     {
    //         Debug.Log(brightnessValue);
    //     }
    // }

    // Function to be called by the On Value Changed event of the slider
    public void SetBrightness(float value)
    {
        if(value != default(float))
        {
            colorGrading.postExposure.value = value - 2.5f;
            brightnessValue = value - 2.5f;
        }
    }
}
