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

        private void Awake()
        {
            positionManager.OnPlayerPositionChange += PlayerPositionChange;
        }

        private void PlayerPositionChange(int newPosition)
        {
            currentPosition.text = newPosition.ToString();
        }
    }
}
