using UnityEngine;

namespace ZuyZuy.Workspace
{
    public class UIViewController : MonoBehaviour
    {
        public UIView firstView;

        private UIViewContainer _viewContainer;
        private UIView _activeView;

        private void Start()
        {
            _viewContainer = FindFirstObjectByType<UIViewContainer>();
            if (_viewContainer == null)
                Debug.LogError("UIViewContainer not found in scene!");

            if (firstView == null)
                Debug.LogError("First view reference is not set!");
            else
                ShowView(firstView.ViewName);
        }

        public void ShowView(string viewName, object data = null)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                Debug.LogError("View name cannot be null or empty!");
                return;
            }

            if (_viewContainer == null)
            {
                Debug.LogError("UIViewContainer is not initialized!");
                return;
            }

            if (_activeView != null)
            {
                _activeView.Hide();
            }

            _activeView = _viewContainer.GetView(viewName);
            if (_activeView == null)
            {
                Debug.LogError($"View '{viewName}' not found in container!");
                return;
            }

            _activeView.Show(data);
        }

        public void HideCurrentView()
        {
            if (_activeView != null)
            {
                _activeView.Hide();
                _activeView = null;
            }
        }

        public bool IsViewActive(string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                Debug.LogError("View name cannot be null or empty!");
                return false;
            }

            return _activeView != null && _activeView.ViewName.Equals(viewName);
        }

        public UIView GetActiveView() => _activeView;
    }
}