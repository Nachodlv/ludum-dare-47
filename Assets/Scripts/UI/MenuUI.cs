using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameMode gameMode;
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
#if UNITY_WEBGL
            quitButton.gameObject.SetActive(false);
#else
            quitButton.gameObject.SetActive(true);
#endif
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(StartRace);
            quitButton.onClick.AddListener(CloseGame);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(StartRace);
            quitButton.onClick.RemoveListener(CloseGame);
        }

        private void StartRace()
        {
            gameObject.SetActive(false);
            gameMode.StartRace();
        }

        private void CloseGame()
        {
            Application.Quit();
        }

    }
}
