using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace ZuyZuy.Workspace
{
    public class UIDialog : UIPopup
    {
        [Header("Dialog References")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TextMeshProUGUI _confirmButtonText;
        [SerializeField] private TextMeshProUGUI _cancelButtonText;

        private Action<string> _onConfirm;
        private Action _onCancel;
        private DialogType _currentType;

        protected override void Init()
        {
            m_popupName = "Dialog";

            if (_confirmButton != null)
                _confirmButton.onClick.AddListener(OnConfirmClicked);

            if (_cancelButton != null)
                _cancelButton.onClick.AddListener(OnCancelClicked);
        }

        public void ShowDialog(string title, string message, DialogType type,
            Action<string> onConfirm = null, Action onCancel = null,
            string confirmText = "OK", string cancelText = "Cancel")
        {
            _currentType = type;
            _onConfirm = onConfirm;
            _onCancel = onCancel;

            // Set texts
            if (_titleText != null) _titleText.text = title;
            if (_messageText != null) _messageText.text = message;
            if (_confirmButtonText != null) _confirmButtonText.text = confirmText;
            if (_cancelButtonText != null) _cancelButtonText.text = cancelText;

            // Setup based on dialog type
            switch (type)
            {
                case DialogType.Alert:
                    SetupAlertDialog();
                    break;
                case DialogType.Confirm:
                    SetupConfirmDialog();
                    break;
                case DialogType.Input:
                    SetupInputDialog();
                    break;
            }

            Show();
        }

        private void SetupAlertDialog()
        {
            if (_inputField != null) _inputField.gameObject.SetActive(false);
            if (_cancelButton != null) _cancelButton.gameObject.SetActive(false);
            if (_confirmButton != null) _confirmButton.gameObject.SetActive(true);
        }

        private void SetupConfirmDialog()
        {
            if (_inputField != null) _inputField.gameObject.SetActive(false);
            if (_cancelButton != null) _cancelButton.gameObject.SetActive(true);
            if (_confirmButton != null) _confirmButton.gameObject.SetActive(true);
        }

        private void SetupInputDialog()
        {
            if (_inputField != null)
            {
                _inputField.gameObject.SetActive(true);
                _inputField.text = string.Empty;
            }
            if (_cancelButton != null) _cancelButton.gameObject.SetActive(true);
            if (_confirmButton != null) _confirmButton.gameObject.SetActive(true);
        }

        private void OnConfirmClicked()
        {
            string result = string.Empty;
            if (_currentType == DialogType.Input && _inputField != null)
            {
                result = _inputField.text;
            }

            _onConfirm?.Invoke(result);
            Hide();
        }

        private void OnCancelClicked()
        {
            _onCancel?.Invoke();
            Hide();
        }

        protected override void OnHide()
        {
            base.OnHide();
            _onConfirm = null;
            _onCancel = null;
        }
    }
}