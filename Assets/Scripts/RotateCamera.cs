using System;
using Cinemachine;
using UnityEngine;

namespace DefaultNamespace
{
    public class RotateCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private float rotationSpeed;

        private void Update()
        {
            var currentDutch = virtualCamera.m_Lens.Dutch;
            if (currentDutch >= 180)
            {
                currentDutch *= -1;
            }
            currentDutch += rotationSpeed * Time.deltaTime;
            virtualCamera.m_Lens.Dutch = currentDutch;
        }
    }
}
