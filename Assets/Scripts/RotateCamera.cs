using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace DefaultNamespace
{
    public class RotateCamera : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            virtualCamera.Priority = 200;
        }

        private void Update()
        {
            var playerPosition = player.position;
            var angles = Vector2.SignedAngle(Vector2.down, playerPosition);
            virtualCamera.m_Lens.Dutch = angles;
        }
    }
}
