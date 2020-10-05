using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class WrongDirectionUI : MonoBehaviour
    {
        [SerializeField] private PositionManager positionManager;
        [SerializeField] private Animator animator;
        [SerializeField] private float timeShowingAlert;

        private static readonly int Show = Animator.StringToHash("show");
        private static readonly int Hide = Animator.StringToHash("hide");
        private bool showing;
        private float _lastWarning;

        private void Awake()
        {
            positionManager.OnWrongDirection += WrongDirection;
        }

        private void WrongDirection()
        {
            _lastWarning = Time.time;
            if (showing)
                return;
            animator.SetTrigger(Show);
            showing = true;
            StartCoroutine(WaitToHide());
        }

        private IEnumerator WaitToHide()
        {
            while (Time.time - _lastWarning < timeShowingAlert)
            {
                yield return null;
            }
            animator.SetTrigger(Hide);
            showing = false;
        }
    }
}
