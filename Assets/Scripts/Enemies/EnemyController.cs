using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    private Transform player;
    private EnemyState currentState;

   

    [Header("AI Settings")]
    [Header("Idle")]
    [SerializeField] private float minIdleTime = 1.0f, maxIdleTime = 5.0f;
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float roamDistance = 10.0f;
    [SerializeField] private float stoppingDistance = 1.0f;
    [Header("Chase")]
    [SerializeField] private float chaseDistance = 5.0f;

    private float idleTime = 0;
    private Vector3 randomTargetPosition;
    private float distanceFromPlayer;

    private void Start()
    {
        // This is a VERY slow function so as much as possible, avoid this
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Set the default state of the enemy
        currentState = EnemyState.Idle;
        // Set the initial value of the idleTime
        idleTime = Random.Range(minIdleTime, maxIdleTime);
    }

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position,
            player.position);
        if(distanceFromPlayer <= chaseDistance)
        {
            currentState = EnemyState.Chase;
        }

        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    private float elapsedTime;
    // Just wait for a random duration
    private void Idle()
    {
        // Behavior
        //Increase the timer
        elapsedTime += Time.deltaTime;
        //Did our timer exceed the idleTime? -> Transition Rule to next state
        if(elapsedTime >= idleTime)
        {
            //Generate a new idleTime
            idleTime = Random.Range(minIdleTime, maxIdleTime);
            //Reset the timer
            elapsedTime = 0;
            //After waiting for a certain time, we go to a different state
            currentState = EnemyState.Patrol;
        }
    }

    // Walk around in a random direction
    private void Patrol()
    {
        //Initialize our target position
        if(randomTargetPosition == Vector3.zero)
        {
            randomTargetPosition = GetRandomPosition();
        }

        //Keep track of the distance tothe target position -> Behavior
        float distanceToTarget = Vector3.Distance(transform.position, 
            randomTargetPosition);
        //Make the enemy move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, 
            randomTargetPosition,
            moveSpeed * Time.deltaTime);
        //Make sure the enemy is facing the direction
        transform.LookAt(randomTargetPosition);

        //Transition
        if(distanceToTarget <= stoppingDistance)
        {
            //Randomize our targetPosition for the next entry to Patrol
            randomTargetPosition = GetRandomPosition();
            //Go back to the idle state when reaching the target
            currentState = EnemyState.Idle;
        }

    }

    private Vector3 GetRandomPosition()
    {
        float randomX = transform.position.x + Random.Range(-roamDistance, roamDistance);
        float randomZ = transform.position.z + Random.Range(-roamDistance, roamDistance);
        //Use the current y position of the enemy
        return new Vector3(randomX, transform.position.y, randomZ);
    }

    // Follow the player when spotted
    private void Chase()
    {
        // Follow the player -> Behavior
        Vector3 targetPosition = player.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 
            moveSpeed * Time.deltaTime);
        transform.LookAt(targetPosition);

        // Transition
        if(distanceFromPlayer > chaseDistance)
        {
            currentState = EnemyState.Idle;
        }
    }

    // Attack the player
    private void Attack()
    {

    }
}
