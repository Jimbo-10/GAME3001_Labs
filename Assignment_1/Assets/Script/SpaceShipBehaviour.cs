using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpaceShipBehaviour : MonoBehaviour
{
    
   // [SerializeField] EnemyBehavoiur _enemyBehaviour;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject spaceShip;
    private GameObject spawnedEnemy;
    private GameObject spawnedSpaceShip;
    private float slowingDistance = 2.2f;
   
    [SerializeField] float rotationSpeed;
    [SerializeField] float speed;
    [SerializeField] private float whiskerLength;
    [SerializeField] private float whiskerAngle;
    [SerializeField] private float avoidanceWeight;

    Rigidbody2D rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            spawnedEnemy = Instantiate(enemy, new Vector3(5.52f, 2.49f, 0f), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // spawnedSpaceShip = Instantiate(spaceShip, new Vector3(-0.83f, -0.74f, 0f), Quaternion.identity);
            spaceShip.transform.position = new Vector3(-0.83f, -0.74f, 0f);
            spawnedEnemy = Instantiate(enemy, new Vector3(0.83f, 0.73f, 0f), Quaternion.identity);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            spawnedEnemy = Instantiate(enemy, new Vector3(5.52f, 2.49f, 0f), Quaternion.identity);
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            Seek();
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            Flee();
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            Arrival();
        }
    }

    public void Seek()
    {
            Vector2 directionToTarget = (spawnedEnemy.transform.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg + 90.0f; 
            float angleDifference = Mathf.DeltaAngle(targetAngle, transform.eulerAngles.z);
            float rotationStep = rotationSpeed * Time.deltaTime;
            float rotationAmount = Mathf.Clamp(angleDifference, -rotationStep, rotationStep);
            transform.Rotate(Vector3.forward, rotationAmount);

            rigidBody.velocity = transform.up * speed;   
    }

    public void Flee()
    {
        
        Vector2 targetDirection = (spaceShip.transform.position - spawnedEnemy.transform.position).normalized;
        float angleOfTarget = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg + 90.0f;
        float angleDifference = Mathf.DeltaAngle(angleOfTarget, transform.eulerAngles.z);
        float rotationStep = rotationSpeed * Time.deltaTime;
        float rotationAmount = Mathf.Clamp(angleDifference, -rotationStep, rotationStep);
        transform.Rotate(Vector3.forward, rotationAmount);

        rigidBody.velocity = transform.up * speed;
    }

    public void Arrival()
    {
       
        Vector2 directionToTarget = (spawnedEnemy.transform.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg + 90.0f;
        float angleDifference = Mathf.DeltaAngle(targetAngle, transform.eulerAngles.z);
        float rotationStep = rotationSpeed * Time.deltaTime;
        float rotationAmount = Mathf.Clamp(angleDifference, -rotationStep, rotationStep);
        transform.Rotate(Vector3.forward, rotationAmount);

        
        float distance = Vector2.Distance(transform.position, spawnedEnemy.transform.position);
        Vector2 desiredVelocity;
        if (distance < slowingDistance)
        {
            desiredVelocity = directionToTarget * speed * (distance/slowingDistance);
            rigidBody.velocity = transform.up * desiredVelocity;
        }
        else
            desiredVelocity = directionToTarget * speed;
            rigidBody.velocity = transform.up * desiredVelocity;
    }

    //private void AvoidObstacles()
    //{

    //    bool hitLeft = CastWiskers(whiskerAngle, Color.red);
    //    bool hitRight = CastWiskers(-whiskerAngle, Color.blue);
    //    bool hitleftCorner = CastWiskers(whiskerAngle * 2, Color.magenta);
    //    bool hitrightCorner = CastWiskers(-whiskerAngle * 2, Color.cyan);

    //    if (hitLeft || hitleftCorner)
    //    {
    //        RotateClockWise();
    //    }

    //    else if (hitRight && !hitLeft)
    //    {
    //        RotateCounterClockWise();
    //    }

    //    else if (hitrightCorner && !hitleftCorner)
    //    {
    //        RotateCounterClockWise();
    //    }
    //}

    //private void RotateClockWise()
    //{
    //    transform.Rotate(Vector3.forward, -rotationSpeed * avoidanceWeight * Time.deltaTime);
    //}

    //private void RotateCounterClockWise()
    //{
    //    transform.Rotate(Vector3.forward, rotationSpeed * avoidanceWeight * Time.deltaTime);
    //}
    //private bool CastWiskers(float angle, Color color)
    //{
    //    bool hitResult = false;
    //    Color rayColor = color;


    //    Vector2 whiskerDirection = Quaternion.Euler(0, 0, angle) * transform.up;


    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, whiskerDirection, whiskerLength);


    //    if (hit.collider != null)
    //    {
    //        Debug.Log("Obstacle detected!");
    //        rayColor = Color.green;
    //        hitResult = true;
    //    }

    //    Debug.DrawRay(transform.position, whiskerDirection * whiskerLength, rayColor);

    //    return hitResult;
    //}
}
