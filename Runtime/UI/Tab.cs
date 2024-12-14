using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Zuy.Workspace
{
    public sealed class Tab : MonoBehaviour
    {
        [SerializeField] private Button _btn;

        [Header("Image")]
        [SerializeField] private Image _img;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _deactiveSprite;
        [SerializeField] private Color _activeImgColor;
        [SerializeField] private Color _deactiveImgColor;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _txt;
        [SerializeField] private Color _activeTxtColor;
        [SerializeField] private Color _deactiveTxtColor;
        [SerializeField] private string _activeTxtStr;
        [SerializeField] private string _deactiveTxtStr;

        private TabParent _tabParent;
        private bool _isActive;

        private void Start()
        {
            _tabParent = GetComponentInParent<TabParent>();
            _btn.onClick.AddListener(OnClick);
        }

        public void UpdateImageSprite(bool isActive)
        {
            if (_img != null)
                _img.sprite = isActive ? _activeSprite : _deactiveSprite;
        }

        public void UpdateImageColor(bool isActive)
        {
            if (_img != null)
                _img.color = isActive ? _activeImgColor : _deactiveImgColor;
        }

        public void UpdateTextColor(bool isActive)
        {
            if (_txt != null)
                _txt.color = isActive ? _activeTxtColor : _deactiveTxtColor;
        }

        public void UpdateTextString(bool isActive)
        {
            if (_txt != null)
                _txt.text = isActive ? _activeTxtStr : _deactiveTxtStr;
        }

        private void OnClick()
        {
            _tabParent.SetActive(this, !_isActive);
        }
    }
}
