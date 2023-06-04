using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBarScript : MonoBehaviour
{
    public Slider slider;
    public Transform target;
    public Transform waterSource;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = waterSource.GetComponent<WaterScript>().totalShieldPointsRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            transform.LookAt(target);
        }

        // Debug.Log("updating");
        slider.value = waterSource.GetComponent<WaterScript>().totalShieldPointsRemaining;
    }
}
