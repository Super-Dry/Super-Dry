using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolEffects : MonoBehaviour
{
    public float floatStrength = 1f;
    public float rotationSpeed = 100f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        //Floating
        transform.position = startPos + new Vector3(0f, Mathf.Sin(Time.time) * floatStrength, 0f);

        //Rotation
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
