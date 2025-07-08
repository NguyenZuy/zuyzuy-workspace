using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using LitMotion;
using LitMotion.Extensions;
using System;
using TriInspector;

namespace ZuyZuy.Workspace
{
    [RequireComponent(typeof(Button))]
    public class UIButton : MonoBehaviour
    {
        #region Variables

        [Title("Button Effect Settings")]
        [SerializeField] private ButtonClickEffect clickEffect = ButtonClickEffect.Scale;

        [ShowIf(nameof(clickEffect), ButtonClickEffect.Scale)]
        [SerializeField] private Vector3 scaleAmount = Vector3.one * 0.9f;

        [ShowIf(nameof(clickEffect), ButtonClickEffect.Punch)]
        [SerializeField] private Vector3 punchStrength = Vector3.one * 0.1f;

        [ShowIf(nameof(clickEffect), ButtonClickEffect.Shake)]
        [SerializeField] private Vector3 shakeStrength = Vector3.one * 5f;

        [ShowIf(nameof(clickEffect), ButtonClickEffect.Rotation)]
        [SerializeField] private Vector3 rotationAmount = new Vector3(0, 0, 15f);

        [ShowIf(nameof(clickEffect), ButtonClickEffect.ColorTint)]
        [SerializeField] private Color tintColor = Color.gray;

        [ShowIf(nameof(clickEffect), ButtonClickEffect.Bounce)]
        [SerializeField] private float bounceScale = 1.2f;

        [ShowIf(nameof(clickEffect), ButtonClickEffect.Squeeze)]
        [SerializeField] private Vector3 squeezeScale = new Vector3(1.1f, 0.9f, 1f);

        [ShowIf(nameof(clickEffect), ButtonClickEffect.Flash)]
        [SerializeField] private Color flashColor = Color.white;

        [ShowIf(nameof(clickEffect), ButtonClickEffect.Pulse)]
        [SerializeField] private float pulseScale = 1.1f;

        [Title("Animation Settings")]
        [SerializeField] private float animationDuration = 0.2f;
        [SerializeField] private Ease easeType = Ease.OutQuad;
        [SerializeField] private bool useUnscaledTime = false;

        [Title("Interaction Settings")]
        [SerializeField] private bool disableOnClick = false;
        [SerializeField] private float disableDuration = 1f;
        [SerializeField] private bool playAudioOnClick = true;

        [Title("Events")]
        [SerializeField] private UnityEvent onButtonClick;
        [SerializeField] private UnityEvent onEffectComplete;

        private Button _button;
        private Image _image;
        private RectTransform _rectTransform;
        private Vector3 _originalScale;
        private Vector3 _originalRotation;
        private Color _originalColor;
        private MotionHandle _currentEffectHandle;
        private bool _isPlaying;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            CacheComponents();
            StoreOriginalValues();
        }

        private void Start()
        {
            SetupButton();
        }

        private void OnDestroy()
        {
            CleanupEffects();
        }

        #endregion

        #region Virtual Methods

        protected virtual void OnButtonClicked()
        {
            if (_isPlaying) return;

            PlayClickEffect();
            onButtonClick?.Invoke();
        }

        protected virtual void OnEffectCompleted()
        {
            onEffectComplete?.Invoke();
        }

        #endregion

        #region UI Methods

        public void SetInteractable(bool interactable)
        {
            if (_button != null)
                _button.interactable = interactable;
        }

        public void SetClickEffect(ButtonClickEffect effect)
        {
            clickEffect = effect;
        }

        public void PlayEffect()
        {
            PlayClickEffect();
        }

        #endregion

        #region OnClick Methods

        private void OnClick()
        {
            OnButtonClicked();
        }

        #endregion

        #region Utility Methods

        private void CacheComponents()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void StoreOriginalValues()
        {
            _originalScale = transform.localScale;
            _originalRotation = transform.localEulerAngles;

            if (_image != null)
                _originalColor = _image.color;
        }

