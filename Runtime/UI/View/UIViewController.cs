using UnityEngine;

namespace ZuyZuy.Workspace
{
    public class UIViewController : MonoBehaviour
    {
        private UIViewContainer _viewContainer;
        private UIView _activeView;

        private void Start()
        {
            _viewContainer = FindFirstObjectByType<UIViewContainer>();
        }

        public void ShowView(string viewName, object data = null)
        {
            if (_activeView != null)
            {
                _activeView.Hide();
            }

            _activeView = _viewContainer.GetView(viewName);
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
            return _activeView != null && _activeView.ViewName.Equals(viewName);
        }

        public UIView GetActiveView() => _activeView;
    }
}