using UnityEngine;

namespace ZuyZuy.Workspace
{
    public abstract class UIView : MonoBehaviour
    {
        protected string m_viewName;

        private GameObject _container;

        public string ViewName => m_viewName;

        protected virtual void Start()
        {
            Init();
        }

        protected abstract void Init();

        public void Show()
        {
            _container.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            _container.SetActive(false);
            OnHide();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}