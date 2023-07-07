using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 5.0f;
    [SerializeField] private float rotationSmoothTime = 0.1f;
    [SerializeField] private Camera camera;
    [SerializeField] private float gravityScale = 1.0f;
    [SerializeField] private float groundedVelocity = -0.5f;
    [SerializeField] private float jumpHeight = 3f;
    private bool sprint = false;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    private float currentAngle;
    private float currentAngleVelocity;
    private CharacterController characterController;
    private float gravity = -9.8f;
    private float yVelocity = 0;
    private Animator animator;
    private Vector3 targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if(camera == null)
        {
            camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GroundedCheck();
        Move();
    }
    float animationBlend;
    float _speed;

    private void Move()
    {
        //Move the player with input
        Vector3 movementInput = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")).normalized;

        sprint = Input.GetKey(KeyCode.LeftShift);

        float targetSpeed = sprint ? sprintSpeed : moveSpeed;
        if (movementInput == Vector3.zero)
        {
            targetSpeed = 0;
        }

        float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0, characterController.velocity.z).magnitude;
        float speedOffset = 0.1f;
        if (currentHorizontalSpeed < targetSpeed - speedOffset|| currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * movementInput.magnitude, Time.deltaTime * SpeedChangeRate);
        }
        else
        {
            _speed = targetSpeed;
        }

        
        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (animationBlend < 0.01f) animationBlend = 0f;

        if (movementInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movementInput.x, movementInput.z)
            * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle,
                targetAngle,
                ref currentAngleVelocity,
                rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
            targetDirection = Quaternion.Euler(0, targetAngle, 0)
                * Vector3.forward;
        }

        animator.SetFloat("Speed", animationBlend);
        characterController.Move(targetDirection * (_speed * Time.deltaTime) + GetVerticalVelocity());
    }

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    private Vector3 GetVerticalVelocity()
    {
        //Ensure that the character is kept grounded
       
        if (Grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            if (yVelocity < 0f)
            {
                yVelocity = groundedVelocity;
            }

            animator.SetBool("Jump", false);
            animator.SetBool("FreeFall", false);

            if (Input.GetKeyDown(KeyCode.Space) && _jumpTimeoutDelta <= 0.0f)
            {
                // Calculate the velocity given the height
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                yVelocity = Mathf.Sqrt(jumpHeight * 2.0f * Mathf.Abs(gravity));
                animator.SetBool("Jump", true);
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("FreeFall", true);
            }
        }
        // Keep applying gravitational force
        yVelocity += gravity * gravityScale * Time.deltaTime;
        return Vector3.up * yVelocity * Time.deltaTime;
    }

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;

    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.28f;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;


    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);

        // update animator if using character
        animator.SetBool("Grounded", Grounded);
    }

    public void OnFootstep()
    {

    }

    public void OnLand()
    {

    }
}
