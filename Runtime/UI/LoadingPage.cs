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
        private MotionHandle? progressMotionHandle;

        public bool IsLoading => _isLoading;
        public float CurrentProgress => _currentProgress;

        private void Awake()
        {
            if (container != null)
                container.SetActive(false);
        }

        private void OnDestroy()
        {
            if (progressMotionHandle.HasValue)
            {
                progressMotionHandle.Value.Cancel();
            }
        }

        public void Show()
        {
            if (container != null)
                container.SetActive(true);

            _isLoading = true;
            SetProgress(0f);
            SetLoadingText(defaultLoadingText);
        }

        public void Hide()
        {
            if (container != null)
                container.SetActive(false);

            _isLoading = false;
        }

        public void SetProgress(float progress)
        {
            float targetProgress = Mathf.Clamp01(progress);

            // Cancel any existing motion
            if (progressMotionHandle.HasValue)
            {
                progressMotionHandle.Value.Cancel();
            }

            // Create smooth progress animation
            progressMotionHandle = LMotion.Create(_currentProgress, targetProgress, animationDuration)
                .WithEase(easeType)
                .WithOnLoopComplete(value =>
                {
                    _currentProgress = value;
                    UpdateUI();
                })
                .WithOnComplete(() =>
                {
                    if (autoHideOnComplete && _currentProgress >= 1f)
                    {
                        Hide();
                        onLoadingComplete?.Invoke();
                    }
                })
                .RunWithoutBinding();
        }

        private void UpdateUI()
        {
            Debug.Log($"[LoadingPage] Current Progress: {_currentProgress:F2}");

            if (progressBar != null)
            {
                progressBar.value = _currentProgress;
                Debug.Log($"[LoadingPage] Progress Bar Value Set: {_currentProgress:F2}");
            }

            if (progressText != null && showPercentage)
            {
                int percentage = Mathf.RoundToInt(_currentProgress * 100);
                progressText.text = $"{percentage}%";
                Debug.Log($"[LoadingPage] Progress Text Set: {percentage}%");
            }
        }

        public void SetLoadingText(string text)
        {
            if (loadingText != null)
                loadingText.text = text;
        }
    }
}
