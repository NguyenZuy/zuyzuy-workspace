using System.Collections.Generic;
using UnityEngine;

namespace ZuyZuy.Workspace
{
    public class UIPopupContainer : MonoBehaviour
    {
        private List<UIPopup> _popups;

        public UIPopup GetPopup(UIPopupName popupName)
        {
            return _popups.Find(popup => popup.PopupName == popupName);
        }
    }
}
