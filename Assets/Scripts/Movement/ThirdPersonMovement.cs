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
    [SerializeField]
    private float gravityScale = 1.0f;
    private float currentAngle;
    private float currentAngleVelocity;
    private CharacterController characterController;
    private float gravity = -9.8f;
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
        //Move the player with input
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")).normalized;

        if(movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) 
                * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle,
                targetAngle,
                ref currentAngleVelocity,
                rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) 
                * Vector3.forward;
            rotatedMovement.y += gravity * gravityScale;
            characterController.Move(rotatedMovement * 
                moveSpeed * Time.deltaTime);
        }
    }
}
