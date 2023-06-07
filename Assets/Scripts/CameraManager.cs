using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCameraBase killCam;
    [SerializeField] private Cinemachine.CinemachineVirtualCameraBase aimCam;
    [SerializeField] private Cinemachine.CinemachineVirtualCameraBase thirdPersonCam;

    bool Aiming;
    [SerializeField] private bool allowButtonHold;

    void Awake()
    {
        killCam.gameObject.SetActive(false);
    }

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
            aimCam.gameObject.SetActive(true);
        }
        else
        {
            aimCam.gameObject.SetActive(false);
        }
    }

    public void EnableKillCam()
    {
        killCam.gameObject.SetActive(true);
    }
}
