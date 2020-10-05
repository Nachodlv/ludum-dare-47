using System;
using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SlidesController : MonoBehaviour
    {
        [SerializeField] private List<CanvasGroup> slides;
        [SerializeField] private List<SoundWithSettings> sounds;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button leftButton;
        [SerializeField] private float changeSlidesVelocity;
        [SerializeField] private Button closeSlides;

        private int _currentSlide;
        private Coroutine _currentCoroutine;

        private void Awake()
        {
        }

        private void OnEnable()
        {
            ShowFirstSlide();
            _currentSlide = 0;
            leftButton.onClick.AddListener(PreviousSlide);
            rightButton.onClick.AddListener(NextSlide);
            closeSlides.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            leftButton.onClick.RemoveListener(PreviousSlide);
            rightButton.onClick.RemoveListener(NextSlide);
            closeSlides.onClick.RemoveListener(Close);
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        private void NextSlide()
        {
            if(_currentSlide == 0) leftButton.gameObject.SetActive(true);
            _currentSlide++;
            if (_currentSlide >= slides.Count)
            {
                gameObject.SetActive(false);
                return;
            }
            if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(ChangeSlide(slides[_currentSlide - 1], slides[_currentSlide]));
        }

        private void PreviousSlide()
        {
            if (_currentSlide <= 0) return;
            _currentSlide--;
            if (_currentSlide == 0) leftButton.gameObject.SetActive(false);
            if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(ChangeSlide(slides[_currentSlide + 1], slides[_currentSlide]));
        }

        private void ShowFirstSlide()
        {
            leftButton.gameObject.SetActive(false);
            ChangeVisibilityCanvasGroup(slides[0], true);
            for (var i = 1; i < slides.Count; i++)
            {
                ChangeVisibilityCanvasGroup(slides[i], false);
            }
        }

        private void ChangeVisibilityCanvasGroup(CanvasGroup canvasGroup, bool show)
        {
            canvasGroup.alpha = show ? 1 : 0;
            canvasGroup.interactable = show;
            canvasGroup.blocksRaycasts = show;
        }

        private IEnumerator ChangeSlide(CanvasGroup hide, CanvasGroup show)
        {
            while (hide.alpha > 0.01)
            {
                hide.alpha -= Time.deltaTime * changeSlidesVelocity;
                yield return null;
            }

            while (show.alpha < 0.99)
            {
                show.alpha += Time.deltaTime * changeSlidesVelocity;
                yield return null;
            }

            ChangeVisibilityCanvasGroup(hide, false);
            ChangeVisibilityCanvasGroup(show, true);
        }
    }
}
