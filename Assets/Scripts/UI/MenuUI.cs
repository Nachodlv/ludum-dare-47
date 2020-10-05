using System;
using DefaultNamespace;
using Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameMode gameMode;
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private SoundWithSettings menuMusic;
        [SerializeField] private SlidesController _slidesController;

        private void Awake()
        {
#if UNITY_WEBGL
            quitButton.gameObject.SetActive(false);
#else
            quitButton.gameObject.SetActive(true);
#endif
            if (!Params.Instance.SlideShowSeen)
            {
                _slidesController.gameObject.SetActive(true);
                Params.Instance.SlideShowSeen = true;
            }
            else
            {
                _slidesController.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            AudioManager.instance.SetMusicSource(menuMusic);
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

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) StartRace();
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
