using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSmoothTime = 0.1f;
    [SerializeField] private Camera camera;
    [SerializeField] private float gravityScale = 1.0f;
    [SerializeField] private float groundedVelocity = -0.5f;
    [SerializeField] private float jumpHeight = 3f;
    private float currentAngle;
    private float currentAngleVelocity;
    private CharacterController characterController;
    private float gravity = -9.8f;
    private float yVelocity = 0;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();   
        if(camera == null)
        {
            camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //Move the player with input
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")).normalized;

        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z)
            * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle,
                targetAngle,
                ref currentAngleVelocity,
                rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0)
                * Vector3.forward * moveSpeed * Time.deltaTime;
            rotatedMovement += GetGravityAndJump();
            characterController.Move(rotatedMovement);
        }
        else
        {
            characterController.Move(GetGravityAndJump());
        }
    }

    private Vector3 GetGravityAndJump()
    {
        //Ensure that the character is kept grounded
        if (characterController.isGrounded && yVelocity < 0f)
        {
            yVelocity = groundedVelocity;
        }
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Calculate the velocity given the height
            yVelocity = Mathf.Sqrt(jumpHeight * 2.0f * Mathf.Abs(gravity));
        }
        // Keep applying gravitational force
        yVelocity += gravity * gravityScale * Time.deltaTime;
        return Vector3.up * yVelocity * Time.deltaTime;
    }
}
