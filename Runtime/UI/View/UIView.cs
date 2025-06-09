using UnityEngine;

namespace ZuyZuy.Workspace
{
    public abstract class UIView : MonoBehaviour
    {
        protected string m_ViewName;
        protected object m_Data;

        protected GameObject m_Container;

        public string ViewName => m_ViewName;

        protected virtual void Start()
        {
            m_Container = transform.GetChild(0).gameObject;
            Init();
        }

        protected abstract void Init();

        public virtual void Show(object data = null)
        {
            m_Data = data;
            m_Container.SetActive(true);
            OnShow();
        }

        public virtual void Hide()
        {
            m_Container.SetActive(false);
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