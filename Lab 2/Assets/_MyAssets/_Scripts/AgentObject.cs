using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentObject : MonoBehaviour
{
    // A parent class of all the parent objects
    [SerializeField] private Transform m_target;

    public Vector3 TargetPosition
    {
        get { return m_target.position;}
        set { m_target.position = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
       Debug.Log("Starting Agent...");
       TargetPosition = m_target.position;
    }

    
    
}
