using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private CheckpointManager checkpointManager;

    private float _spawnOffset = 0.5f; // An offset used so that Racers are not spawned inside the collider detected.

    private float _angleOffset = 2f; // Used to spawn the Racer a bit ahead of the Checkpoint.

    public void SpawnInCheckpoint(Racer racer)
    {
        
        Checkpoint checkpoint = checkpointManager.GetCheckpoint(racer);
        if (checkpoint != null)
        {
            float angle = checkpoint.GetCheckpointAngle() + _angleOffset;
            
            Vector2 vector = VectorFromAngle(angle);
            racer.transform.position = vector * CalculateDistanceToSpawnPoint(vector);
        }
    }
    
    private Vector2 VectorFromAngle(float angle)
    {
        return Quaternion.Euler(0,0,angle) * Vector2.down;
    }

    private float CalculateDistanceToSpawnPoint(Vector2 vector)
    {
        RaycastHit2D hit = Physics2D.Raycast(Vector2.zero, vector);
        return hit.distance - _spawnOffset; // It should always hit something
    }
}
