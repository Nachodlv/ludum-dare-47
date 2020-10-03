using System;
using Positions;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerPositionUI : MonoBehaviour
    {
        [SerializeField] private PositionManagerMock positionManager;
        [SerializeField] private TextMeshProUGUI currentPosition;
        [SerializeField] private TextMeshProUGUI positionText;

        private void Awake()
        {
            positionManager.OnPlayerPositionChange += PlayerPositionChange;
        }

        private void PlayerPositionChange(int newPosition)
        {
            currentPosition.text = newPosition.ToString();
            var endingText = "th";
            if (newPosition == 1) endingText = "st";
            if (newPosition == 2) endingText = "nd";
            if (newPosition == 3) endingText = "rd";
            positionText.text = endingText;
        }
    }
}
