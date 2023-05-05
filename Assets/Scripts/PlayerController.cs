using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;

    [SerializeField]
    private float movementSpeed, rotationSpeed, gravity;

    private Vector3 movementDirection = Vector3.zero;
    private bool playerShooting;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //playerGrounded = characterController.isGrounded;

        //movement
        Vector3 inputMovement = transform.forward * movementSpeed * -Input.GetAxisRaw("Vertical");
        characterController.Move(inputMovement * Time.deltaTime);

        if(Input.GetButton("Horizontal")){
            transform.Rotate(Vector3.up * Input.GetAxisRaw("Horizontal") * rotationSpeed);
        }else{
            transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * rotationSpeed);
        }

        //jumping
        if(Input.GetButton("Fire1"))
        {
            animator.SetBool("isShooting", true);
        }else{
            animator.SetBool("isShooting", false);
        }
        movementDirection.y -= gravity * Time.deltaTime;

        characterController.Move(movementDirection * Time.deltaTime);




        //animations
        animator.SetBool("isWalking", Input.GetAxisRaw("Vertical") != 0);
        //animator.SetBool("isJumping", !characterController.isGrounded);

    }
}