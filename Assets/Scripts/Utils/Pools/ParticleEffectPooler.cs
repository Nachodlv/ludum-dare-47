using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Utils.Pooler;

namespace Weapons
{
    public enum ParticleType
    {
        CubeExplosion
    }

    [Serializable]
    public class ParticleReferences
    {
        public ParticleType type;
        public ParticleEffectPooleable particleEffect;
        public int quantity = 10;
    }
    public class ParticleEffectPooler: MonoBehaviour
    {
        [SerializeField] private ParticleReferences[] particles;

        public static ParticleEffectPooler Instance;

        private Dictionary<ParticleType, HotterPooler<ParticleEffectPooleable>> _pools;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            _pools = new Dictionary<ParticleType, HotterPooler<ParticleEffectPooleable>>();
            foreach (var particle in particles)
            {
                var pooler = new HotterPooler<ParticleEffectPooleable>();
                pooler.InstantiateObjects(particle.quantity, particle.particleEffect,
                    $"Pool of {particle.particleEffect.name}");
                _pools.Add(particle.type, pooler);
            }
        }

        public ParticleEffectPooleable GetParticleEffect(ParticleType type, Vector3 position = new Vector3())
        {
            var particle = _pools[type]?.GetNextObject();
            if (particle == null) return null;
            particle.transform.position = position;
            return particle;
        }

    }


}
