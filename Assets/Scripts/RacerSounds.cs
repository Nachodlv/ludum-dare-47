using System;
using Sound;
using UnityEngine;

namespace DefaultNamespace
{
    public class RacerSounds : MonoBehaviour
    {
        [SerializeField] private MultipleAudios increasePositionAudio;
        [SerializeField] private MultipleAudios decreasePositionAudio;
        [SerializeField] private PositionManager positionManager;
        [SerializeField] private float timeBetweenAudios = 3f;

        private int _previousPosition = int.MaxValue;
        private float _lastAudio;
        private bool firstTime;
        private void Awake()
        {
            positionManager.OnPlayerPositionChange += NewPlayerPosition;
        }

        private void NewPlayerPosition(int position)
        {
            if (firstTime)
            {
                firstTime = false;
                return;
            }
            var now = Time.time;
            if (now - _lastAudio < timeBetweenAudios) return;
            _lastAudio = now;
            AudioManager.instance.PlaySound(_previousPosition > position
                ? increasePositionAudio.RandomSound
                : decreasePositionAudio.RandomSound);
            _previousPosition = position;
        }
    }
}
