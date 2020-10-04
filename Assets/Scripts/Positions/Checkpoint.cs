using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Checkpoint : MonoBehaviour
{

    [SerializeField] private int checkpointLength = 200;
    private float _angle;

    private EdgeCollider2D _edgeCollider2D;
    private  Vector2 _vector = Vector2.down; // How should this vector be determined?
    // We should probably have someone that creates the checkpoints and defines a vector for each one.
    
    private int _angleThreshold = 300;

    public delegate void CheckpointCallback(Racer racer, Checkpoint checkpoint);

    public event CheckpointCallback OnRacerCheckpointReached;
    
    void Awake()
    {
        _edgeCollider2D = gameObject.GetComponent<EdgeCollider2D>();
        _angle = CalculateAngle(_vector);
    }

    public void AssignAngle(float angle)
    {
        _angle = angle;
        _vector = VectorFromAngle(_angle);
        Vector2[] vectors = {Vector2.zero, _vector * checkpointLength};
        _edgeCollider2D.points = vectors;
    }

    public float GetCheckpointAngle()
    {
        return _angle;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Racer racer = other.GetComponent<Racer>();
        if (racer != null)
        {
            float racerAngle = CalculateAngle(racer.transform.position);
            if (racerAngle - _angle > _angleThreshold || _angle > racerAngle)
            {
                OnRacerCheckpointReached?.Invoke(racer, this); // Is this ok?
            }
        }
    }
    
    /**
     * Calculates the Angle between the vector provided and a Vector2 pointing down.
     */
    private float CalculateAngle(Vector2 angle)
    {
        float value = Vector2.SignedAngle(Vector2.down, angle);
        return value < 0 ? 360 + value : value;
    }
    
    private Vector2 VectorFromAngle(float angle)
    {
        return Quaternion.Euler(0,0,angle) * Vector2.down;
    }
}
