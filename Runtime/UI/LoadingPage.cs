using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Cysharp.Threading.Tasks;
using LitMotion;

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
        public Action onLoadingStart;
        public Action onLoadingComplete;
        public Action onLoadingError;

        [Header("Parallel Job")]
        public Func<UniTask> parallelJob;

        private float _currentProgress;
        private bool _isLoading;
        private MotionHandle progressMotionHandle;
        private bool isProgressAnimationDone;
        private bool isJobDone;
        private UniTask jobTask;

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
            // Cancel any existing operations
            progressMotionHandle.TryCancel();
            jobTask = default;

            if (container != null)
                container.SetActive(true);

            _isLoading = true;
            _currentProgress = 0f;
            isProgressAnimationDone = false;
            isJobDone = false;

            UpdateUI();
            SetLoadingText(defaultLoadingText);
            onLoadingStart?.Invoke();

            // Start progress animation to 99%
            progressMotionHandle = LMotion.Create(0f, 99f, animationDuration)
                .WithEase(easeType)
                .WithOnComplete(() =>
                {
                    isProgressAnimationDone = true;
                    CheckForCompletion();
                })
                .Bind(value =>
                {
                    _currentProgress = value;
                    UpdateUI();
                });

            // Start parallel job
            if (parallelJob != null)
            {
                jobTask = parallelJob.Invoke();
                ObserveJobTask().Forget();
            }
            else
            {
                isJobDone = true;
                CheckForCompletion();
            }
        }

        private async UniTaskVoid ObserveJobTask()
        {
            try
            {
                await jobTask;
                isJobDone = true;
                CheckForCompletion();
            }
            catch
            {
                OnJobFailed();
            }
        }

        private void CheckForCompletion()
        {
            if (isProgressAnimationDone && isJobDone)
            {
                // Animate final 1% when both are ready
                progressMotionHandle.TryCancel();
                LMotion.Create(_currentProgress, 100f, 0.2f)
                    .WithEase(easeType)
                    .WithOnComplete(() => CheckCompletion(100f))
                    .Bind(value =>
                    {
                        _currentProgress = value;
                        UpdateUI();
                    });
            }
        }

        private void OnJobFailed()
        {
            progressMotionHandle.TryCancel();
            _currentProgress = 0f;
            UpdateUI();
            onLoadingError?.Invoke();
            Show(); // Restart the loading process
        }

        public void Hide()
        {
            if (container != null)
                container.SetActive(false);

            _isLoading = false;
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
            if (progressBar != null && progressText != null && showPercentage)
            {
                int percentage = Mathf.RoundToInt(_currentProgress);
                progressBar.value = percentage;
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