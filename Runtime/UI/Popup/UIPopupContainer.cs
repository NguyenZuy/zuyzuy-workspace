using System.Collections.Generic;
using UnityEngine;

namespace ZuyZuy.Workspace
{
    public class UIPopupContainer : MonoBehaviour
    {
        private List<UIPopup> _popups;

        private void Awake()
        {
            // Get all UIPopup components from children
            _popups = new List<UIPopup>(GetComponentsInChildren<UIPopup>(true));
        }

        public UIPopup GetPopup(string popupName)
        {
            return _popups.Find(popup => popup.PopupName.Equals(popupName));
        }
    }
}
