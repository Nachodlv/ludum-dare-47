using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graviting : MonoBehaviour
{
    [SerializeField] private float gravityConstant = 1f; // 9.8f
    
    private Rigidbody2D _rigidbody2D;

    private Vector2 _gravityVector;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _gravityVector = gameObject.transform.position.normalized * gravityConstant;
    }

    private void FixedUpdate()
    {
        // Assuming that the center of gravity is placed in (0, 0, 0)
        // This won't work if the GameObject is exactly in (0, 0, 0)
        _rigidbody2D.AddForce(_gravityVector);
    }
}
