using System;
using Cinemachine;
using UnityEngine;

namespace DefaultNamespace
{
    public class RotateCamera : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private void Update()
        {
            var playerPosition = player.position;
            var angles = Vector2.SignedAngle(Vector2.down, playerPosition);
            virtualCamera.m_Lens.Dutch = angles;
        }
    }
}
