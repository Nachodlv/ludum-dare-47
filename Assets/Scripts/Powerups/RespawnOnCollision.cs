using System;
using UnityEngine;
using Utils;

namespace DefaultNamespace.Powerups
{
    public class RespawnOnCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            var racer = other.gameObject.GetComponent<Racer>();
            if (racer.IsPlayer)
            {
                racer.SpawnInLastCheckpoint();
            }
        }
    }
}
