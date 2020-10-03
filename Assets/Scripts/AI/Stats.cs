using System;
using UnityEngine;

namespace Entities.Enemy.Ai
{
    [Serializable]
    public class Stats
    {
        [SerializeField] private float speed;

        public float Speed => speed;
    }
}
