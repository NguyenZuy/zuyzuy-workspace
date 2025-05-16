using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZuyZuy.Workspace
{
    public class LoadingCircle : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject _container;
        [SerializeField] private Image _loadingCircleImage;
        [SerializeField] private TextMeshProUGUI _loadingText;

        [Header("Settings")]
        [SerializeField] private string _defaultLoadingText = "Loading...";
        [SerializeField] private float _rotationSpeed = 360f;
        [SerializeField] private bool _clockwise = true;
        [SerializeField] private Color _circleColor = Color.white;

        private bool _isLoading;
        private float _currentRotation;

        public bool IsLoading => _isLoading;

        private void Awake()
        {
            if (_container != null)
                _container.SetActive(false);

            if (_loadingCircleImage != null)
                _loadingCircleImage.color = _circleColor;
        }

        private void Update()
        {
            if (_isLoading && _loadingCircleImage != null)
            {
                float direction = _clockwise ? -1f : 1f;
                _currentRotation += _rotationSpeed * direction * Time.deltaTime;
                _loadingCircleImage.transform.rotation = Quaternion.Euler(0, 0, _currentRotation);
            }
        }

        public void Show()
        {
            if (_container != null)
                _container.SetActive(true);

            _isLoading = true;
            SetLoadingText(_defaultLoadingText);
        }

        public void Hide()
        {
            if (_container != null)
                _container.SetActive(false);

            _isLoading = false;
        }

        public void SetLoadingText(string text)
        {
            if (_loadingText != null)
                _loadingText.text = text;
        }

        public void SetCircleColor(Color color)
        {
            if (_loadingCircleImage != null)
            {
                _circleColor = color;
                _loadingCircleImage.color = color;
            }
        }
    }
}