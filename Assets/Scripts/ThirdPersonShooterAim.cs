using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonShooterAim : MonoBehaviour
{

    [SerializeField] private CinemachineFreeLook aimVirtualCamera;
    bool Aiming;
    public bool allowButtonHold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold) Aiming = Input.GetKey(KeyCode.Mouse1);
        else Aiming = Input.GetKeyDown(KeyCode.Mouse1);

        //Shooting
        if (Aiming)
        {
            aimVirtualCamera.gameObject.SetActive(true);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);

        }
    }
}
