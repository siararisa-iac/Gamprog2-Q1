using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private float walkSpeed = 2.0f;
    [SerializeField]
    private float sprintSpeed = 5.0f;
    [SerializeField]
    private float rotationSmoothTime = 0.1f;

    private Animator animator;
    
    private CharacterController controller;
    private float gravity = -9.8f;

    private float currentSpeed;
    private float targetSpeed;
    private float currentAngleVelocity;
    private float currentAngle;

    private bool isRunning;

    private void Awake()
    {
        // Get the reference to the attached character controller Component
        controller = GetComponent<CharacterController>();
        // Get the reference to the attached animator Component
        animator = GetComponent<Animator>();
        // automatically set up reference to the camera to avoid null reference error
        if(camera == null)
        {
            camera = Camera.main;
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movementInput = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical"));

        // Store boolean to check if the shift has been pressed
        isRunning = Input.GetKey(KeyCode.LeftShift);

        targetSpeed = isRunning ? sprintSpeed : walkSpeed;

       
        if(movementInput == Vector3.zero)
        {
            // Set speed to 0 when there is no input
            targetSpeed = 0;
        }

        currentSpeed = targetSpeed;
        // The momvementInput determines whether we are making the character move so we will
        // use it for the animator
        animator.SetFloat("moveSpeed", movementInput.magnitude);
        animator.SetBool("isRunning", isRunning);
        // Make the player move
        if(movementInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movementInput.x, movementInput.z) 
                // Make sure to convert the Atan2 (radians) to degreees
                * Mathf.Rad2Deg
                // Make sure to add the Camera's angle to respect the direction of the camera
                + camera.transform.eulerAngles.y;
            // Apply a smoothing to the angle using the Mathf.SmoothDampAngle function
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            //Change the rotation angle of the player to use the smooth angle value
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);

            // To move in the rotated direction considering the camera angle
            // We need to multiply the target angle with the forward direction
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(rotatedMovement * currentSpeed * Time.deltaTime);
        }
    }
}
