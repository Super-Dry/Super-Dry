using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing; 

public class BrightnessControl : MonoBehaviour
{
    // private float brightnessValue = 1f;
    public  Slider BrightnessSlider; 
    public  static PostProcessProfile brightness;
    public  static PostProcessLayer layer;

    public static AutoExposure exposure;
    public static float brightnessValue = 1;
    void Start()
    {
        
        PostProcessVolume brightness = GetComponent<PostProcessVolume>();
        if (brightness != null && brightness.sharedProfile != null && BrightnessSlider != null)
        {
            brightness.sharedProfile.TryGetSettings(out exposure);
            exposure.keyValue.value = brightnessValue;
            BrightnessSlider.value = brightnessValue;
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
            brightnessValue = value;
            if(value > 0.05f)
            {
                exposure.keyValue.value = value;
                brightnessValue = value;
            }
            else
            {
                exposure.keyValue.value = 0.05f;
                brightnessValue = 0.05f;
            }
        }
    }
}
