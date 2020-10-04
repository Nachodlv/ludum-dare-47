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

    private readonly string[] _names = {"Juan", "Eduardo", "Rochi"};

    private void Awake()
    {
        racerName = GetRandomName();
    }

    private string GetRandomName()
    {
        return _names[Random.Range(0, _names.Length)];
    }

    public void SpawnInLastCheckpoint()
    {
        spawner.SpawnInCheckpoint(this);
    }
}
