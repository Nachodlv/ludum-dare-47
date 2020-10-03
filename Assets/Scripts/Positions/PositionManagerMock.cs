using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Positions
{
    public class PositionManagerMock : MonoBehaviour, IPositionManager
    {
        [SerializeField] private GameMode gameMode;
        public event PositionCallback OnPlayerPositionChange;
        public event PositionCallback OnPlayerFinishRace;
        public event LapCallback OnPlayerFinishLap;

        private void Awake()
        {
            ChangePlayerPositionRandom();
            ChangeLapRandom();
        }

        public List<Racer> GetRacersPositions()
        {
            return FindObjectsOfType<Racer>().ToList();
        }

        public void ChangePlayerPositionRandom()
        {
            OnPlayerPositionChange?.Invoke(Random.Range(1, 10));
            Invoke(nameof(ChangePlayerPositionRandom), Random.Range(1, 5));
        }

        public void ChangeLapRandom()
        {
            OnPlayerFinishLap?.Invoke(Random.Range(1, 4));
            Invoke(nameof(ChangeLapRandom), Random.Range(1, 5));
        }

    }
}
