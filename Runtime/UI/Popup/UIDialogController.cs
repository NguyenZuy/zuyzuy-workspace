using UnityEngine;
using System;

namespace ZuyZuy.Workspace
{
    public class UIDialogController : MonoBehaviour
    {
        private UIPopupController _popupController;
        private UIDialog _dialogInstance;

        private void Start()
        {
            _popupController = FindFirstObjectByType<UIPopupController>();
            if (_popupController == null)
            {
                Debug.LogError("UIPopupController not found in scene!");
                return;
            }

            // Get the dialog instance from the popup container
            _dialogInstance = _popupController.ShowPopup("Dialog") as UIDialog;
            if (_dialogInstance == null)
            {
                Debug.LogError("Dialog prefab not found in popup container!");
                return;
            }

            // Hide it initially
            _popupController.HidePopup("Dialog");
        }

        public void ShowAlert(string title, string message, Action onConfirm = null, string confirmText = "OK")
        {
            ShowDialog(title, message, DialogType.Alert,
                result => onConfirm?.Invoke(),
                null,
                confirmText);
        }

        public void ShowConfirm(string title, string message, Action onConfirm = null, Action onCancel = null,
            string confirmText = "OK", string cancelText = "Cancel")
        {
            ShowDialog(title, message, DialogType.Confirm,
                result => onConfirm?.Invoke(),
                onCancel,
                confirmText,
                cancelText);
        }

        public void ShowInput(string title, string message, Action<string> onConfirm = null, Action onCancel = null,
            string confirmText = "OK", string cancelText = "Cancel")
        {
            ShowDialog(title, message, DialogType.Input,
                onConfirm,
                onCancel,
                confirmText,
                cancelText);
        }

        private void ShowDialog(string title, string message, DialogType type,
            Action<string> onConfirm = null, Action onCancel = null,
            string confirmText = "OK", string cancelText = "Cancel")
        {
            if (_dialogInstance == null)
            {
                Debug.LogError("Dialog instance is not initialized!");
                return;
            }

            _dialogInstance.ShowDialog(title, message, type, onConfirm, onCancel, confirmText, cancelText);
        }
    }
}