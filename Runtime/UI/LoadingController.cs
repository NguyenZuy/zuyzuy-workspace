using UnityEngine;
using System;

namespace ZuyZuy.Workspace
{
    public class LoadingController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool _useLoadingPageByDefault = true;

        private LoadingPage _loadingPage;
        private LoadingCircle _loadingCircle;

        public bool IsLoading => (_loadingPage != null && _loadingPage.IsLoading) ||
                        (_loadingCircle != null && _loadingCircle.IsLoading);

        void Awake()
        {
            _loadingPage = FindFirstObjectByType<LoadingPage>();
            _loadingCircle = FindFirstObjectByType<LoadingCircle>();
        }

        public void ShowLoadingPage(string loadingText = "Loading...", float initialProgress = 0f)
        {
            if (_loadingPage == null)
            {
                Debug.LogError("Loading page reference is missing!");
                return;
            }

            HideAll();
            _loadingPage.Show();
            _loadingPage.SetLoadingText(loadingText);
            _loadingPage.SetProgress(initialProgress);
        }

        public void ShowLoadingCircle(string loadingText = "Loading...")
        {
            if (_loadingCircle == null)
            {
                Debug.LogError("Loading circle reference is missing!");
                return;
            }

            HideAll();
            _loadingCircle.Show();
            _loadingCircle.SetLoadingText(loadingText);
        }

        public void UpdateLoadingProgress(float progress)
        {
            if (_loadingPage != null && _loadingPage.IsLoading)
            {
                _loadingPage.SetProgress(progress);
            }
        }

        public void SetLoadingText(string text)
        {
            if (_loadingPage != null && _loadingPage.IsLoading)
            {
                _loadingPage.SetLoadingText(text);
            }
            else if (_loadingCircle != null && _loadingCircle.IsLoading)
            {
                _loadingCircle.SetLoadingText(text);
            }
        }

        public void HideAll()
        {
            if (_loadingPage != null)
                _loadingPage.Hide();

            if (_loadingCircle != null)
                _loadingCircle.Hide();
        }
    }
}
