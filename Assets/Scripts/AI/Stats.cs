using System;
using UnityEngine;

namespace Entities.Enemy.Ai
{
    [Serializable]
    public class Stats
    {
        [SerializeField] private float accelerationSpeed;
        [SerializeField] private float maxSpeed;

        public float AccelerationSpeed => accelerationSpeed;
        public float MaxSpeed => maxSpeed;
    }
}
