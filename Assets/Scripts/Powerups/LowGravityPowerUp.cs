using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Powerups
{
    public class LowGravityPowerUp : MonoBehaviour
    {
        [SerializeField] private float newGravityConstant = .5f;
        [SerializeField] private float powerUpTime = 5f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var gravityModifier = other.GetComponent<GravityModifier>();
            if (gravityModifier == null) return;
            gravityModifier.AddPowerUp(newGravityConstant, powerUpTime);
        }
    }
}
