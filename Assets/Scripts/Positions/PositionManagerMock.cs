using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Positions
{
    public class PositionManagerMock : MonoBehaviour, IPositionManager
    {
        public event PositionCallback OnPlayerPositionChange;
        public event PositionCallback OnPlayerFinishRace;
        public event LapCallback OnPlayerFinishLap;

        private void Awake()
        {
            ChangePlayerPositionRandom();
        }

        public List<Racer> GetRacersPositions()
        {
            throw new System.NotImplementedException();
        }

        public void ChangePlayerPositionRandom()
        {
            OnPlayerPositionChange?.Invoke(Random.Range(1, 10));
            Invoke(nameof(ChangePlayerPositionRandom), Random.Range(1, 5));
        }
    }
}
