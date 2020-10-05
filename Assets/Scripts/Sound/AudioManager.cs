using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private int audioSourceQuantity;
        [SerializeField] private AudioSourcePooleable audioSourcePrefab;

        public static AudioManager instance;

        private AudioSource _musicSource;
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
            _musicSource = GetComponent<AudioSource>();
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

        public void SetMusicSource(SoundWithSettings music)
        {
            StartCoroutine(ChangeMusicSource(music));
        }

        private IEnumerator ChangeMusicSource(SoundWithSettings music)
        {
            yield return AudioFades.FadeOut(_musicSource, 1f);
            _musicSource.clip = music.audioClip;
            _musicSource.volume = 0;
            _musicSource.Play();
            _musicSource.loop = true;
            yield return AudioFades.FadeIn(_musicSource, 1f, music.volume);
        }

        private void SetUpAudioSource(AudioSourcePooleable audioSource, SoundWithSettings soundWithSettings)
        {
            audioSource.SetClip(soundWithSettings.audioClip);
            audioSource.SetVolume(soundWithSettings.volume);
            audioSource.StartClip();
        }
    }
}
