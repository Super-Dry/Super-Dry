using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    public float speed = 5f;
    public float rotationSmoothTime = 0.1f;
    float rotatoinSmoothVelocity;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public bool allowToMove;

    GameObject shootingSound;
    GameObject footstepSound;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        allowToMove = true;
        shootingSound = GameObject.Find("ShootingSound");
        footstepSound = GameObject.Find("Footsteps");
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        if(direction.magnitude >= 0.1f && allowToMove)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angel = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotatoinSmoothVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angel, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            animator.SetBool("isWalking", true);
            footstepSound.SetActive(true);
        }else{
            animator.SetBool("isWalking", false);
            footstepSound.SetActive(false);
        }
        transform.rotation = cam.transform.rotation;
        // transform.rotation = Quaternion.Euler(0, cam.transform.rotation.y, cam.transform.rotation.z);                
        // transform.rotation = Quaternion.Euler(0, cam.transform.rotation.y, cam.transform.rotation.z);

        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer && allowToMove)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
//            animator.SetBool("isShooting", true);
        }else{
//            animator.SetBool("isShooting", false);
        }

        if(Input.GetButton("Fire1") && allowToMove){
            animator.SetBool("isShooting", true);
            shootingSound.SetActive(true);
        }else{
            animator.SetBool("isShooting", false);
            shootingSound.SetActive(false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        // animator.SetBool("isWalking", Input.GetAxisRaw("Vertical") != 0);
    }
}
