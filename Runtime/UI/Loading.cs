using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using LitMotion;
using LitMotion.Extensions;

namespace Zuy.Workspace.UI
{
    public class Loading : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Slider progressBar;
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private TextMeshProUGUI loadingText;
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private Image sliderFillImage;

        [Header("Settings")]
        [SerializeField] private string defaultLoadingText = "Loading...";
        [SerializeField] private bool showPercentage = true;
        [SerializeField] private bool autoHideOnComplete = true;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private Ease easeType = Ease.OutQuad;

        [Header("Events")]
        public Action onLoadingComplete;

        private float currentProgress;
        private bool isLoading;
        private MotionHandle progressMotionHandle;

        private void Awake()
        {
            if (loadingPanel != null)
                loadingPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            progressMotionHandle.Cancel();
        }

        public void Show()
        {
            if (loadingPanel != null)
                loadingPanel.SetActive(true);

            isLoading = true;
            SetProgress(0f);
            SetLoadingText(defaultLoadingText);
        }

        public void Hide()
        {
            if (loadingPanel != null)
                loadingPanel.SetActive(false);

            isLoading = false;
        }

        public void SetProgress(float progress)
        {
            float targetProgress = Mathf.Clamp01(progress);

            // Cancel any existing motion
            progressMotionHandle.Cancel();

            // Create smooth progress animation
            progressMotionHandle = LMotion.Create(currentProgress, targetProgress, animationDuration)
                .WithEase(easeType)
                .WithOnLoopComplete(value =>
                {
                    currentProgress = value;
                    UpdateUI();
                })
                .WithOnComplete(() =>
                {
                    if (autoHideOnComplete && currentProgress >= 1f)
                    {
                        Hide();
                        onLoadingComplete?.Invoke();
                    }
                })
                .RunWithoutBinding();
        }

        private void UpdateUI()
        {
            if (progressBar != null)
                progressBar.value = currentProgress;

            if (progressText != null && showPercentage)
                progressText.text = $"{Mathf.RoundToInt(currentProgress * 100)}%";
        }

        public void SetLoadingText(string text)
        {
            if (loadingText != null)
                loadingText.text = text;
        }

        public void SetProgressBarColor(Color color)
        {
            if (sliderFillImage != null)
            {
                LMotion.Create(sliderFillImage.color, color, animationDuration)
                    .WithEase(easeType)
                    .BindToColor(sliderFillImage);
            }
        }

        public bool IsLoading => isLoading;
        public float CurrentProgress => currentProgress;
    }
}
