using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using LitMotion;
using LitMotion.Extensions;

namespace ZuyZuy.Workspace
{
    public class LoadingPage : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Slider progressBar;
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private TextMeshProUGUI loadingText;
        [SerializeField] private GameObject container;

        [Header("Settings")]
        [SerializeField] private string defaultLoadingText = "Loading...";
        [SerializeField] private bool showPercentage = true;
        [SerializeField] private bool autoHideOnComplete = true;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private Ease easeType = Ease.OutQuad;

        [Header("Events")]
        public Action onLoadingComplete;

        private float _currentProgress;
        private bool _isLoading;
        private MotionHandle progressMotionHandle;

        public bool IsLoading => _isLoading;
        public float CurrentProgress => _currentProgress;

        private void Awake()
        {
            if (container != null)
                container.SetActive(false);
        }

        private void OnDestroy()
        {
            progressMotionHandle.TryCancel();
        }

        public void Show()
        {
            if (container != null)
                container.SetActive(true);

            _isLoading = true;
            _currentProgress = 0f;
            UpdateUI();
            SetLoadingText(defaultLoadingText);
        }

        public void Hide()
        {
            if (container != null)
                container.SetActive(false);

            _isLoading = false;
        }

        public void SetProgress(float targetProgress, bool animate = true)
        {
            progressMotionHandle.TryCancel();

            if (!animate)
            {
                _currentProgress = targetProgress;
                UpdateUI();
                CheckCompletion(targetProgress);
                return;
            }

            progressMotionHandle = LMotion.Create(_currentProgress, targetProgress, animationDuration)
                .WithEase(easeType)
                .WithOnComplete(() => CheckCompletion(targetProgress))
                .Bind(value =>
                {
                    _currentProgress = value;
                    UpdateUI();
                });
        }

        private void CheckCompletion(float finalProgress)
        {
            if (autoHideOnComplete && Mathf.Approximately(finalProgress, 100f))
            {
                Hide();
                onLoadingComplete?.Invoke();
            }
        }

        private void UpdateUI()
        {
            if (progressBar != null)
            {
                progressBar.value = _currentProgress;
            }

            if (progressText != null && showPercentage)
            {
                int percentage = Mathf.RoundToInt(_currentProgress);
                progressText.text = $"{percentage}%";
            }
        }

        public void SetLoadingText(string text)
        {
            if (loadingText != null)
                loadingText.text = text;
        }
    }
}
