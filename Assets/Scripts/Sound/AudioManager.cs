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

        private AudioSource _audioSource;
        private ObjectPooler<AudioSourcePooleable> _pooler;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _pooler = new ObjectPooler<AudioSourcePooleable>();
            _pooler.InstantiateObjects(audioSourceQuantity, audioSourcePrefab, "Audio Sources");
        }


        public void PlaySound(SoundWithSettings soundWithSettings)
        {
            var audioSource = _pooler.GetNextObject();
            audioSource.SetClip(soundWithSettings.audioClip);
            audioSource.SetVolume(soundWithSettings.volume);
            audioSource.StartClip();
        }

    }
}
