using UnityEngine;
using System.Collections.Generic;

namespace ZuyZuy.Workspace
{
    public class UIPopupController : MonoBehaviour
    {
        private UIPopupContainer _popupContainer;
        private readonly Stack<UIPopup> _popupStack = new Stack<UIPopup>();
        private readonly Dictionary<string, UIPopup> _activePopups = new Dictionary<string, UIPopup>();
        private bool _isTransitioning;

        [SerializeField] private float _transitionDelay = 0.1f;
        [SerializeField] private bool _allowMultiplePopups = false;

        void Start()
        {
            _popupContainer = FindFirstObjectByType<UIPopupContainer>();
        }

        public UIPopup ShowPopup(string popupName)
        {
            if (_isTransitioning)
            {
                Debug.LogWarning("Cannot show popup while another transition is in progress");
                return null;
            }

            var popup = _popupContainer.GetPopup(popupName);
            if (popup == null)
            {
                Debug.LogError($"Popup {popupName} not found");
                return null;
            }

            _isTransitioning = true;

            if (!_allowMultiplePopups && _popupStack.Count > 0)
            {
                var currentPopup = _popupStack.Peek();
                currentPopup.Hide();
                _popupStack.Push(popup);
            }
            else
            {
                _popupStack.Push(popup);
            }

            _activePopups[popupName] = popup;
            popup.Show();

            // Reset transition flag after animation duration
            Invoke(nameof(ResetTransitionFlag), popup._animationDuration + _transitionDelay);

            return popup;
        }

        public void HidePopup(string popupName)
        {
            if (_isTransitioning)
            {
                Debug.LogWarning("Cannot hide popup while another transition is in progress");
                return;
            }

            if (!_activePopups.TryGetValue(popupName, out var popup))
            {
                Debug.LogWarning($"Popup {popupName} is not active");
                return;
            }

            _isTransitioning = true;

            // Remove from active popups
            _activePopups.Remove(popupName);

            // Remove from stack
            var tempStack = new Stack<UIPopup>();
            while (_popupStack.Count > 0)
            {
                var current = _popupStack.Pop();
                if (current != popup)
                {
                    tempStack.Push(current);
                }
            }

            // Restore stack
            while (tempStack.Count > 0)
            {
                _popupStack.Push(tempStack.Pop());
            }

            popup.Hide();

            // If we have previous popups and multiple popups are not allowed, show the previous one
            if (!_allowMultiplePopups && _popupStack.Count > 0)
            {
                var previousPopup = _popupStack.Peek();
                previousPopup.Show();
            }

            // Reset transition flag after animation duration
            Invoke(nameof(ResetTransitionFlag), popup._animationDuration + _transitionDelay);
        }

        public void HideAllPopups()
        {
            if (_isTransitioning)
            {
                Debug.LogWarning("Cannot hide all popups while a transition is in progress");
                return;
            }

            _isTransitioning = true;

            foreach (var popup in _activePopups.Values)
            {
                popup.Hide();
            }

            _activePopups.Clear();
            _popupStack.Clear();

            // Reset transition flag after a reasonable delay
            Invoke(nameof(ResetTransitionFlag), 0.5f);
        }

        public bool IsPopupActive(string popupName)
        {
            return _activePopups.ContainsKey(popupName);
        }

        public UIPopup GetCurrentPopup()
        {
            return _popupStack.Count > 0 ? _popupStack.Peek() : null;
        }

        private void ResetTransitionFlag()
        {
            _isTransitioning = false;
        }
    }
}
