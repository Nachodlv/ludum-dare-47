using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class GravityModifier : MonoBehaviour
    {
        [SerializeField] private Graviting graviting;

        private List<GravityPowerUp> _powerUps;
        private Coroutine _affectingPowerUpsCoroutine;
        public ParticleSystem particles;

        private void Awake()
        {
            _powerUps = new List<GravityPowerUp>();
        }

        public void AddPowerUp(float gravityModifier, float totalTime)
        {
            var gravityPowerUp = new GravityPowerUp
            {
                gravityModifier = gravityModifier, totalTime = totalTime, startingTime = Time.time,
                lastValue = graviting.GravityConstant
            };
            _powerUps.Add(gravityPowerUp);
            graviting.GravityConstant = gravityPowerUp.gravityModifier;
            if (_affectingPowerUpsCoroutine != null) StopCoroutine(_affectingPowerUpsCoroutine);
            _affectingPowerUpsCoroutine = StartCoroutine(AffectingPowerUps());
        }

        private IEnumerator AffectingPowerUps()
        {
            particles.Play();
            while (_powerUps.Count > 0)
            {
                var gravityPowerUp = _powerUps.Last();
                var now = Time.time;
                var waitingTime = gravityPowerUp.totalTime - (now - gravityPowerUp.startingTime);
                if(waitingTime > 0) yield return new WaitForSeconds(waitingTime);
                graviting.GravityConstant = gravityPowerUp.lastValue;
                _powerUps.Remove(gravityPowerUp);
                yield return null;
            }
            particles.Stop();
        }
    }


    [Serializable]
    public struct GravityPowerUp
    {
        public float gravityModifier;
        public float startingTime;
        public float totalTime;
        public float lastValue;
    }
}
