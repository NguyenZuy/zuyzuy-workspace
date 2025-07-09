using System;
using UnityEngine;
using LitMotion;
using LitMotion.Extensions;
using TriInspector;
using System.Collections.Generic;
using System.Collections;

namespace ZuyZuy.Workspace
{
    public sealed class TabParent : MonoBehaviour
    {
        #region Variables

        [Title("🎛️ Tab Configuration")]
        [SerializeField] private Tab _firstSelectedTab;

        [Title("🎨 Visual Style Configuration")]
        [SerializeField] private bool useAdvancedStyling = false;

        [ShowIf(nameof(useAdvancedStyling))]
        [Group("Advanced Styling")]
        [SerializeField] private bool enableGradientColors = false;

        [ShowIf(nameof(useAdvancedStyling))]
        [ShowIf(nameof(enableGradientColors))]
        [Group("Advanced Styling")]
        [SerializeField] private Gradient activeImageGradient = new Gradient();

        [ShowIf(nameof(useAdvancedStyling))]
        [ShowIf(nameof(enableGradientColors))]
        [Group("Advanced Styling")]
        [SerializeField] private Gradient inactiveImageGradient = new Gradient();

        [ShowIf(nameof(useAdvancedStyling))]
        [Group("Advanced Styling")]
        [SerializeField] private bool enableTextOutline = false;

        [ShowIf(nameof(useAdvancedStyling))]
        [ShowIf(nameof(enableTextOutline))]
        [Group("Advanced Styling")]
        [SerializeField] private Color activeTextOutlineColor = Color.black;

        [ShowIf(nameof(useAdvancedStyling))]
        [ShowIf(nameof(enableTextOutline))]
        [Group("Advanced Styling")]
        [SerializeField] private Color inactiveTextOutlineColor = Color.gray;

        [ShowIf(nameof(useAdvancedStyling))]
        [ShowIf(nameof(enableTextOutline))]
        [Group("Advanced Styling")]
        [SerializeField] private float textOutlineWidth = 0.2f;

        [ShowIf(nameof(useAdvancedStyling))]
        [Group("Advanced Styling")]
        [SerializeField] private bool enableTextShadow = false;

        [ShowIf(nameof(useAdvancedStyling))]
        [ShowIf(nameof(enableTextShadow))]
        [Group("Advanced Styling")]
        [SerializeField] private Color activeTextShadowColor = new Color(0f, 0f, 0f, 0.5f);

        [ShowIf(nameof(useAdvancedStyling))]
        [ShowIf(nameof(enableTextShadow))]
        [Group("Advanced Styling")]
        [SerializeField] private Color inactiveTextShadowColor = new Color(0f, 0f, 0f, 0.3f);

        [ShowIf(nameof(useAdvancedStyling))]
        [ShowIf(nameof(enableTextShadow))]
        [Group("Advanced Styling")]
        [SerializeField] private Vector2 textShadowOffset = new Vector2(1f, -1f);

        [Title("🎭 Tab Effect Configuration")]
        [SerializeField] private bool usePreset = true;

        [ShowIf(nameof(usePreset))]
        [Group("Preset Selection")]
        [LabelText("Effect Preset")]
        [SerializeField] private TabPresetType presetType = TabPresetType.WhisperScale;

        [ShowIf(nameof(usePreset))]
        [ShowIf(nameof(ShouldShowPresetCustomization))]
        [Group("Preset Selection")]
        [SerializeField] private bool allowCustomDuration = false;

        [ShowIf(nameof(usePreset))]
        [ShowIf(nameof(allowCustomDuration))]
        [Group("Preset Selection")]
        [SerializeField] private float customPresetDuration = 0.3f;

        [ShowIf(nameof(ShouldShowCustomEffectSettings))]
        [Title("🛠️ Custom Effect Settings")]
        [Group("Custom Effect")]
        [LabelText("Effect Type")]
        [SerializeField] private TabEffectType customEffectType = TabEffectType.Scale;

        [ShowIf(nameof(ShouldShowCustomEffectSettings))]
        [Group("Custom Effect")]
        [SerializeField] private float customDuration = 0.3f;

