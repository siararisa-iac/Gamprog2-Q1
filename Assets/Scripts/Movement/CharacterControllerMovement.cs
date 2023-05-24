using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float gravityScale = 1.0f;

    private float gravity = -9.8f;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
       float xMove = Input.GetAxis("Horizontal");
       float zMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * xMove) + (transform.forward * zMove);
        moveDirection.y += gravity * Time.deltaTime * gravityScale;
        moveDirection *= moveSpeed * Time.deltaTime;
       
        //Debug.Log(moveDirection);
        characterController.Move(moveDirection);
    }
}
