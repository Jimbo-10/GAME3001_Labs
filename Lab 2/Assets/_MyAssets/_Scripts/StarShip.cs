using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class StarShip : AgentObject
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody2D rb;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start(); //explicitly invoking Start of AgentObject
        Debug.Log("Starting StarShip");

        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetPosition != null)
        {
            //Seek();
            SeekForward();
        }
        Reset();
    }

    private void Seek()
    {
        // Calculate desired velocity using kinematic seek equation
        Vector2 desiredVelocity = (TargetPosition - transform.position).normalized * movementSpeed;

        // Calculate the steering force
        // Check current velocity and only apply for differece between desired velocity and current one
        Vector2 steeringForce = desiredVelocity - rb.velocity;

        // Apply the steering force to the agent
        rb.AddForce(steeringForce);
    }

    private void SeekForward() // Always moves forward while rotate to the target.
    {
        // Calculate direction to the target
        Vector2 directionToTarget = (TargetPosition - transform.position).normalized;

        // Calculate the angle to rotate towards the target
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Smothly rotate towards the target
        float angleDifference = Mathf.DeltaAngle(targetAngle, transform.eulerAngles.z);
        float rotationStep = rotationSpeed * Time.deltaTime;
        float rotationAmount = Mathf.Clamp(angleDifference, -rotationStep, rotationStep);

        transform.Rotate(Vector3.forward, rotationAmount);

        // Move along the forward vector using rigidbody2D
        rb.velocity = transform.up * movementSpeed;
    }

    private void Reset()
    {
        if (Input.anyKeyDown)
        {
            transform.position = initialPosition;
            SeekForward();
        }
    }


}
