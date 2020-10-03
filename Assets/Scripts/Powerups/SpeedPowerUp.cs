using System;
using UnityEngine;
using Utils;

namespace DefaultNamespace.Powerups
{
    public class SpeedPowerUp : MonoBehaviour
    {
        [SerializeField] private float power;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var rigidbody2D = other.GetComponent<Rigidbody2D>();
            if (rigidbody2D == null) return;
            rigidbody2D.AddForce(rigidbody2D.position.normalized.Rotate90CCW() * power);
        }
    }
}
