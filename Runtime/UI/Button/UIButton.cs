using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using LitMotion;
using LitMotion.Extensions;
using TriInspector;

namespace ZuyZuy.Workspace
{
    [RequireComponent(typeof(Button))]
    public class UIButton : MonoBehaviour
    {
        #region Variables

        [Title("🎨 Button Effect Configuration")]
        [SerializeField] private bool useUnityDefaultEffect = false;
        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [SerializeField] private bool usePreset = true;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(usePreset))]
        [Group("Preset Selection")]
        [LabelText("Effect Preset")]
        [SerializeField] private ButtonPresetType presetType = ButtonPresetType.WhisperScale;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(usePreset))]
        [ShowIf(nameof(ShouldShowPresetCustomization))]
        [Group("Preset Selection")]
        [SerializeField] private bool allowCustomDuration = false;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(usePreset))]
        [ShowIf(nameof(allowCustomDuration))]
        [Group("Preset Selection")]
        [SerializeField] private float customPresetDuration = 0.2f;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(ShouldShowCustomEffectSettings))]
        [Title("🛠️ Custom Effect Settings")]
        [Group("Custom Effect")]
        [LabelText("Effect Type")]
        [SerializeField] private ButtonClickEffect clickEffect = ButtonClickEffect.Scale;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(ShouldShowScaleSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Vector3 scaleAmount = Vector3.one * 0.9f;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(ShouldShowPunchSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Vector3 punchStrength = Vector3.one * 0.1f;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(ShouldShowShakeSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Vector3 shakeStrength = Vector3.one * 5f;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(ShouldShowRotationSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Vector3 rotationAmount = new Vector3(0, 0, 15f);

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(ShouldShowColorTintSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Color tintColor = Color.gray;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(ShouldShowBounceSettings))]
        [Group("Custom Effect")]
        [SerializeField] private float bounceScale = 1.2f;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(ShouldShowSqueezeSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Vector3 squeezeScale = new Vector3(1.1f, 0.9f, 1f);

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(ShouldShowFlashSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Color flashColor = Color.white;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(ShouldShowPulseSettings))]
        [Group("Custom Effect")]
        [SerializeField] private float pulseScale = 1.1f;

        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(usePreset), false)]
        [Title("⏱️ Custom Animation Settings")]
        [ShowIf(nameof(usePreset), false)]
        [Group("Animation Settings")]
        [SerializeField] private float animationDuration = 0.2f;
        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(usePreset), false)]
        [Group("Animation Settings")]
        [SerializeField] private Ease easeType = Ease.OutQuad;
        [ShowIf(nameof(useUnityDefaultEffect), false)]
        [ShowIf(nameof(usePreset), false)]
        [Group("Animation Settings")]
        [SerializeField] private bool useUnscaledTime = false;

        [Title("🎛️ Interaction Settings")]
        [SerializeField] private float disableDuration = 0f;
        [SerializeField] private bool playAudioOnClick = true;
        [SerializeField] private bool disableOnClick = false;

        [Title("📢 Events")]
        [ShowIf(nameof(disableOnClick), false)]
        [SerializeField] private UnityEvent onButtonClick;
        [ShowIf(nameof(disableOnClick), false)]
        [SerializeField] private UnityEvent onEffectComplete;

        private Button _button;
        private Image _image;
        private Vector3 _originalScale;
        private Vector3 _originalRotation;
        private Color _originalColor;
        private MotionHandle _currentEffectHandle;
        private bool _isPlaying;

        // Current effect configuration (either from preset or custom)
        private ButtonEffectConfig _currentConfig;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            CacheComponents();
            StoreOriginalValues();
            ApplyCurrentConfiguration();
        }

        private void Start()
        {
            SetupButton();
        }

        private void OnDestroy()
        {
            CleanupEffects();
        }

        private void OnValidate()
        {
            // Auto-apply configuration when values change in inspector
            ApplyCurrentConfiguration();
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
            usePreset = false;
            clickEffect = effect;
            ApplyCurrentConfiguration();
        }

        public void SetPreset(ButtonPresetType preset)
        {
            usePreset = true;
            presetType = preset;
            ApplyCurrentConfiguration();
        }

        public void SetPreset(string presetName)
        {
            var config = ButtonEffectPresets.GetPreset(presetName);
            if (config != null)
            {
                usePreset = true;
                _currentConfig = config;
            }
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

        private void ApplyCurrentConfiguration()
        {
            if (useUnityDefaultEffect)
            {
                _currentConfig = null;
                return;
            }
            if (usePreset && presetType != ButtonPresetType.Custom)
            {
                _currentConfig = GetPresetConfig(presetType);
                // Note: _currentConfig will be null for UnityDefault, which is intended

                // Override duration if custom duration is enabled
                if (_currentConfig != null && allowCustomDuration)
                {
                    _currentConfig.duration = customPresetDuration;
                }
            }
            else
            {
                // Use custom settings
                _currentConfig = new ButtonEffectConfig
                {
                    effectType = clickEffect,
                    duration = animationDuration,
                    easeType = easeType,
                    scaleAmount = scaleAmount,
                    punchStrength = punchStrength,
                    shakeStrength = shakeStrength,
                    rotationAmount = rotationAmount,
                    tintColor = tintColor,
                    bounceScale = bounceScale,
                    squeezeScale = squeezeScale,
                    flashColor = flashColor,
                    pulseScale = pulseScale
                };
            }
        }

        private ButtonEffectConfig GetPresetConfig(ButtonPresetType preset)
        {
            return preset switch
            {
                ButtonPresetType.UnityDefault => null, // No animation, use Unity's default button behavior
                ButtonPresetType.WhisperScale => ButtonEffectPresets.WHISPER_SCALE,
                ButtonPresetType.SilkSqueeze => ButtonEffectPresets.SILK_SQUEEZE,
                ButtonPresetType.VelvetPulse => ButtonEffectPresets.VELVET_PULSE,
                ButtonPresetType.PearlFlash => ButtonEffectPresets.PEARL_FLASH,

                ButtonPresetType.SpringBounce => ButtonEffectPresets.SPRING_BOUNCE,
                ButtonPresetType.NovaFlash => ButtonEffectPresets.NOVA_FLASH,
                ButtonPresetType.ElectricShake => ButtonEffectPresets.ELECTRIC_SHAKE,
                ButtonPresetType.CosmicSpin => ButtonEffectPresets.COSMIC_SPIN,

                ButtonPresetType.ThunderPunch => ButtonEffectPresets.THUNDER_PUNCH,
                ButtonPresetType.MeteorBounce => ButtonEffectPresets.METEOR_BOUNCE,
                ButtonPresetType.EclipseTint => ButtonEffectPresets.ECLIPSE_TINT,
                ButtonPresetType.SupernovaPulse => ButtonEffectPresets.SUPERNOVA_PULSE,

                ButtonPresetType.RoseGoldFlash => ButtonEffectPresets.ROSE_GOLD_FLASH,
                ButtonPresetType.OceanTint => ButtonEffectPresets.OCEAN_TINT,
                ButtonPresetType.ForestSqueeze => ButtonEffectPresets.FOREST_SQUEEZE,
                ButtonPresetType.SunsetFlash => ButtonEffectPresets.SUNSET_FLASH,

                ButtonPresetType.PowerUpBounce => ButtonEffectPresets.POWER_UP_BOUNCE,
                ButtonPresetType.ComboShake => ButtonEffectPresets.COMBO_SHAKE,
                ButtonPresetType.LevelUpSpin => ButtonEffectPresets.LEVEL_UP_SPIN,
                ButtonPresetType.CrystalPulse => ButtonEffectPresets.CRYSTAL_PULSE,

                _ => ButtonEffectPresets.WHISPER_SCALE
            };
        }

        private void PlayClickEffect()
        {
            if (_isPlaying) return;

            // Handle Unity default behavior (no custom animation)
            if (_currentConfig == null)
            {
                if (playAudioOnClick)
                {
                    // TODO: Play button click audio if needed
                }
                return;
            }

            if (_currentConfig.effectType == ButtonClickEffect.None) return;

            CleanupEffects();
            _isPlaying = true;

            if (disableOnClick)
                SetInteractable(false);

            switch (_currentConfig.effectType)
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
            var scaleTo = Vector3.Scale(_originalScale, _currentConfig.scaleAmount);
            var duration = _currentConfig.duration;
            var ease = _currentConfig.easeType;

            _currentEffectHandle = LSequence.Create()
                .Append(LMotion.Create(_originalScale, scaleTo, duration * 0.5f)
                    .WithEase(ease)
                    .BindToLocalScale(transform))
                .Append(LMotion.Create(scaleTo, _originalScale, duration * 0.5f)
                    .WithEase(ease)
                    .WithOnComplete(OnEffectFinished)
                    .BindToLocalScale(transform))
                .Run();
        }

        private void PlayPunchEffect()
        {
            _currentEffectHandle = LMotion.Punch.Create(Vector3.zero, _currentConfig.punchStrength, _currentConfig.duration)
                .WithEase(_currentConfig.easeType)
                .WithOnComplete(OnEffectFinished)
                .BindToLocalPosition(transform);
        }

        private void PlayShakeEffect()
        {
            _currentEffectHandle = LMotion.Shake.Create(Vector3.zero, _currentConfig.shakeStrength, _currentConfig.duration)
                .WithEase(_currentConfig.easeType)
                .WithOnComplete(OnEffectFinished)
                .BindToLocalPosition(transform);
        }

        private void PlayRotationEffect()
        {
            var targetRotation = _originalRotation + _currentConfig.rotationAmount;
            var duration = _currentConfig.duration;
            var ease = _currentConfig.easeType;

            _currentEffectHandle = LSequence.Create()
                .Append(LMotion.Create(_originalRotation, targetRotation, duration * 0.5f)
                    .WithEase(ease)
                    .BindToLocalEulerAngles(transform))
                .Append(LMotion.Create(targetRotation, _originalRotation, duration * 0.5f)
                    .WithEase(ease)
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

            var duration = _currentConfig.duration;
            var ease = _currentConfig.easeType;
            var tintColor = _currentConfig.tintColor;

            _currentEffectHandle = LSequence.Create()
                .Append(LMotion.Create(_originalColor, tintColor, duration * 0.5f)
                    .WithEase(ease)
                    .BindToColor(_image))
                .Append(LMotion.Create(tintColor, _originalColor, duration * 0.5f)
                    .WithEase(ease)
                    .WithOnComplete(OnEffectFinished)
                    .BindToColor(_image))
                .Run();
        }

        private void PlayBounceEffect()
        {
            var targetScale = _originalScale * _currentConfig.bounceScale;
            var duration = _currentConfig.duration;

            _currentEffectHandle = LSequence.Create()
                .Append(LMotion.Create(_originalScale, targetScale, duration * 0.3f)
                    .WithEase(Ease.OutBack)
                    .BindToLocalScale(transform))
                .Append(LMotion.Create(targetScale, _originalScale, duration * 0.7f)
                    .WithEase(Ease.OutBounce)
                    .WithOnComplete(OnEffectFinished)
                    .BindToLocalScale(transform))
                .Run();
        }

        private void PlaySqueezeEffect()
        {
            var targetScale = Vector3.Scale(_originalScale, _currentConfig.squeezeScale);
            var duration = _currentConfig.duration;
            var ease = _currentConfig.easeType;

            _currentEffectHandle = LSequence.Create()
                 .Append(LMotion.Create(_originalScale, targetScale, duration * 0.5f)
                     .WithEase(ease)
                     .BindToLocalScale(transform))
                 .Append(LMotion.Create(targetScale, _originalScale, duration * 0.5f)
                     .WithEase(ease)
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

            var duration = _currentConfig.duration;
            var flashColor = _currentConfig.flashColor;

            _currentEffectHandle = LSequence.Create()
                .Append(LMotion.Create(_originalColor, flashColor, duration * 0.2f)
                    .WithEase(Ease.OutQuad)
                    .BindToColor(_image))
                .Append(LMotion.Create(flashColor, _originalColor, duration * 0.8f)
                    .WithEase(Ease.OutQuad)
                    .WithOnComplete(OnEffectFinished)
                    .BindToColor(_image))
                .Run();
        }

        private void PlayPulseEffect()
        {
            var targetScale = _originalScale * _currentConfig.pulseScale;

            _currentEffectHandle = LMotion.Create(_originalScale, targetScale, _currentConfig.duration)
                .WithEase(_currentConfig.easeType)
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

        #endregion

        #region Inspector Helper Methods

        private bool ShouldShowScaleSettings()
        {
            return !usePreset && clickEffect == ButtonClickEffect.Scale || (ShouldShowCustomEffectSettings() && clickEffect == ButtonClickEffect.Scale);
        }

        private bool ShouldShowPunchSettings()
        {
            return !usePreset && clickEffect == ButtonClickEffect.Punch || (ShouldShowCustomEffectSettings() && clickEffect == ButtonClickEffect.Punch);
        }

        private bool ShouldShowShakeSettings()
        {
            return !usePreset && clickEffect == ButtonClickEffect.Shake || (ShouldShowCustomEffectSettings() && clickEffect == ButtonClickEffect.Shake);
        }

        private bool ShouldShowRotationSettings()
        {
            return !usePreset && clickEffect == ButtonClickEffect.Rotation || (ShouldShowCustomEffectSettings() && clickEffect == ButtonClickEffect.Rotation);
        }

        private bool ShouldShowColorTintSettings()
        {
            return !usePreset && clickEffect == ButtonClickEffect.ColorTint || (ShouldShowCustomEffectSettings() && clickEffect == ButtonClickEffect.ColorTint);
        }

        private bool ShouldShowBounceSettings()
        {
            return !usePreset && clickEffect == ButtonClickEffect.Bounce || (ShouldShowCustomEffectSettings() && clickEffect == ButtonClickEffect.Bounce);
        }

        private bool ShouldShowSqueezeSettings()
        {
            return !usePreset && clickEffect == ButtonClickEffect.Squeeze || (ShouldShowCustomEffectSettings() && clickEffect == ButtonClickEffect.Squeeze);
        }

        private bool ShouldShowFlashSettings()
        {
            return !usePreset && clickEffect == ButtonClickEffect.Flash || (ShouldShowCustomEffectSettings() && clickEffect == ButtonClickEffect.Flash);
        }

        private bool ShouldShowPulseSettings()
        {
            return !usePreset && clickEffect == ButtonClickEffect.Pulse || (ShouldShowCustomEffectSettings() && clickEffect == ButtonClickEffect.Pulse);
        }

        private bool ShouldShowCustomEffectSettings()
        {
            return !usePreset || presetType == ButtonPresetType.Custom;
        }

        private bool IsUnityDefaultSelected()
        {
            return usePreset && presetType == ButtonPresetType.UnityDefault;
        }

        // Add this helper for button visibility
        private bool ShouldShowTestButtons()
        {
            return !IsUnityDefaultSelected() && !useUnityDefaultEffect;
        }

        private bool ShouldShowPresetCustomization()
        {
            return usePreset && presetType != ButtonPresetType.UnityDefault && presetType != ButtonPresetType.Custom;
        }

        #endregion

        #region Button Test Methods

        [ShowIf(nameof(ShouldShowTestButtons))]
        [Button(ButtonSizes.Large, "🎭 Test Current Effect")]
        private void TestEffect()
        {
#if UNITY_EDITOR
            // Ensure components are cached
            if (_button == null || _image == null)
                CacheComponents();

            // Store original values if not already stored
            if (_originalScale == Vector3.zero)
            {
                _originalScale = transform.localScale;
                _originalRotation = transform.localEulerAngles;

                if (_image != null)
                    _originalColor = _image.color;
            }

            // Apply current configuration
            ApplyCurrentConfiguration();

            // Show message for Unity default
            if (_currentConfig == null)
            {
                Debug.Log("🔲 Unity Default: No custom animation to test. Uses standard Unity button behavior.");
                return;
            }

            // Play the effect
            PlayClickEffect();
#endif
        }

        [ShowIf(nameof(ShouldShowTestButtons))]
        [Button("🎨 Apply Random Preset")]
        private void ApplyRandomPreset()
        {
#if UNITY_EDITOR
            var presets = System.Enum.GetValues(typeof(ButtonPresetType));
            var randomIndex = UnityEngine.Random.Range(2, presets.Length); // Skip Custom and UnityDefault
            var randomPreset = (ButtonPresetType)presets.GetValue(randomIndex);

            SetPreset(randomPreset);
            Debug.Log($"Applied random preset: {randomPreset}");
#endif
        }

        #endregion
    }
}