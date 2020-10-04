using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    [SerializeField] private Checkpoint checkpointPrefab;
    [SerializeField] private int numberOfCheckpoints;

    // Create a dictionary for storing the checkpoints of each Racer.
    private Dictionary<Racer, Checkpoint> _checkpoints = new Dictionary<Racer, Checkpoint>(); // Should we give all of them an initial checkpoint?

    void Awake()
    {
        float increment = 360f / numberOfCheckpoints;
        
        for (int i = 0; i < numberOfCheckpoints; i++)
        {
            Checkpoint checkpoint = Instantiate(checkpointPrefab, Vector2.zero, Quaternion.identity);
            checkpoint.AssignAngle(increment * i);
            checkpoint.OnRacerCheckpointReached += RegisterCheckpoint;
        }
    }

    public Checkpoint GetCheckpoint(Racer racer)
    {
        return _checkpoints[racer];
    }

    private void RegisterCheckpoint(Racer racer, Checkpoint checkpoint)
    {
        _checkpoints[racer] = checkpoint;
    }
}
