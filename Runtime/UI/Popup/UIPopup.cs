using UnityEngine;
using LitMotion;
using LitMotion.Extensions;
using TriInspector;

namespace ZuyZuy.Workspace
{
    public abstract class UIPopup : MonoBehaviour
    {
        #region Variables

        protected string m_PopupName;
        protected object m_Data;

        protected GameObject m_Container;
        protected CanvasGroup m_CanvasGroup;
        protected MotionHandle m_FadeHandle;
        protected MotionHandle m_ScaleHandle;
        protected MotionHandle m_SlideHandle;

        [Title("🎭 Popup Animation Configuration")]
        [SerializeField] private bool usePreset = true;

        [ShowIf(nameof(usePreset))]
        [Group("Preset Selection")]
        [LabelText("Animation Preset")]
        [SerializeField] private PopupPresetType presetType = PopupPresetType.SilkFade;

        [ShowIf(nameof(usePreset))]
        [ShowIf(nameof(ShouldShowPresetCustomization))]
        [Group("Preset Selection")]
        [SerializeField] private bool allowCustomDuration = false;

        [ShowIf(nameof(usePreset))]
        [ShowIf(nameof(allowCustomDuration))]
        [Group("Preset Selection")]
        [SerializeField] private float customPresetDuration = 0.3f;

        [ShowIf(nameof(ShouldShowCustomAnimationSettings))]
        [Title("🛠️ Custom Animation Settings")]
        [Group("Custom Animation")]
        [LabelText("Animation Type")]
        [SerializeField] private PopupAppearanceAnim customAppearanceAnim = PopupAppearanceAnim.Fade;

        [ShowIf(nameof(ShouldShowCustomAnimationSettings))]
        [Group("Custom Animation")]
        [SerializeField] private float customAnimationDuration = 0.3f;

        [ShowIf(nameof(ShouldShowCustomAnimationSettings))]
        [Group("Custom Animation")]
        [SerializeField] private Ease customEaseType = Ease.OutQuad;

        [ShowIf(nameof(ShouldShowSlideSettings))]
        [Group("Custom Animation")]
        [SerializeField] private Vector2 customSlideOffset = new Vector2(0, 100f);

        [ShowIf(nameof(ShouldShowScaleSettings))]
        [Group("Custom Animation")]
        [SerializeField] private Vector3 customScaleStart = new Vector3(0.8f, 0.8f, 1f);

        [ShowIf(nameof(ShouldShowFadeSettings))]
        [Group("Custom Animation")]
        [SerializeField] private float customFadeStartAlpha = 0f;

        // Current animation configuration (either from preset or custom)
        private PopupAnimationConfig _currentConfig;

        #endregion

        #region Properties

        public string PopupName => m_PopupName;
        public float AnimationDuration => _currentConfig?.duration ?? 0.3f;

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            m_Container = transform.GetChild(0).gameObject;
            m_CanvasGroup = m_Container.GetComponent<CanvasGroup>();
            if (m_CanvasGroup == null)
                m_CanvasGroup = m_Container.AddComponent<CanvasGroup>();

            ApplyCurrentConfiguration();
            Init();
        }

        private void OnValidate()
        {
            ApplyCurrentConfiguration();
        }

        #endregion

        #region Virtual Methods

        protected abstract void Init();

        #endregion

        #region UI Methods

        public virtual void Show(object data = null)
        {
            m_Data = data;
            m_Container.SetActive(true);
            CancelCurrentMotions();

            var config = _currentConfig ?? GetLegacyConfig();

            switch (config.animationType)
            {
                case PopupAppearanceAnim.Fade:
                    PlayFadeAnimation(config.fadeStartAlpha, 1f, config);
                    break;
                case PopupAppearanceAnim.Scale:
                    PlayScaleAnimation(config.scaleStart, Vector3.one, config);
                    break;
                case PopupAppearanceAnim.SlideFromTop:
                    PlaySlideAnimation(new Vector2(0, config.slideOffset.y), Vector2.zero, config);
                    break;
                case PopupAppearanceAnim.SlideFromBottom:
                    PlaySlideAnimation(new Vector2(0, -config.slideOffset.y), Vector2.zero, config);
                    break;
                case PopupAppearanceAnim.SlideFromLeft:
                    PlaySlideAnimation(new Vector2(-config.slideOffset.x, 0), Vector2.zero, config);
                    break;
                case PopupAppearanceAnim.SlideFromRight:
                    PlaySlideAnimation(new Vector2(config.slideOffset.x, 0), Vector2.zero, config);
                    break;
                case PopupAppearanceAnim.SlideFromTopLeft:
                    PlaySlideAnimation(new Vector2(-config.slideOffset.x, config.slideOffset.y), Vector2.zero, config);
                    break;
                case PopupAppearanceAnim.SlideFromTopRight:
                    PlaySlideAnimation(new Vector2(config.slideOffset.x, config.slideOffset.y), Vector2.zero, config);
                    break;
                case PopupAppearanceAnim.SlideFromBottomLeft:
                    PlaySlideAnimation(new Vector2(-config.slideOffset.x, -config.slideOffset.y), Vector2.zero, config);
                    break;
                case PopupAppearanceAnim.SlideFromBottomRight:
                    PlaySlideAnimation(new Vector2(config.slideOffset.x, -config.slideOffset.y), Vector2.zero, config);
                    break;
                case PopupAppearanceAnim.Bounce:
                    PlayScaleAnimation(config.scaleStart * 1.2f, Vector3.one, config);
                    PlayFadeAnimation(config.fadeStartAlpha, 1f, config);
                    break;
            }

            OnShow();
        }

