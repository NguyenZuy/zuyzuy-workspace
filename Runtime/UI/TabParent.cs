using System;
using UnityEngine;

namespace Zuy.Workspace
{
    public sealed class TabParent : MonoBehaviour
    {
        [SerializeField] private Tab _firstSelectedTab;
        [SerializeField] private bool _changeImg;
        [SerializeField] private bool _changeImgColor;
        [SerializeField] private bool _changeTextColor;
        [SerializeField] private bool _changeText;

        public event Action<int> OnChangeTab;

        private Tab _curTab;

        private void Start()
        {
            ResetToDefault();
        }

        private void OnEnable()
        {
            ResetToDefault();
        }

        private void ResetToDefault()
        {
            SetTabState(_curTab, false);
            _curTab = _firstSelectedTab;
            SetTabState(_curTab, true);
        }

        private void SetTabState(Tab tab, bool isActive)
        {
            if (tab == null)
                return;

            try
            {
                if (_changeImg)
                    tab.UpdateImageSprite(isActive);
                if (_changeImgColor)
                    tab.UpdateImageColor(isActive);
                if (_changeTextColor)
                    tab.UpdateTextColor(isActive);
                if (_changeText)
                    tab.UpdateTextString(isActive);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error updating tab state: {ex}");
            }
        }

        public void SetActive(Tab tab, bool isActive)
        {
            if (tab == _curTab && isActive)
                return;

            OnChangeTab?.Invoke(tab.Id);

            SetTabState(_curTab, false);
            _curTab = isActive ? tab : _firstSelectedTab;
            SetTabState(_curTab, true);
        }
    }
}
