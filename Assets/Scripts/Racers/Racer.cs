using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Racer : MonoBehaviour
{
    [SerializeField] private string racerName;
    [SerializeField] private bool isPlayer;
    [SerializeField] private Spawner spawner;

    public bool IsPlayer => isPlayer;
    public string RacerName => racerName;
    

    public void SpawnInLastCheckpoint()
    {
        spawner.SpawnInCheckpoint(this);
    }
}
