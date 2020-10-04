using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private CheckpointManager checkpointManager;

    private Racer[] _racers; // Remove

    private float _spawnOffset = 0.5f; // An offset used so that Racers are not spawned inside the collider detected.

    private float _angleOffset = 2f; // Used to spawn the Racer a bit ahead of the Checkpoint.
    // Start is called before the first frame update
    void Start()
    {
        _racers = FindObjectsOfType<Racer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Remove, only for testing.
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnInCheckpoint(_racers[0]); // Remove
        }
    }

    public void SpawnInCheckpoint(Racer racer)
    {
        // We should check if the checkpoint is null (How should this be done?)
        Checkpoint checkpoint = checkpointManager.GetCheckpoint(racer);
        if (checkpoint != null)
        {
            float angle = checkpoint.GetCheckpointAngle() + _angleOffset;
            // Use a Raycast to detect any obstacles that might be between the center and the ring.
            // We should somehow calculate a distance from the center to the spawn point.
            // Can I get the distance from the Raycast?
            Vector2 vector = VectorFromAngle(angle);
            racer.transform.position = vector * CalculateDistanceToSpawnPoint(vector);
            // We should probably make the velocity 0 as well.
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
