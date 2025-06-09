using UnityEngine;

namespace ZuyZuy.Workspace
{
    public abstract class UIView : MonoBehaviour
    {
        protected string m_viewName;
        protected object m_data;

        private GameObject _container;

        public string ViewName => m_viewName;

        protected virtual void Start()
        {
            _container = transform.GetChild(0).gameObject;
            Init();
        }

        protected abstract void Init();

        public virtual void Show(object data = null)
        {
            m_data = data;
            _container.SetActive(true);
            OnShow();
        }

        public virtual void Hide()
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

        protected virtual void OnHideClick()
        {
            Hide();
        }
    }
}