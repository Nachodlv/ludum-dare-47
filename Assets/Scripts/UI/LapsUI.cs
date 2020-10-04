using System;
using Positions;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LapsUI : MonoBehaviour
    {
        [SerializeField] private PositionManager positionManager;
        [SerializeField] private GameMode gameMode;

        [Header("Texts")] [SerializeField] private TextMeshProUGUI totalLaps;
        [SerializeField] private TextMeshProUGUI currentLap;

        private void Awake()
        {
            totalLaps.text = gameMode.TotalLaps.ToString();
            positionManager.OnPlayerFinishLap += LapFinished;
        }

        private void LapFinished(int lap)
        {
            if (lap < 0 || lap > gameMode.TotalLaps) return;
            currentLap.text = (lap + 1).ToString();
        }
    }
}
