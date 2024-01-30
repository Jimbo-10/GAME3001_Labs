using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starship : AgentObject
{
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;

    // Add fields for whisker length, angle and avoidance weight.
    [SerializeField] private float whiskerLength;
    [SerializeField] private float whiskerAngle;
    [SerializeField] private float avoidanceWeight;
    //
    //
    private Rigidbody2D rb;

    new void Start() // Note the new.
    {
        base.Start(); // Explicitly invoking Start of AgentObject.
        Debug.Log("Starting Starship.");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (TargetPosition != null)
        {
            // Seek();
            SeekForward();
            // Add call to AvoidObstacles.
            //
            AvoidObstacles();
        }
    }

    private void AvoidObstacles()
    {
        // Cast whiskers to detect obstacles.
        bool hitLeft = CastWiskers(whiskerAngle, Color.red);
        bool hitRight = CastWiskers(-whiskerAngle, Color.blue);
        bool hitleftCorner = CastWiskers(whiskerAngle * 2, Color.magenta);
        bool hitrightCorner = CastWiskers(-whiskerAngle * 2, Color.cyan);
        //

        // Adjust rotation based on detected obstacles.
        //
        if (hitLeft || hitleftCorner)
        {
            // rotate clockwise direction
            RotateClockWise();

        }
        else if (hitRight && !hitLeft)
        {
            // rotate counter clockwise
            RotateCounterClockWise();
        }
        else if (hitrightCorner && !hitleftCorner)
        {
            RotateCounterClockWise();
        }
    }

    private void RotateClockWise()
    {
        // Rotate clockwise based on rotation speed
        transform.Rotate(Vector3.forward, - rotationSpeed * avoidanceWeight * Time.deltaTime);
    }

    private void RotateCounterClockWise()
    {
        // Rotate clockwise based on rotation speed
        transform.Rotate(Vector3.forward, rotationSpeed * avoidanceWeight * Time.deltaTime);
    }

    private bool CastWiskers(float angle, Color color)
    {
        bool hitResult = false;
        Color rayColor = color;

        // Calculate direction of the whiskers
        Vector2 whiskerDirection = Quaternion.Euler(0, 0, angle) * transform.up;

        // Cast a ray in the whisker direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, whiskerDirection,whiskerLength);

        // Check if the ray hits the obstacle
        if (hit.collider != null)
        {
            Debug.Log("Obstacle detected!");
            rayColor = Color.green;
            hitResult = true;
        }

        Debug.DrawRay(transform.position, whiskerDirection * whiskerLength, rayColor);

        return hitResult;
    }

    private void RotateCounterClockwise()
    {
        // Rotate counterclockwise based on rotationSpeed and a weight.
        // 
    }

    private void RotateClockwise()
    {
        // Rotate clockwise based on rotationSpeed and a weight.
        // 
    }

    // Add CastWhisker method. I removed it entirely.
    //
    //
    //
    //

    private void SeekForward() // A seek with rotation to target but only moving along forward vector.
    {
        // Calculate direction to the target.
        Vector2 directionToTarget = (TargetPosition - transform.position).normalized;

        // Calculate the angle to rotate towards the target.
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg + 90.0f; // Note the +90 when converting from Radians.

        // Smoothly rotate towards the target.
        float angleDifference = Mathf.DeltaAngle(targetAngle, transform.eulerAngles.z);
        float rotationStep = rotationSpeed * Time.deltaTime;
        float rotationAmount = Mathf.Clamp(angleDifference, -rotationStep, rotationStep);
        transform.Rotate(Vector3.forward, rotationAmount);

        // Move along the forward vector using Rigidbody2D.
        rb.velocity = transform.up * movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Target")
        {
            GetComponent<AudioSource>().Play();
            // What is this!?! Didn't you learn how to create a static sound manager last week in 1017?
        }
    }
}
