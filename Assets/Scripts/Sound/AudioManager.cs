using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager: MonoBehaviour
    {
        [SerializeField] private int audioSourceQuantity;
        [SerializeField] private AudioSourcePooleable audioSourcePrefab;

        public static AudioManager instance;

        private AudioSource _audioSource;
        private ObjectPooler<AudioSourcePooleable> _pooler;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
            _pooler = new ObjectPooler<AudioSourcePooleable>();
            _pooler.InstantiateObjects(audioSourceQuantity, audioSourcePrefab, "Audio Sources");
        }


        public void PlaySound(SoundWithSettings soundWithSettings)
        {
            var audioSource = _pooler.GetNextObject();
            audioSource.SetSpatialBlend(0);
            SetUpAudioSource(audioSource, soundWithSettings);

        }

        public void PlaySound(SoundWithSettings soundWithSettings, Vector2 position)
        {
            var audioSource = _pooler.GetNextObject();
            audioSource.SetSpatialBlend(1);
            audioSource.Transform.position = position;
            SetUpAudioSource(audioSource, soundWithSettings);
        }

        private void SetUpAudioSource(AudioSourcePooleable audioSource, SoundWithSettings soundWithSettings)
        {
            audioSource.SetClip(soundWithSettings.audioClip);
            audioSource.SetVolume(soundWithSettings.volume);
            audioSource.StartClip();
        }

    }
}
