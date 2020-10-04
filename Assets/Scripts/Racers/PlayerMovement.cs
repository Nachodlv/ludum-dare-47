using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour {
    public enum RotationState {
        LowerLeft,
        UpperLeft,
        UpperRight,
        LowerRight
    }

    private struct Corner {
        public readonly ParticleSystem Particles;
        public readonly Transform Transform;
        public Vector2 Target;
        public Corner(Transform transform,ParticleSystem particles, Vector2 target) {
            Target = target;
            Transform = transform;
            Particles = particles;
        }
    }

    private Corner _corner1;
    private Corner _corner2;

    public float offsetDiagonal;
    public float offsetNonDiagonal;
    public float forwardForce;
    public float fromAboveForce;
    
    public bool secondJump = true;
    
    public RotationState rotationState;
    public bool Enabled { get; set; }
    private float _distToGround;

    public bool isGrounded = false;
    private Rigidbody2D _rigidBody2D;
    // Start is called before the first frame update
    void Start() {
        var corner1 = gameObject.transform.Find("corner1");
        var corner2 = gameObject.transform.Find("corner2");
        _corner1 = new Corner(corner1.transform, corner1.transform.GetComponentInChildren<ParticleSystem>(), Vector2.zero);
        _corner2 = new Corner(corner2.transform, corner2.transform.GetComponentInChildren<ParticleSystem>(), Vector2.zero);
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _distToGround = GetComponent<Collider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;
        _CalculateRotationState();
        _CalculateAngle();

        var position1 = _corner1.Transform.position;
        Debug.DrawLine(
            position1,
            position1 + new Vector3(_corner1.Target.x, _corner1.Target.y),
            Color.red
        );
        var position2 = _corner2.Transform.position;
        Debug.DrawLine(
            position2,
            position2 + new Vector3(_corner2.Target.x, _corner2.Target.y),
            Color.green
        );
        isGrounded = IsGrounded();
        if (isGrounded || secondJump) {
                        
            if (Input.GetKeyDown(KeyCode.A)) {
                secondJump = isGrounded;
                _rigidBody2D.AddForceAtPosition(_corner1.Target, position1, ForceMode2D.Impulse);
                _corner1.Particles.Play();
            }

            if (Input.GetKeyDown(KeyCode.D)) {
                secondJump = isGrounded;
                _rigidBody2D.AddForceAtPosition(_corner2.Target, position2, ForceMode2D.Impulse);
                _corner2.Particles.Play();
            }
        }
    }

    public bool IsGrounded() {
        var position = transform.position;
        var direction = position.normalized;
        Debug.DrawLine(
            position,
            position + direction * (_distToGround + 2f),
            Color.black
        );
        Debug.Log(Physics2D.Raycast(position, direction, _distToGround + 2f, LayerMask.GetMask("Terrain")));
        return Physics2D.Raycast(position, direction, _distToGround + 2f, LayerMask.GetMask("Terrain"));
    }

    private void _CalculateRotationState() {
        var angle = _getAngleRelatedCircularFloor();
        if (angle < 90) {
            rotationState = RotationState.UpperRight;
        } else if (angle < 180) {
            rotationState = RotationState.LowerRight;
        } else if (angle < 270) {
            rotationState = RotationState.LowerLeft;
        } else {
            rotationState = RotationState.UpperLeft;
        }
    }

    private void _CalculateAngle() {
        var position1 = _corner1.Transform.position;
        var position2 = _corner2.Transform.position;
        switch (rotationState) {
            case RotationState.LowerLeft:
                _corner1.Target = (position2 - position1 * offsetDiagonal) * forwardForce;
                _corner2.Target = (position1 *  offsetNonDiagonal - position2) * fromAboveForce;
                break;
            case RotationState.UpperLeft:
                _corner1.Target = (position2 *  offsetNonDiagonal - position1) * fromAboveForce;
                _corner2.Target = (position1 - position2 * offsetDiagonal) * forwardForce;
                break;
            case RotationState.LowerRight:
                _corner1.Target = (position2 - position1 * offsetDiagonal) * forwardForce;
                _corner2.Target = (position1 *  offsetNonDiagonal - position2) * fromAboveForce;
                break;
            case RotationState.UpperRight:
                _corner1.Target = (position2 *  offsetNonDiagonal - position1) * fromAboveForce;
                _corner2.Target = (position1 - position2 * offsetDiagonal) * forwardForce;
                break;
        }
    }
    
    private float _getAngleRelatedCircularFloor()
    {
        var position = transform.position;
        var cornerPosition = _corner1.Transform.position;
        var firstAngle = _polarAngle(position.x, position.y);
        var centeredPosition = new Vector2(position.x - cornerPosition.x, position.y - cornerPosition.y);
        var secondAngle = _polarAngle(centeredPosition.x, centeredPosition.y);
        var resultAngle = secondAngle - firstAngle;
        return resultAngle < 0 ? 360 + resultAngle : resultAngle;
    }

    private float _polarAngle(float x, float y)  => Mathf.Atan2(x, y) * Mathf.Rad2Deg;
}
