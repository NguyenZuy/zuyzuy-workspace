using UnityEngine;

namespace ZuyZuy.Workspace
{
    public class UIPopupController : MonoBehaviour
    {
        private UIPopupContainer _popupContainer;

        void Start()
        {
            _popupContainer = FindFirstObjectByType<UIPopupContainer>();
        }

        public UIPopup ShowPopup(UIPopupName popupName)
        {
            var popup = _popupContainer.GetPopup(popupName);
            popup.Show();
            return popup;
        }

        public void HidePopup(UIPopupName popupName)
        {
            var popup = _popupContainer.GetPopup(popupName);
            popup.Hide();
        }
    }
}
