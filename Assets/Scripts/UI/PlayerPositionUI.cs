using System;
using Positions;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerPositionUI : MonoBehaviour
    {
        [SerializeField] private PositionManager positionManager;
        [SerializeField] private TextMeshProUGUI currentPosition;
        [SerializeField] private TextMeshProUGUI positionText;

        private void Awake()
        {
            positionManager.OnPlayerPositionChange += PlayerPositionChange;
        }

        private void PlayerPositionChange(int newPosition)
        {
            var position = newPosition + 1;
            currentPosition.text = position.ToString();
            var endingText = "th";
            if (position == 1) endingText = "st";
            if (position == 2) endingText = "nd";
            if (position == 3) endingText = "rd";
            positionText.text = endingText;
        }
    }
}
