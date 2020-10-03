using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Racer : MonoBehaviour
{
    [SerializeField] private string racerName;
    public string RacerName => racerName;

    private readonly string[] _names = {"Juan", "Eduardo"};

    private void Awake()
    {
        racerName = GetRandomName();
    }

    private string GetRandomName()
    {
        return _names[Random.Range(0, _names.Length)];
    }
}
