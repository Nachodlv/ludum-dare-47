using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Graviting : MonoBehaviour
{
    [SerializeField] private float gravityConstant = 1f; // 9.8f

    public float GravityConstant
    {
        get => gravityConstant;
        set => gravityConstant = value;
    }

    private Rigidbody2D _rigidbody2D;
    private Vector2 _gravityVector;
    private float _distanceToCenter;

    // Start is called before the first frame update
    void Start() {
        _distanceToCenter = transform.position.magnitude;
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        float gravityMultiplier = 1;
        if (transform.position.magnitude < _distanceToCenter * 0.8) {
            gravityMultiplier = 2;
        }
        _gravityVector = gameObject.transform.position.normalized * (gravityConstant * gravityMultiplier);
    }

    private void FixedUpdate()
    {
        // Assuming that the center of gravity is placed in (0, 0, 0)
        // This won't work if the GameObject is exactly in (0, 0, 0)
        _rigidbody2D.AddForce(_gravityVector);
    }
}