        private void SetupButton()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnClick);
                _button.onClick.AddListener(OnClick);
            }
        }

        private void PlayClickEffect()
        {
            if (_isPlaying || clickEffect == ButtonClickEffect.None) return;

            CleanupEffects();
            _isPlaying = true;

            if (disableOnClick)
                SetInteractable(false);

            switch (clickEffect)
            {
                case ButtonClickEffect.Scale:
                    PlayScaleEffect();
                    break;
                case ButtonClickEffect.Punch:
                    PlayPunchEffect();
                    break;
                case ButtonClickEffect.Shake:
                    PlayShakeEffect();
                    break;
                case ButtonClickEffect.Rotation:
                    PlayRotationEffect();
                    break;
                case ButtonClickEffect.ColorTint:
                    PlayColorTintEffect();
                    break;
                case ButtonClickEffect.Bounce:
                    PlayBounceEffect();
                    break;
                case ButtonClickEffect.Squeeze:
                    PlaySqueezeEffect();
                    break;
                case ButtonClickEffect.Flash:
                    PlayFlashEffect();
                    break;
                case ButtonClickEffect.Pulse:
                    PlayPulseEffect();
                    break;
            }
        }

        private void PlayScaleEffect()
        {
            _currentEffectHandle = LSequence.Create()
                .Append(LMotion.Create(_originalScale, Vector3.Scale(_originalScale, scaleAmount), animationDuration * 0.5f)
                    .WithEase(easeType)
                    .BindToLocalScale(transform))
                .Append(LMotion.Create(Vector3.Scale(_originalScale, scaleAmount), _originalScale, animationDuration * 0.5f)
                    .WithEase(easeType)
                    .WithOnComplete(OnEffectFinished)
                    .BindToLocalScale(transform))
                .Run();
        }

        private void PlayPunchEffect()
        {
            _currentEffectHandle = LMotion.Punch.Create(Vector3.zero, punchStrength, animationDuration)
                .WithEase(easeType)
                .WithOnComplete(OnEffectFinished)
                .BindToLocalPosition(transform);
        }

        private void PlayShakeEffect()
        {
            _currentEffectHandle = LMotion.Shake.Create(Vector3.zero, shakeStrength, animationDuration)
                .WithEase(easeType)
                .WithOnComplete(OnEffectFinished)
                .BindToLocalPosition(transform);
        }

        private void PlayRotationEffect()
        {
            var targetRotation = _originalRotation + rotationAmount;

            _currentEffectHandle = LSequence.Create()
                .Append(LMotion.Create(_originalRotation, targetRotation, animationDuration * 0.5f)
                    .WithEase(easeType)
                    .BindToLocalEulerAngles(transform))
                .Append(LMotion.Create(targetRotation, _originalRotation, animationDuration * 0.5f)
                    .WithEase(easeType)
                    .WithOnComplete(OnEffectFinished)
                    .BindToLocalEulerAngles(transform))
                .Run();
        }

        private void PlayColorTintEffect()
        {
            if (_image == null)
            {
                OnEffectFinished();
                return;
            }

            _currentEffectHandle = LSequence.Create()
                .Append(LMotion.Create(_originalColor, tintColor, animationDuration * 0.5f)
                    .WithEase(easeType)
                    .BindToColor(_image))
                .Append(LMotion.Create(tintColor, _originalColor, animationDuration * 0.5f)
                    .WithEase(easeType)
                    .WithOnComplete(OnEffectFinished)
                    .BindToColor(_image))
                .Run();
        }

        private void PlayBounceEffect()
        {
            var targetScale = _originalScale * bounceScale;

            _currentEffectHandle = LSequence.Create()
                .Append(LMotion.Create(_originalScale, targetScale, animationDuration * 0.3f)
                    .WithEase(Ease.OutBack)
                    .BindToLocalScale(transform))
                .Append(LMotion.Create(targetScale, _originalScale, animationDuration * 0.7f)
                    .WithEase(Ease.OutBounce)
                    .WithOnComplete(OnEffectFinished)
                    .BindToLocalScale(transform))
                .Run();
        }

        private void PlaySqueezeEffect()
        {
            var targetScale = Vector3.Scale(_originalScale, squeezeScale);

            _currentEffectHandle = LSequence.Create()
                 .Append(LMotion.Create(_originalScale, targetScale, animationDuration * 0.5f)
                     .WithEase(easeType)
                     .BindToLocalScale(transform))
                 .Append(LMotion.Create(targetScale, _originalScale, animationDuration * 0.5f)
                     .WithEase(easeType)
                     .WithOnComplete(OnEffectFinished)
                     .BindToLocalScale(transform))
                .Run();
        }

        private void PlayFlashEffect()
        {
            if (_image == null)
            {
                OnEffectFinished();
                return;
            }

            _currentEffectHandle = LSequence.Create()
                .Append(LMotion.Create(_originalColor, flashColor, animationDuration * 0.2f)
                    .WithEase(Ease.OutQuad)
                    .BindToColor(_image))
                .Append(LMotion.Create(flashColor, _originalColor, animationDuration * 0.8f)
                    .WithEase(Ease.OutQuad)
                    .WithOnComplete(OnEffectFinished)
                    .BindToColor(_image))
                .Run();
        }

        private void PlayPulseEffect()
        {
            var targetScale = _originalScale * pulseScale;

            _currentEffectHandle = LMotion.Create(_originalScale, targetScale, animationDuration)
                .WithEase(Ease.InOutSine)
                .WithLoops(2, LoopType.Yoyo)
                .WithOnComplete(OnEffectFinished)
                .BindToLocalScale(transform);
        }

        private void OnEffectFinished()
        {
            _isPlaying = false;

            if (disableOnClick)
            {
                LMotion.Create(0f, 1f, disableDuration)
                    .WithOnComplete(() => SetInteractable(true))
                    .RunWithoutBinding();
            }

            OnEffectCompleted();
        }

        private void CleanupEffects()
        {
            if (_currentEffectHandle.IsActive())
                _currentEffectHandle.Cancel();
        }

        [Button("Test Effect")]
        private void TestEffect()
        {
            if (Application.isPlaying)
                PlayClickEffect();
        }

        [Button("Reset Transform")]
        private void ResetTransform()
        {
            if (_originalScale != Vector3.zero)
                transform.localScale = _originalScale;

            if (_originalRotation != Vector3.zero)
                transform.localEulerAngles = _originalRotation;

            if (_image != null && _originalColor != Color.clear)
                _image.color = _originalColor;
        }

        #endregion
    }
}