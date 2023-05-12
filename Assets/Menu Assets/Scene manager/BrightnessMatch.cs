using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static BrightnessControl;
public class BrightnessMatch : MonoBehaviour
{
    public  PostProcessProfile brightness;
    public  PostProcessLayer layer;
    public AutoExposure exposure;
    // Start is called before the first frame update
    void Start()
    {
        if(BrightnessControl.brightness != null && BrightnessControl.layer != null & BrightnessControl.exposure != null)
        {
            this.brightness = BrightnessControl.brightness;
            layer = BrightnessControl.layer;
            exposure = BrightnessControl.exposure;
            Debug.Log("Variables passed!\n");
        }
        Debug.Log(BrightnessControl.brightnessValue);
        PostProcessVolume brightness = GetComponent<PostProcessVolume>();
        if (brightness != null && brightness.sharedProfile != null)
        {
            brightness.sharedProfile.TryGetSettings(out exposure);
        }
        exposure.keyValue.value = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(BrightnessControl.brightnessValue != default(float))
        {
            if(BrightnessControl.brightnessValue != 0)
            {
                exposure.keyValue.value = BrightnessControl.brightnessValue;
            }
            else
            {
                exposure.keyValue.value = 0.05f;
            }
        }
    }

    public void SetBrightness(float value)
    {
        if(value != default(float))
        {
            BrightnessControl.brightnessValue = value;
            if(value > 0.05f)
            {
                BrightnessControl.exposure.keyValue.value = value;
                BrightnessControl.brightnessValue = value;
            }
            else
            {
                BrightnessControl.exposure.keyValue.value = 0.05f;
                BrightnessControl.brightnessValue = 0.05f;
            }
        }
    }
}
