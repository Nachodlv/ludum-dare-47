using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public enum RotationState {
        LowerLeft,
        UpperLeft,
        UpperRight,
        LowerRight
    }

    public RotationState rotationState;
    private Transform _corner1;
    private Vector2 _corner1Target;
    
    private Transform _corner2;
    private Vector2 _corner2Target;

    private float _distToGround;
    
    public bool isGrounded = false;
    private Rigidbody2D _rigidBody2D;
    // Start is called before the first frame update
    void Start()
    {
        _corner1 = GameObject.Find("corner1").transform;
        _corner2 = GameObject.Find("corner2").transform;
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _distToGround = GetComponent<Collider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update() {
        _CalculateRotationState();
        _CalculateAngle();
        
        Debug.DrawLine(_corner1.position, _corner1.position + new Vector3(_corner1Target.x, _corner1Target.y), Color.red);
        Debug.DrawLine(_corner2.position, _corner2.position + new Vector3(_corner2Target.x, _corner2Target.y), Color.green);
        
        isGrounded = IsGrounded();
        if (IsGrounded()) {
            if (Input.GetKeyDown(KeyCode.A)) {
                _rigidBody2D.AddForceAtPosition(_corner1Target, _corner1.position, ForceMode2D.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.D)) {
                _rigidBody2D.AddForceAtPosition(_corner2Target, _corner2.position, ForceMode2D.Impulse);
            }
        }

        if (transform.position.x > 50) {
            transform.Translate(-100, 0, 0, Space.World);
        }

        if (transform.position.x < -50) {
            transform.Translate(100, 0, 0, Space.World);
        }


    }
    
    public bool IsGrounded() {
        Debug.DrawLine(transform.position, transform.position + -Vector3.up * (_distToGround + 2f), Color.black);
        return Physics2D.Raycast(transform.position, -Vector3.up, _distToGround + 2f);
    }

    private void _CalculateRotationState() {
        var position1 = _corner1.position;
        var x = position1.x;
        var y = position1.y;
        var position2 = _corner2.position;
        var x2 = position2.x;
        var y2 = position2.y;
        if (x < x2) {
            if (y < y2) {
                rotationState = RotationState.LowerLeft;
            } else {
                rotationState = RotationState.UpperLeft;
            }
        } else {
            if (y < y2) {
                rotationState = RotationState.LowerRight;
            } else {
                rotationState = RotationState.UpperRight;
            }
        }
    }

    private void _CalculateAngle() {
        const float forwardForce = 15;
        const float fromAboveForce = 15;
        switch (rotationState) {
            case RotationState.LowerLeft:
                _corner1Target = new Vector2(0.65f ,1 ) * forwardForce;
                _corner2Target = new Vector2(-1f ,-0.5f ) * fromAboveForce;
                break;
            case RotationState.UpperLeft:
                _corner1Target = new Vector2(1f ,-0.5f ) * fromAboveForce;
                _corner2Target = new Vector2(-0.65f ,1 ) * forwardForce;
                break;
            case RotationState.LowerRight:
                _corner1Target = new Vector2(-0.65f ,1 ) * forwardForce;
                _corner2Target = new Vector2(1f ,-0.5f ) * fromAboveForce;
                break;
            case RotationState.UpperRight:
                _corner1Target = new Vector2(-1f ,-0.5f) * fromAboveForce;
                _corner2Target = new Vector2(0.65f ,1 )  * forwardForce;
                break;
        }
    }
}
