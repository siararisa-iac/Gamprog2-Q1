using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float speed = 5.0f;

    //[SerializeField]
    //private int updateCount, fixedUpdateCount;

    private float xMove, zMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        //Get the input values from the player
        xMove = Input.GetAxis("Horizontal") * speed;
        zMove = Input.GetAxis("Vertical") * speed;
    }

    private void FixedUpdate()
    {
        //Change the velocity of our player
        //rb.velocity = new Vector3(xMove, rb.velocity.y, zMove);
        Vector3 movement = (transform.forward * zMove) + (transform.right * xMove);
        rb.velocity = movement;
    }
}
