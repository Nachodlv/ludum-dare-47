using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WaitTimeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeDisplay;
        [SerializeField] private string messageAtEnd = "Go!";
        [SerializeField] private float timeMessageAtEnd = 2f;
        [SerializeField] private Animator animator;

        public event Action OnFinishWaiting;

        private WaitForSeconds _waitOneSecond;
        private static readonly int Show = Animator.StringToHash("show");
        private static readonly int Hide = Animator.StringToHash("hide");

        private void Awake()
        {
            _waitOneSecond = new WaitForSeconds(1);
        }

        public void StartCountdown(int time)
        {
            StartCoroutine(Countdown(time));
        }

        private IEnumerator Countdown(int time)
        {
            timeDisplay.gameObject.SetActive(true);
            timeDisplay.text = time.ToString();
            var timeRemaining = time - 1;
            while (timeRemaining > 0)
            {
                yield return _waitOneSecond;
                animator.SetTrigger(Show);
                timeDisplay.text = timeRemaining.ToString();
                timeRemaining--;
                animator.SetTrigger(Hide);
            }
            yield return _waitOneSecond;
            animator.SetTrigger(Show);
            timeDisplay.text = messageAtEnd;
            OnFinishWaiting?.Invoke();
            yield return new WaitForSeconds(timeMessageAtEnd);
            animator.SetTrigger(Hide);
        }

    }
}
