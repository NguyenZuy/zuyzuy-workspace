using System.Collections.Generic;
using UnityEngine;

namespace ZuyZuy.Workspace
{
    public class UIViewContainer : MonoBehaviour
    {
        private List<UIView> _views;

        private void Awake()
        {
            _views = new List<UIView>(GetComponentsInChildren<UIView>(true));
        }

        public UIView GetView(string viewName)
        {
            return _views.Find(view => view.ViewName.Equals(viewName));
        }
    }
}