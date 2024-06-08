using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private float moveSpeed = 3.0f;
    private void Start()
    {
        // This is a VERY slow function so as much as possible, avoid this
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Follow the player
        Vector3 targetPosition = player.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.LookAt(targetPosition);
    }
}
