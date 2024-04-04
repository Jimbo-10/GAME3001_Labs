using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShip : MonoBehaviour
{
    public Rigidbody2D rb;

    public static StarShip Instance { get; private set; }
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

}