        public virtual void Hide()
        {
            CancelCurrentMotions();

            var config = _currentConfig ?? GetLegacyConfig();

            switch (config.animationType)
            {
                case PopupAppearanceAnim.Fade:
                    PlayFadeAnimation(1f, config.fadeStartAlpha, config, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.Scale:
                    PlayScaleAnimation(Vector3.one, config.scaleStart, config, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromTop:
                    PlaySlideAnimation(Vector2.zero, new Vector2(0, config.slideOffset.y), config, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromBottom:
                    PlaySlideAnimation(Vector2.zero, new Vector2(0, -config.slideOffset.y), config, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromLeft:
                    PlaySlideAnimation(Vector2.zero, new Vector2(-config.slideOffset.x, 0), config, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromRight:
                    PlaySlideAnimation(Vector2.zero, new Vector2(config.slideOffset.x, 0), config, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromTopLeft:
                    PlaySlideAnimation(Vector2.zero, new Vector2(-config.slideOffset.x, config.slideOffset.y), config, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromTopRight:
                    PlaySlideAnimation(Vector2.zero, new Vector2(config.slideOffset.x, config.slideOffset.y), config, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromBottomLeft:
                    PlaySlideAnimation(Vector2.zero, new Vector2(-config.slideOffset.x, -config.slideOffset.y), config, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.SlideFromBottomRight:
                    PlaySlideAnimation(Vector2.zero, new Vector2(config.slideOffset.x, -config.slideOffset.y), config, () => m_Container.SetActive(false));
                    break;
                case PopupAppearanceAnim.Bounce:
                    PlayScaleAnimation(Vector3.one, config.scaleStart * 1.2f, config);
                    PlayFadeAnimation(1f, config.fadeStartAlpha, config, () => m_Container.SetActive(false));
                    break;
            }

            OnHide();
        }

        public void SetPreset(PopupPresetType preset)
        {
            usePreset = true;
            presetType = preset;
            ApplyCurrentConfiguration();
        }

        public void SetCustomAnimation(PopupAppearanceAnim animationType)
        {
            usePreset = false;
            customAppearanceAnim = animationType;
            ApplyCurrentConfiguration();
        }

        #endregion

        #region Override Methods

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }

        protected virtual void OnHideClick()
        {
            Hide();
        }

        #endregion

        #region Utility Methods

        private void ApplyCurrentConfiguration()
        {
            if (usePreset && presetType != PopupPresetType.Custom)
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
                _currentConfig = new PopupAnimationConfig
                {
                    animationType = customAppearanceAnim,
                    duration = customAnimationDuration,
                    easeType = customEaseType,
                    slideOffset = customSlideOffset,
                    scaleStart = customScaleStart,
                    fadeStartAlpha = customFadeStartAlpha
                };
            }


        }

        private PopupAnimationConfig GetPresetConfig(PopupPresetType preset)
        {
            return preset switch
            {
                PopupPresetType.SilkFade => PopupAnimationPresets.SILK_FADE,
                PopupPresetType.VelvetScale => PopupAnimationPresets.VELVET_SCALE,
                PopupPresetType.WhisperSlide => PopupAnimationPresets.WHISPER_SLIDE,
                PopupPresetType.GentleBounce => PopupAnimationPresets.GENTLE_BOUNCE,

                PopupPresetType.SpringScale => PopupAnimationPresets.SPRING_SCALE,
                PopupPresetType.QuickSlide => PopupAnimationPresets.QUICK_SLIDE,
                PopupPresetType.SmoothFade => PopupAnimationPresets.SMOOTH_FADE,
                PopupPresetType.BouncyEntry => PopupAnimationPresets.BOUNCY_ENTRY,

                PopupPresetType.PowerScale => PopupAnimationPresets.POWER_SCALE,
                PopupPresetType.RapidSlide => PopupAnimationPresets.RAPID_SLIDE,
                PopupPresetType.SnapFade => PopupAnimationPresets.SNAP_FADE,
                PopupPresetType.ImpactBounce => PopupAnimationPresets.IMPACT_BOUNCE,

                PopupPresetType.DreamyFade => PopupAnimationPresets.DREAMY_FADE,
                PopupPresetType.FloatingScale => PopupAnimationPresets.FLOATING_SCALE,
                PopupPresetType.GlidingSlide => PopupAnimationPresets.GLIDING_SLIDE,
                PopupPresetType.CloudBounce => PopupAnimationPresets.CLOUD_BOUNCE,

                PopupPresetType.GameOverScale => PopupAnimationPresets.GAME_OVER_SCALE,
                PopupPresetType.VictorySlide => PopupAnimationPresets.VICTORY_SLIDE,
                PopupPresetType.LevelUpFade => PopupAnimationPresets.LEVEL_UP_FADE,
                PopupPresetType.AchievementBounce => PopupAnimationPresets.ACHIEVEMENT_BOUNCE,

                PopupPresetType.MeteorStrike => PopupAnimationPresets.METEOR_STRIKE,
                PopupPresetType.CometEntry => PopupAnimationPresets.COMET_ENTRY,
                PopupPresetType.RisingMoon => PopupAnimationPresets.RISING_MOON,
                PopupPresetType.FallingLeaf => PopupAnimationPresets.FALLING_LEAF,
                PopupPresetType.DiagonalSwipe => PopupAnimationPresets.DIAGONAL_SWIPE,
                PopupPresetType.CornerPeek => PopupAnimationPresets.CORNER_PEEK,

                _ => PopupAnimationPresets.SILK_FADE
            };
        }

        private PopupAnimationConfig GetLegacyConfig()
        {
            return new PopupAnimationConfig
            {
                animationType = PopupAppearanceAnim.Fade,
                duration = 0.3f,
                easeType = Ease.OutQuad,
                slideOffset = new Vector2(0, 100f),
                scaleStart = new Vector3(0.8f, 0.8f, 1f),
                fadeStartAlpha = 0f
            };
        }

        protected virtual void PlayFadeAnimation(float from, float to, PopupAnimationConfig config, System.Action onComplete = null)
        {
            m_FadeHandle = LMotion.Create(from, to, config.duration)
                .WithEase(config.easeType)
                .WithOnComplete(onComplete)
                .BindToAlpha(m_CanvasGroup)
                .AddTo(m_Container);
        }

        protected virtual void PlayScaleAnimation(Vector3 from, Vector3 to, PopupAnimationConfig config, System.Action onComplete = null)
        {
            m_ScaleHandle = LMotion.Create(from, to, config.duration)
                .WithEase(config.easeType)
                .WithOnComplete(onComplete)
                .BindToLocalScale(m_Container.transform)
                .AddTo(m_Container);
        }

        protected virtual void PlaySlideAnimation(Vector2 from, Vector2 to, PopupAnimationConfig config, System.Action onComplete = null)
        {
            m_SlideHandle = LMotion.Create(from, to, config.duration)
                .WithEase(config.easeType)
                .WithOnComplete(onComplete)
                .BindToAnchoredPosition(m_Container.GetComponent<RectTransform>())
                .AddTo(m_Container);
        }

        protected virtual void CancelCurrentMotions()
        {
            m_FadeHandle.TryCancel();
            m_ScaleHandle.TryCancel();
            m_SlideHandle.TryCancel();
        }

        #endregion

        #region Inspector Helper Methods

        private bool ShouldShowCustomAnimationSettings()
        {
            return !usePreset || presetType == PopupPresetType.Custom;
        }

        private bool ShouldShowSlideSettings()
        {
            if (!ShouldShowCustomAnimationSettings()) return false;

            return customAppearanceAnim == PopupAppearanceAnim.SlideFromTop ||
                   customAppearanceAnim == PopupAppearanceAnim.SlideFromBottom ||
                   customAppearanceAnim == PopupAppearanceAnim.SlideFromLeft ||
                   customAppearanceAnim == PopupAppearanceAnim.SlideFromRight ||
                   customAppearanceAnim == PopupAppearanceAnim.SlideFromTopLeft ||
                   customAppearanceAnim == PopupAppearanceAnim.SlideFromTopRight ||
                   customAppearanceAnim == PopupAppearanceAnim.SlideFromBottomLeft ||
                   customAppearanceAnim == PopupAppearanceAnim.SlideFromBottomRight;
        }

        private bool ShouldShowScaleSettings()
        {
            if (!ShouldShowCustomAnimationSettings()) return false;

            return customAppearanceAnim == PopupAppearanceAnim.Scale ||
                   customAppearanceAnim == PopupAppearanceAnim.Bounce;
        }

        private bool ShouldShowFadeSettings()
        {
            if (!ShouldShowCustomAnimationSettings()) return false;

            return customAppearanceAnim == PopupAppearanceAnim.Fade ||
                   customAppearanceAnim == PopupAppearanceAnim.Bounce;
        }

        private bool ShouldShowTestButtons()
        {
            return true; // Allow testing in both editor and play mode
        }

        private bool ShouldShowPresetCustomization()
        {
            return usePreset && presetType != PopupPresetType.Custom;
        }

        private void EnsureComponentsInitialized()
        {
            // Cache container if not already set
            if (m_Container == null && transform.childCount > 0)
            {
                m_Container = transform.GetChild(0).gameObject;
            }

            // Cache canvas group if not already set
            if (m_CanvasGroup == null && m_Container != null)
            {
                m_CanvasGroup = m_Container.GetComponent<CanvasGroup>();
                if (m_CanvasGroup == null)
                    m_CanvasGroup = m_Container.AddComponent<CanvasGroup>();
            }

            // Ensure container is active for testing
            if (m_Container != null && !m_Container.activeSelf)
            {
                m_Container.SetActive(true);
            }

            // Initialize popup name if not set
            if (string.IsNullOrEmpty(m_PopupName))
            {
                m_PopupName = gameObject.name;
            }
        }

        #endregion

        #region Button Test Methods

        [ShowIf(nameof(ShouldShowTestButtons))]
        [Button(ButtonSizes.Large, "🎭 Test Show Animation")]
        private void TestShowAnimation()
        {
#if UNITY_EDITOR
            // Ensure components are cached and initialized
            EnsureComponentsInitialized();

            // Apply current configuration
            ApplyCurrentConfiguration();

            // Show the popup
            Show();

            string mode = Application.isPlaying ? "Play Mode" : "Editor Mode";
            Debug.Log($"🎭 Testing show animation with preset: {presetType} ({mode})");
#endif
        }

        [ShowIf(nameof(ShouldShowTestButtons))]
        [Button(ButtonSizes.Large, "🚪 Test Hide Animation")]
        private void TestHideAnimation()
        {
#if UNITY_EDITOR
            // Ensure components are cached and initialized
            EnsureComponentsInitialized();

            // Apply current configuration
            ApplyCurrentConfiguration();

            // Hide the popup
            Hide();

            string mode = Application.isPlaying ? "Play Mode" : "Editor Mode";
            Debug.Log($"🚪 Testing hide animation with preset: {presetType} ({mode})");
#endif
        }

        [ShowIf(nameof(ShouldShowTestButtons))]
        [Button("🎨 Apply Random Preset")]
        private void ApplyRandomPreset()
        {
#if UNITY_EDITOR
            var presets = System.Enum.GetValues(typeof(PopupPresetType));
            var randomIndex = UnityEngine.Random.Range(1, presets.Length); // Skip Custom
            var randomPreset = (PopupPresetType)presets.GetValue(randomIndex);

            SetPreset(randomPreset);
            Debug.Log($"🎨 Applied random preset: {randomPreset}");
#endif
        }

        [ShowIf(nameof(ShouldShowTestButtons))]
        [Button("🔄 Reset Animation")]
        private void ResetAnimation()
        {
#if UNITY_EDITOR
            // Ensure components are initialized
            EnsureComponentsInitialized();

            // Cancel any running animations
            CancelCurrentMotions();

            // Reset to original state
            if (m_Container != null)
            {
                m_Container.transform.localScale = Vector3.one;
                m_Container.transform.localPosition = Vector3.zero;

                if (m_Container.GetComponent<RectTransform>() != null)
                {
                    m_Container.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }

            if (m_CanvasGroup != null)
            {
                m_CanvasGroup.alpha = 1f;
            }

            Debug.Log("🔄 Animation reset to default state");
#endif
        }
        #endregion
    }
}