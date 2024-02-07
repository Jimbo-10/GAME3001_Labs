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
   // [SerializeField] Transform targetEnemy;
    [SerializeField] float rotationSpeed;
    [SerializeField] float speed;

    //private Vector3 targetPosition;

    //[SerializeField] private float whiskerLength;
    //[SerializeField] private float whiskerAngle;
    //[SerializeField] private float avoidanceWeight;

    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    //public Vector3 TargetPosition
    //{
    //    get { return targetEnemy.position; }
    //    set { targetEnemy.position = value; }
    //}
    void Start()
    {
        
        rigidBody = GetComponent<Rigidbody2D>();
        //TargetPosition = targetEnemy.position;
        
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

        if (Input.GetKey(KeyCode.Alpha1))
        {
            Seek();
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            Flee();
        }


    }

    public void Seek()
    {
        
            // Calculate direction to the target.
            Vector2 directionToTarget = (spawnedEnemy.transform.position - transform.position).normalized;

            // Calculate the angle to rotate towards the target.
            float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg + 90.0f; // Note the +90 when converting from Radians.

            // Smoothly rotate towards the target.
            float angleDifference = Mathf.DeltaAngle(targetAngle, transform.eulerAngles.z);
            float rotationStep = rotationSpeed * Time.deltaTime;
            float rotationAmount = Mathf.Clamp(angleDifference, -rotationStep, rotationStep);
            transform.Rotate(Vector3.forward, rotationAmount);

            // Move along the forward vector using Rigidbody2D.
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
