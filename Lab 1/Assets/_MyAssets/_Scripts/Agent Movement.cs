using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private Camera m_mainCamera;
    [SerializeField] private Vector3 m_targetPosition;
    [SerializeField] private float m_speed = 5f;
    [SerializeField] private GameObject island;
    private float distance;

    public static AgentMovement s_instance;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            m_targetPosition = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
            m_targetPosition.z = 0;
            LookAt2D(m_targetPosition);
        }

        transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, m_speed * Time.deltaTime);

        distance = Vector2.Distance(transform.position, island.transform.position);

        if (distance < 1f)
        {
            SceneManager.LoadScene("Scenes/EndScene");
        }
    }

    void LookAt2D(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
