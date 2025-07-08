using UnityEngine;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Extensions;
using TriInspector;
using TMPro;
using System;

public class UIToggle : MonoBehaviour
{
    #region Variables
    [SerializeField] private Image _background;
    [SerializeField] private Image _handle;
    [SerializeField] private TextMeshProUGUI _onTxt;
    [SerializeField] private TextMeshProUGUI _offTxt;

    [SerializeField] private Color _activeBgColor;
    [SerializeField] private Color _inactiveBgColor;
    [SerializeField] private Color _activeHandleColor;
    [SerializeField] private Color _inactiveHandleColor;

    [Title("Animation Settings")]
    [SerializeField] private float _animationDuration = 0.3f;
    [SerializeField] private Ease _easeType = Ease.OutBack;
    [SerializeField] private float _handleScaleOnPress = 0.9f;
    [SerializeField] private float _handleScaleDuration = 0.1f;

    [Title("Handle Padding")]
    [SerializeField] private float _leftPadding = 8f;
    [SerializeField] private float _rightPadding = 8f;

    private bool _isOn = false;
    private MotionHandle _currentAnimationHandle;

    public Action<bool> OnToggleChanged;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _background.color = _inactiveBgColor;
        _handle.color = _inactiveHandleColor;
    }

    private void OnDestroy()
    {
        if (_currentAnimationHandle.IsActive())
        {
            _currentAnimationHandle.Cancel();
        }
    }
    #endregion

    #region UI Methods
    public void OnSwitchClick()
    {
        _isOn = !_isOn;

        if (_currentAnimationHandle.IsActive())
        {
            _currentAnimationHandle.Cancel();
        }

        var targetBgColor = _isOn ? _activeBgColor : _inactiveBgColor;
        var targetHandleColor = _isOn ? _activeHandleColor : _inactiveHandleColor;

        // Calculate handle target position
        var targetAnchoredPos = CalculateTargetPosition();
        var targetRotation = new Vector3(0, 0, _isOn ? 360 : 0);

        // Create sequence for all animations
        _currentAnimationHandle = LSequence.Create()
            // Handle scale animation (press effect)
            .Append(LMotion.Create(Vector3.one, Vector3.one * _handleScaleOnPress, _handleScaleDuration)
                .BindToLocalScale(_handle.rectTransform))
            .Append(LMotion.Create(Vector3.one * _handleScaleOnPress, Vector3.one, _handleScaleDuration)
                .BindToLocalScale(_handle.rectTransform))

            // Background color animation (starts with scale)
            .Join(LMotion.Create(_background.color, targetBgColor, _animationDuration)
                .WithEase(_easeType)
                .BindToColor(_background))

            // Handle color animation
            .Join(LMotion.Create(_handle.color, targetHandleColor, _animationDuration)
                .WithEase(_easeType)
                .BindToColor(_handle))

            // Handle position animation
            .Join(LMotion.Create(_handle.rectTransform.anchoredPosition, targetAnchoredPos, _animationDuration)
                .WithEase(_easeType)
                .BindToAnchoredPosition(_handle.rectTransform))

            // Handle rotation animation
            .Join(LMotion.Create(_handle.rectTransform.eulerAngles, targetRotation, _animationDuration)
                .WithEase(Ease.OutQuad)
                .BindToEulerAngles(_handle.rectTransform))

            .Run();

        // Update text immediately
        _onTxt.gameObject.SetActive(_isOn);
        _offTxt.gameObject.SetActive(!_isOn);

        OnToggleChanged?.Invoke(_isOn);
    }
    #endregion

    #region Utility Methods
    private Vector2 CalculateTargetPosition()
    {
        var bgRect = _background.rectTransform;
        var handleRect = _handle.rectTransform;
        var parentWidth = bgRect.rect.width;
        var handleWidth = handleRect.rect.width;
        var minX = -parentWidth / 2f + _leftPadding + handleWidth / 2f;
        var maxX = parentWidth / 2f - _rightPadding - handleWidth / 2f;
        var targetX = _isOn ? maxX : minX;

        return new Vector2(targetX, handleRect.anchoredPosition.y);
    }

    [Button("Test Toggle On")]
    private void TestToggleOn()
    {
        if (!_isOn)
        {
            OnSwitchClick();
        }
    }

    [Button("Test Toggle Off")]
    private void TestToggleOff()
    {
        if (_isOn)
        {
            OnSwitchClick();
        }
    }
    #endregion
}