        [ShowIf(nameof(ShouldShowCustomEffectSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Ease customEaseType = Ease.OutQuad;

        [ShowIf(nameof(ShouldShowScaleSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Vector3 customScaleAmount = new Vector3(1.1f, 1.1f, 1f);

        [ShowIf(nameof(ShouldShowColorSettings))]
        [Title("🎨 Color Palette")]
        [Group("Color Settings")]
        [LabelText("Active Image")]
        [SerializeField] private Color customActiveImageColor = Color.white;
        [ShowIf(nameof(ShouldShowColorSettings))]
        [Group("Color Settings")]
        [LabelText("Inactive Image")]
        [SerializeField] private Color customInactiveImageColor = new Color(0.7f, 0.7f, 0.7f, 1f);
        [ShowIf(nameof(ShouldShowColorSettings))]
        [Group("Color Settings")]
        [LabelText("Active Text")]
        [SerializeField] private Color customActiveTextColor = Color.black;
        [ShowIf(nameof(ShouldShowColorSettings))]
        [Group("Color Settings")]
        [LabelText("Inactive Text")]
        [SerializeField] private Color customInactiveTextColor = new Color(0.5f, 0.5f, 0.5f, 1f);

        [ShowIf(nameof(ShouldShowColorSettings))]
        [Group("Color Settings")]
        [LabelText("Hover Image")]
        [SerializeField] private Color customHoverImageColor = new Color(0.9f, 0.9f, 0.9f, 1f);

        [ShowIf(nameof(ShouldShowColorSettings))]
        [Group("Color Settings")]
        [LabelText("Hover Text")]
        [SerializeField] private Color customHoverTextColor = new Color(0.3f, 0.3f, 0.3f, 1f);

        [ShowIf(nameof(ShouldShowGlowSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Color customGlowColor = new Color(1f, 1f, 0f, 0.5f);

        [ShowIf(nameof(ShouldShowSlideSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Vector2 customSlideOffset = new Vector2(0, 10f);

        [ShowIf(nameof(ShouldShowFadeSettings))]
        [Group("Custom Effect")]
        [SerializeField] private float customFadeAlpha = 0.5f;

        [ShowIf(nameof(ShouldShowPulseSettings))]
        [Group("Custom Effect")]
        [SerializeField] private float customPulseScale = 1.15f;

        [ShowIf(nameof(ShouldShowShakeSettings))]
        [Group("Custom Effect")]
        [SerializeField] private Vector3 customShakeStrength = new Vector3(2f, 0f, 0f);

        [Title("🔊 Audio Configuration")]
        [SerializeField] private bool enableTabSounds = false;

        [ShowIf(nameof(enableTabSounds))]
        [Group("Audio Settings")]
        [SerializeField] private AudioClip tabClickSound;

        [ShowIf(nameof(enableTabSounds))]
        [Group("Audio Settings")]
        [SerializeField] private AudioClip tabHoverSound;

        [ShowIf(nameof(enableTabSounds))]
        [Group("Audio Settings")]
        [SerializeField] private float audioVolume = 1f;

        [Title("🎯 Interaction Settings")]
        [SerializeField] private bool enableHoverEffects = true;

        [ShowIf(nameof(enableHoverEffects))]
        [Group("Hover Effects")]
        [SerializeField] private float hoverTransitionDuration = 0.15f;

        [ShowIf(nameof(enableHoverEffects))]
        [Group("Hover Effects")]
        [SerializeField] private Vector3 hoverScaleMultiplier = new Vector3(1.05f, 1.05f, 1f);

        [ShowIf(nameof(enableHoverEffects))]
        [Group("Hover Effects")]
        [SerializeField] private bool enableHoverColorChange = true;

        public event Action<int> OnChangeTab;
        public event Action<Tab> OnTabHover;
        public event Action<Tab> OnTabUnhover;

        // Current effect configuration (either from preset or custom)
        private TabEffectConfig _currentConfig;
        private Tab _curTab;
        private Dictionary<Tab, MotionHandle> _activeAnimations = new Dictionary<Tab, MotionHandle>();
        private Dictionary<Tab, MotionHandle> _maintainEffects = new Dictionary<Tab, MotionHandle>();
        private Dictionary<Tab, MotionHandle> _hoverAnimations = new Dictionary<Tab, MotionHandle>();
        private AudioSource _audioSource;

        private List<Tab> _tabs = new List<Tab>();

        #endregion

        #region Unity Methods

        private void Start()
        {
            InitializeTabsList();
            InitializeAudio();
            ApplyCurrentConfiguration();
            ResetToDefault();
            SetupTabHoverEvents();
        }

        private void OnEnable()
        {
            ResetToDefault();
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (_tabs.Count == 0)
            {
                _tabs.Clear();
                var tabs = GetComponentsInChildren<Tab>(true);
                _tabs.AddRange(tabs);
            }
#endif
            ApplyCurrentConfiguration();
        }

        private void OnDestroy()
        {
            CleanupAllAnimations();
        }

        #endregion

        #region UI Methods

        public void SetPreset(TabPresetType preset)
        {
            usePreset = true;
            presetType = preset;
            ApplyCurrentConfiguration();
        }

        public void SetCustomEffect(TabEffectType effectType)
        {
            usePreset = false;
            customEffectType = effectType;
            ApplyCurrentConfiguration();
        }

        public void SetActive(Tab tab, bool isActive)
        {
            if (tab == _curTab && isActive)
                return;

            PlayTabSound(tabClickSound);
            OnChangeTab?.Invoke(tab.Id);

            // Stop maintain effect for previous tab
            if (_curTab != null)
            {
                StopMaintainEffect(_curTab);
                SetTabState(_curTab, false);
            }

            _curTab = isActive ? tab : _firstSelectedTab;

            if (_curTab != null)
            {
                SetTabState(_curTab, true);
                StartMaintainEffectIfNeeded(_curTab);
            }
        }

        public void SetTabColors(Color activeImage, Color inactiveImage, Color activeText, Color inactiveText)
        {
            customActiveImageColor = activeImage;
            customInactiveImageColor = inactiveImage;
            customActiveTextColor = activeText;
            customInactiveTextColor = inactiveText;
            ApplyCurrentConfiguration();
        }

        public void SetHoverColors(Color hoverImage, Color hoverText)
        {
            customHoverImageColor = hoverImage;
            customHoverTextColor = hoverText;
            ApplyCurrentConfiguration();
        }

        public void EnableAdvancedStyling(bool enable)
        {
            useAdvancedStyling = enable;
            ApplyCurrentConfiguration();
        }

        #endregion

        #region Utility Methods

        private void InitializeTabsList()
        {
            _tabs.Clear();
            var tabs = GetComponentsInChildren<Tab>();
            _tabs.AddRange(tabs);

#if UNITY_EDITOR
            Debug.Log($"🎯 Initialized {_tabs.Count} tabs for testing");
#endif
        }

        private void InitializeAudio()
        {
            if (enableTabSounds && _audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
                _audioSource.playOnAwake = false;
                _audioSource.volume = audioVolume;
            }
        }

        private void SetupTabHoverEvents()
        {
            if (!enableHoverEffects) return;

            if (_tabs.Count == 0)
                InitializeTabsList();

            foreach (var tab in _tabs)
            {
                var eventTrigger = tab.GetComponent<UnityEngine.EventSystems.EventTrigger>();
                if (eventTrigger == null)
                    eventTrigger = tab.gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();

                // Hover enter
                var pointerEnter = new UnityEngine.EventSystems.EventTrigger.Entry
                {
                    eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter
                };
                pointerEnter.callback.AddListener((data) => OnTabHoverEnter(tab));
                eventTrigger.triggers.Add(pointerEnter);

                // Hover exit
                var pointerExit = new UnityEngine.EventSystems.EventTrigger.Entry
                {
                    eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit
                };
                pointerExit.callback.AddListener((data) => OnTabHoverExit(tab));
                eventTrigger.triggers.Add(pointerExit);
            }
        }

        private void OnTabHoverEnter(Tab tab)
        {
            if (tab == _curTab) return; // Don't hover effect on active tab

            PlayTabSound(tabHoverSound);
            OnTabHover?.Invoke(tab);

            StopHoverAnimation(tab);

            if (enableHoverColorChange)
            {
                // Apply hover colors
                tab.SetImageColor(customHoverImageColor);
                tab.SetTextColor(customHoverTextColor);
            }

            // Apply hover scale
            var targetScale = Vector3.Scale(tab.transform.localScale, hoverScaleMultiplier);
            var handle = LMotion.Create(tab.transform.localScale, targetScale, hoverTransitionDuration)
                .WithEase(Ease.OutQuad)
                .BindToLocalScale(tab.transform);

            _hoverAnimations[tab] = handle;
        }

        private void OnTabHoverExit(Tab tab)
        {
            if (tab == _curTab) return; // Don't hover effect on active tab

            OnTabUnhover?.Invoke(tab);

            StopHoverAnimation(tab);

            if (enableHoverColorChange)
            {
                // Restore original colors
                ApplyColors(tab, false);
            }

            // Restore original scale
            var originalScale = Vector3.one; // Assuming default scale is Vector3.one
            var handle = LMotion.Create(tab.transform.localScale, originalScale, hoverTransitionDuration)
                .WithEase(Ease.OutQuad)
                .BindToLocalScale(tab.transform);

            _hoverAnimations[tab] = handle;
        }

        private void StopHoverAnimation(Tab tab)
        {
            if (_hoverAnimations.TryGetValue(tab, out var handle))
            {
                handle.TryCancel();
                _hoverAnimations.Remove(tab);
            }
        }

        private void PlayTabSound(AudioClip clip)
        {
            if (!enableTabSounds || _audioSource == null || clip == null) return;

            _audioSource.clip = clip;
            _audioSource.volume = audioVolume;
            _audioSource.Play();
        }

        private void ApplyCurrentConfiguration()
        {
            if (usePreset && presetType != TabPresetType.Custom)
            {
                _currentConfig = GetPresetConfig(presetType);

                // Override duration if custom duration is enabled
                if (_currentConfig != null && allowCustomDuration)
                {
                    _currentConfig.duration = customPresetDuration;
                }
            }
            else
            {
                // Use custom settings
                _currentConfig = new TabEffectConfig
                {
                    effectType = customEffectType,
                    duration = customDuration,
                    easeType = customEaseType,
                    scaleAmount = customScaleAmount,
                    activeImageColor = customActiveImageColor,
                    inactiveImageColor = customInactiveImageColor,
                    activeTextColor = customActiveTextColor,
                    inactiveTextColor = customInactiveTextColor,
                    glowColor = customGlowColor,
                    slideOffset = customSlideOffset,
                    fadeAlpha = customFadeAlpha,
                    pulseScale = customPulseScale,
                    shakeStrength = customShakeStrength
                };
            }

            // Apply advanced styling if enabled
            if (useAdvancedStyling)
            {
                ApplyAdvancedStyling();
            }
        }

        private void ApplyAdvancedStyling()
        {
            if (_tabs.Count == 0)
                InitializeTabsList();

            foreach (var tab in _tabs)
            {
                ApplyAdvancedTabStyling(tab, tab == _curTab);
            }
        }

        private void ApplyAdvancedTabStyling(Tab tab, bool isActive)
        {
            // Apply gradient colors if enabled
            if (enableGradientColors)
            {
                var gradient = isActive ? activeImageGradient : inactiveImageGradient;
                var color = gradient.Evaluate(0.5f); // Sample middle of gradient
                tab.SetImageColor(color);
            }

            // Apply text effects would require extending Tab class to support these features
            // For now, we'll document this as a future enhancement
        }

        private TabEffectConfig GetPresetConfig(TabPresetType preset)
        {
            return preset switch
            {
                TabPresetType.WhisperScale => TabEffectPresets.WHISPER_SCALE,
                TabPresetType.SilkGlow => TabEffectPresets.SILK_GLOW,
                TabPresetType.VelvetFade => TabEffectPresets.VELVET_FADE,
                TabPresetType.PearlSlide => TabEffectPresets.PEARL_SLIDE,
                TabPresetType.SpringBounce => TabEffectPresets.SPRING_BOUNCE,
                TabPresetType.SmoothPulse => TabEffectPresets.SMOOTH_PULSE,
                TabPresetType.GentleShake => TabEffectPresets.GENTLE_SHAKE,
                TabPresetType.FlowTransition => TabEffectPresets.FLOW_TRANSITION,
                TabPresetType.PowerScale => TabEffectPresets.POWER_SCALE,
                TabPresetType.NovaGlow => TabEffectPresets.NOVA_GLOW,
                TabPresetType.QuantumFade => TabEffectPresets.QUANTUM_FADE,
                TabPresetType.LightningSlide => TabEffectPresets.LIGHTNING_SLIDE,
                TabPresetType.GoldenGlow => TabEffectPresets.GOLDEN_GLOW,
                TabPresetType.SapphireFade => TabEffectPresets.SAPPHIRE_FADE,
                TabPresetType.EmeraldScale => TabEffectPresets.EMERALD_SCALE,
                TabPresetType.RubyPulse => TabEffectPresets.RUBY_PULSE,
                TabPresetType.LevelUpScale => TabEffectPresets.LEVEL_UP_SCALE,
                TabPresetType.QuestGlow => TabEffectPresets.QUEST_GLOW,
                TabPresetType.InventorySlide => TabEffectPresets.INVENTORY_SLIDE,
                TabPresetType.SkillFade => TabEffectPresets.SKILL_FADE,
                _ => TabEffectPresets.WHISPER_SCALE
            };
        }

        private void ResetToDefault()
        {
            if (_curTab != null)
            {
                StopMaintainEffect(_curTab);
                SetTabState(_curTab, false);
            }

            _curTab = _firstSelectedTab;

            if (_curTab != null)
            {
                SetTabState(_curTab, true);
                StartMaintainEffectIfNeeded(_curTab);
            }
        }

        private void SetTabState(Tab tab, bool isActive)
        {
            if (tab == null || _currentConfig == null)
                return;

            try
            {
                // Stop any existing animation for this tab
                StopTabAnimation(tab);

                // Apply effect based on configuration
                switch (_currentConfig.effectType)
                {
                    case TabEffectType.Scale:
                        PlayScaleEffect(tab, isActive);
                        break;
                    case TabEffectType.Bounce:
                        PlayBounceEffect(tab, isActive);
                        break;
                    case TabEffectType.Glow:
                        PlayGlowEffect(tab, isActive);
                        break;
                    case TabEffectType.Slide:
                        PlaySlideEffect(tab, isActive);
                        break;
                    case TabEffectType.Fade:
                        PlayFadeEffect(tab, isActive);
                        break;
                    case TabEffectType.Pulse:
                        PlayPulseEffect(tab, isActive);
                        break;
                    case TabEffectType.Shake:
                        PlayShakeEffect(tab, isActive);
                        break;
                    case TabEffectType.ColorTransition:
                        PlayColorTransitionEffect(tab, isActive);
                        break;
                    case TabEffectType.SpriteSwap:
                        PlaySpriteSwapEffect(tab, isActive);
                        break;
                    case TabEffectType.None:
                        // Just apply static state without animation
                        ApplyStaticState(tab, isActive);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error updating tab state: {ex}");
            }
        }

        private void PlayScaleEffect(Tab tab, bool isActive)
        {
            var targetScale = isActive ? _currentConfig.scaleAmount : _currentConfig.inactiveScale;
            var currentScale = tab.transform.localScale;

            var handle = LMotion.Create(currentScale, targetScale, _currentConfig.duration)
                .WithEase(_currentConfig.easeType)
                .WithOnComplete(() => RemoveTabAnimation(tab))
                .BindToLocalScale(tab.transform);

            _activeAnimations[tab] = handle;
            ApplyColors(tab, isActive);
        }

        private void PlayBounceEffect(Tab tab, bool isActive)
        {
            var targetScale = isActive ? _currentConfig.scaleAmount : _currentConfig.inactiveScale;
            var bounceScale = targetScale * 1.2f;

            var handle = LSequence.Create()
                .Append(LMotion.Create(tab.transform.localScale, bounceScale, _currentConfig.duration * 0.3f)
                    .WithEase(Ease.OutQuad)
                    .BindToLocalScale(tab.transform))
                .Append(LMotion.Create(bounceScale, targetScale, _currentConfig.duration * 0.7f)
                    .WithEase(Ease.OutBounce)
                    .WithOnComplete(() => RemoveTabAnimation(tab))
                    .BindToLocalScale(tab.transform))
                .Run();

            _activeAnimations[tab] = handle;
            ApplyColors(tab, isActive);
        }

        private void PlayGlowEffect(Tab tab, bool isActive)
        {
            // Apply scale and color changes with glow
            ApplyColors(tab, isActive);

            // For glow effect, we could add a background glow image if available
            // This is a simplified version that just applies color changes
            var targetColor = isActive ? _currentConfig.glowColor : Color.clear;

            // If tab has a glow component, animate it
            // For now, just do a simple color transition
            PlayColorTransitionEffect(tab, isActive);
        }

        private void PlaySlideEffect(Tab tab, bool isActive)
        {
            var rectTransform = tab.GetComponent<RectTransform>();
            if (rectTransform == null) return;

            var originalPos = rectTransform.anchoredPosition;
            var offset = isActive ? Vector2.zero : _currentConfig.slideOffset;

            var handle = LMotion.Create(originalPos, originalPos + offset, _currentConfig.duration)
                .WithEase(_currentConfig.easeType)
                .WithOnComplete(() => RemoveTabAnimation(tab))
                .BindToAnchoredPosition(rectTransform);

            _activeAnimations[tab] = handle;
            ApplyColors(tab, isActive);
        }

        private void PlayFadeEffect(Tab tab, bool isActive)
        {
            var canvasGroup = tab.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = tab.gameObject.AddComponent<CanvasGroup>();

            var targetAlpha = isActive ? 1f : _currentConfig.fadeAlpha;

            var handle = LMotion.Create(canvasGroup.alpha, targetAlpha, _currentConfig.duration)
                .WithEase(_currentConfig.easeType)
                .WithOnComplete(() => RemoveTabAnimation(tab))
                .BindToAlpha(canvasGroup);

            _activeAnimations[tab] = handle;
            ApplyColors(tab, isActive);
        }

        private void PlayPulseEffect(Tab tab, bool isActive)
        {
            if (isActive)
            {
                var targetScale = Vector3.Scale(_currentConfig.inactiveScale, new Vector3(_currentConfig.pulseScale, _currentConfig.pulseScale, 1f));

                var handle = LMotion.Create(_currentConfig.inactiveScale, targetScale, _currentConfig.duration)
                    .WithEase(_currentConfig.easeType)
                    .WithLoops(-1, LoopType.Yoyo)
                    .BindToLocalScale(tab.transform);

                _activeAnimations[tab] = handle;
            }
            else
            {
                // Return to normal scale
                var handle = LMotion.Create(tab.transform.localScale, _currentConfig.inactiveScale, _currentConfig.duration)
                    .WithEase(_currentConfig.easeType)
                    .WithOnComplete(() => RemoveTabAnimation(tab))
                    .BindToLocalScale(tab.transform);

                _activeAnimations[tab] = handle;
            }

            ApplyColors(tab, isActive);
        }

        private void PlayShakeEffect(Tab tab, bool isActive)
        {
            if (isActive)
            {
                var handle = LMotion.Shake.Create(Vector3.zero, _currentConfig.shakeStrength, _currentConfig.duration)
                    .WithEase(_currentConfig.easeType)
                    .WithOnComplete(() => RemoveTabAnimation(tab))
                    .BindToLocalPosition(tab.transform);

                _activeAnimations[tab] = handle;
            }

            ApplyColors(tab, isActive);
        }

        private void PlayColorTransitionEffect(Tab tab, bool isActive)
        {
            // Animate image color
            tab.AnimateImageColor(isActive, _currentConfig);

            // Animate text color  
            tab.AnimateTextColor(isActive, _currentConfig);
        }

        private void PlaySpriteSwapEffect(Tab tab, bool isActive)
        {
            // Simple sprite swap with scale animation
            tab.UpdateImageSprite(isActive);
            PlayScaleEffect(tab, isActive);
        }

        private void ApplyStaticState(Tab tab, bool isActive)
        {
            tab.UpdateImageSprite(isActive);
            tab.UpdateImageColor(isActive, _currentConfig);
            tab.UpdateTextColor(isActive, _currentConfig);
            tab.UpdateTextString(isActive);
        }

        private void ApplyColors(Tab tab, bool isActive)
        {
            tab.UpdateImageColor(isActive, _currentConfig);
            tab.UpdateTextColor(isActive, _currentConfig);
        }

        private void StartMaintainEffectIfNeeded(Tab tab)
        {
            if (_currentConfig == null || !_currentConfig.maintainEffectWhileActive) return;

            switch (_currentConfig.effectType)
            {
                case TabEffectType.Pulse:
                    StartMaintainPulse(tab);
                    break;
                case TabEffectType.Glow:
                    StartMaintainGlow(tab);
                    break;
            }
        }

        private void StartMaintainPulse(Tab tab)
        {
            var targetScale = Vector3.Scale(_currentConfig.scaleAmount, new Vector3(_currentConfig.pulseScale, _currentConfig.pulseScale, 1f));

            var handle = LMotion.Create(_currentConfig.scaleAmount, targetScale, 1f / _currentConfig.maintainEffectPulseSpeed)
                .WithEase(Ease.InOutSine)
                .WithLoops(-1, LoopType.Yoyo)
                .BindToLocalScale(tab.transform);

            _maintainEffects[tab] = handle;
        }

        private void StartMaintainGlow(Tab tab)
        {
            // Maintain glow effect - could animate glow intensity  
            var baseColor = _currentConfig.activeImageColor;
            var glowColor = Color.Lerp(baseColor, _currentConfig.glowColor, 0.5f);

            // Simple glow effect using color animation
            tab.AnimateImageColor(true, _currentConfig);
        }

        private void StopMaintainEffect(Tab tab)
        {
            if (_maintainEffects.TryGetValue(tab, out var handle))
            {
                handle.TryCancel();
                _maintainEffects.Remove(tab);
            }
        }

        private void StopTabAnimation(Tab tab)
        {
            if (_activeAnimations.TryGetValue(tab, out var handle))
            {
                handle.TryCancel();
                _activeAnimations.Remove(tab);
            }
        }

        private void RemoveTabAnimation(Tab tab)
        {
            _activeAnimations.Remove(tab);
        }

        private void CleanupAllAnimations()
        {
            foreach (var handle in _activeAnimations.Values)
            {
                handle.TryCancel();
            }
            _activeAnimations.Clear();

            foreach (var handle in _maintainEffects.Values)
            {
                handle.TryCancel();
            }
            _maintainEffects.Clear();

            foreach (var handle in _hoverAnimations.Values)
            {
                handle.TryCancel();
            }
            _hoverAnimations.Clear();
        }

        #endregion

        #region Inspector Helper Methods

        private bool ShouldShowCustomEffectSettings()
        {
            return !usePreset || presetType == TabPresetType.Custom;
        }

        private bool ShouldShowScaleSettings()
        {
            if (!ShouldShowCustomEffectSettings()) return false;
            return customEffectType == TabEffectType.Scale ||
                   customEffectType == TabEffectType.Bounce ||
                   customEffectType == TabEffectType.Pulse;
        }

        private bool ShouldShowColorSettings()
        {
            return ShouldShowCustomEffectSettings();
        }

        private bool ShouldShowGlowSettings()
        {
            if (!ShouldShowCustomEffectSettings()) return false;
            return customEffectType == TabEffectType.Glow;
        }

        private bool ShouldShowSlideSettings()
        {
            if (!ShouldShowCustomEffectSettings()) return false;
            return customEffectType == TabEffectType.Slide;
        }

        private bool ShouldShowFadeSettings()
        {
            if (!ShouldShowCustomEffectSettings()) return false;
            return customEffectType == TabEffectType.Fade;
        }

        private bool ShouldShowPulseSettings()
        {
            if (!ShouldShowCustomEffectSettings()) return false;
            return customEffectType == TabEffectType.Pulse;
        }

        private bool ShouldShowShakeSettings()
        {
            if (!ShouldShowCustomEffectSettings()) return false;
            return customEffectType == TabEffectType.Shake;
        }

        private bool ShouldShowPresetCustomization()
        {
            return usePreset && presetType != TabPresetType.Custom;
        }

        private bool ShouldShowTestButtons()
        {
            return true;
        }

        #endregion

        #region Button Test Methods

        [ShowIf(nameof(ShouldShowTestButtons))]
        [Button(ButtonSizes.Large, "🎭 Test Random Tab Selection")]
        private void TestRandomTabSelection()
        {
#if UNITY_EDITOR
            // Ensure tabs list is initialized
            if (_tabs.Count == 0)
                InitializeTabsList();

            if (_tabs.Count == 0)
            {
                Debug.LogWarning("⚠️ No tabs found! Please ensure TabParent has Tab children.");
                return;
            }

            // Get a random tab that's different from current
            Tab randomTab = null;
            int attempts = 0;
            do
            {
                randomTab = _tabs[UnityEngine.Random.Range(0, _tabs.Count)];
                attempts++;
            }
            while (randomTab == _curTab && _tabs.Count > 1 && attempts < 10);

            if (randomTab != null)
            {
                Debug.Log($"🎯 Switching to random tab: {randomTab.name} with effect: {_currentConfig?.effectType ?? TabEffectType.None}");
                SetActive(randomTab, true);
            }
            else
            {
                Debug.LogWarning("⚠️ Could not find a different tab to switch to.");
            }
#endif
        }

        [ShowIf(nameof(ShouldShowTestButtons))]
        [Button(ButtonSizes.Medium, "🔄 Test All Tabs Sequence")]
        private void TestAllTabsSequence()
        {
#if UNITY_EDITOR
            // Ensure tabs list is initialized
            if (_tabs.Count == 0)
                InitializeTabsList();

            if (_tabs.Count == 0)
            {
                Debug.LogWarning("⚠️ No tabs found! Please ensure TabParent has Tab children.");
                return;
            }

            StartCoroutine(TestTabsSequentially());
#endif
        }

        [ShowIf(nameof(ShouldShowTestButtons))]
        [Button("🎨 Apply Random Preset")]
        private void ApplyRandomPreset()
        {
#if UNITY_EDITOR
            var presets = System.Enum.GetValues(typeof(TabPresetType));
            var randomIndex = UnityEngine.Random.Range(1, presets.Length); // Skip Custom
            var randomPreset = (TabPresetType)presets.GetValue(randomIndex);

            SetPreset(randomPreset);
            Debug.Log($"🎨 Applied random preset: {randomPreset}");

            // Test the new preset immediately
            TestRandomTabSelection();
#endif
        }

        [ShowIf(nameof(ShouldShowTestButtons))]
        [Button("🧪 Test Current Tab Effect")]
        private void TestCurrentTabEffect()
        {
#if UNITY_EDITOR
            if (_curTab == null)
            {
                if (_firstSelectedTab != null)
                {
                    _curTab = _firstSelectedTab;
                }
                else if (_tabs.Count > 0)
                {
                    _curTab = _tabs[0];
                }
                else
                {
                    Debug.LogWarning("⚠️ No current tab or tabs available for testing.");
                    return;
                }
            }

            Debug.Log($"🧪 Testing current tab effect: {_curTab.name} with {_currentConfig?.effectType ?? TabEffectType.None}");

            // Temporarily deactivate then reactivate to see the effect
            SetTabState(_curTab, false);

            // Small delay then reactivate
            LMotion.Create(0f, 1f, 0.1f)
                .WithOnComplete(() => SetTabState(_curTab, true))
                .RunWithoutBinding();
#endif
        }

#if UNITY_EDITOR
        private System.Collections.IEnumerator TestTabsSequentially()
        {
            Debug.Log($"🔄 Testing all {_tabs.Count} tabs in sequence...");

            foreach (var tab in _tabs)
            {
                Debug.Log($"   → Switching to tab: {tab.name}");
                SetActive(tab, true);

                // Wait for animation duration plus a bit extra
                float waitTime = _currentConfig?.duration ?? 0.3f;
                yield return new WaitForSeconds(waitTime + 0.2f);
            }

            Debug.Log("✅ Sequence test completed!");
        }
#endif

        #endregion
    }
}
