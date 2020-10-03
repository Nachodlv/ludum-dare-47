using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LeaderBoardUI : MonoBehaviour
    {
        [SerializeField] private RacerPosition racerPosition;
        [SerializeField] private RectTransform leaderBoardContent;
        [SerializeField] private Animator animator;
        [SerializeField] private Button retryButton;

        private List<RacerPosition> _positions;
        private static readonly int Show = Animator.StringToHash("show");

        private void Awake()
        {
            _positions = new List<RacerPosition>();
            retryButton.onClick.AddListener(ReloadScene);
        }

        public void ShowLeaderBoard(List<Racer> racers)
        {
            for (var i = 0; i < racers.Count; i++)
            {
                var racer = racers[i];
                RacerPosition newRacerPosition;
                if (i < _positions.Count)
                {
                    newRacerPosition = _positions[i];
                }
                else
                {
                    newRacerPosition = Instantiate(racerPosition, leaderBoardContent);
                    _positions.Add(newRacerPosition);
                }
                newRacerPosition.FillWithRacer(racer, i + 1);
            }
            animator.SetTrigger(Show);
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
