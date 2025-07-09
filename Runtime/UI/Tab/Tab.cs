using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LitMotion;
using LitMotion.Extensions;
using TriInspector;

namespace ZuyZuy.Workspace
{
    public sealed class Tab : MonoBehaviour
    {
        #region Variables

        [Title("🆔 Tab Identification")]
        [SerializeField] private int _id;
        [SerializeField] private Button _btn;

        [Title("🖼️ Image Configuration")]
        [SerializeField] private Image _img;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _deactiveSprite;
        [SerializeField] private Color _activeImgColor;
        [SerializeField] private Color _deactiveImgColor;

        [Title("📝 Text Configuration")]
        [SerializeField] private TextMeshProUGUI _txt;
        [SerializeField] private Color _activeTxtColor;
        [SerializeField] private Color _deactiveTxtColor;
        [SerializeField] private string _activeTxtStr;
        [SerializeField] private string _deactiveTxtStr;

        [Title("🎨 Advanced Visual Options")]
        [SerializeField] private bool enableAdvancedVisuals = false;

        [ShowIf(nameof(enableAdvancedVisuals))]
        [Group("Visual Effects")]
        [SerializeField] private Image _backgroundImg;

        [ShowIf(nameof(enableAdvancedVisuals))]
        [Group("Visual Effects")]
        [SerializeField] private Image _glowImg;

        [ShowIf(nameof(enableAdvancedVisuals))]
        [Group("Visual Effects")]
        [SerializeField] private CanvasGroup _canvasGroup;

        [ShowIf(nameof(enableAdvancedVisuals))]
        [Group("Visual Effects")]
        [SerializeField] private bool enableParticleEffects = false;

        [ShowIf(nameof(enableAdvancedVisuals))]
        [ShowIf(nameof(enableParticleEffects))]
        [Group("Visual Effects")]
        [SerializeField] private ParticleSystem _particleEffect;

        private TabParent _tabParent;
        private bool _isActive;
        private bool _isHovered;

        // Store original values for restoration
        private Color _originalImageColor;
        private Color _originalTextColor;
        private Vector3 _originalScale;

        // Animation handles
        private MotionHandle _colorAnimationHandle;
        private MotionHandle _scaleAnimationHandle;
        private MotionHandle _glowAnimationHandle;

        #endregion

        #region Properties

        public int Id => _id;
        public bool IsActive => _isActive;
        public bool IsHovered => _isHovered;
        public Button Button => _btn;
        public Image MainImage => _img;
        public TextMeshProUGUI MainText => _txt;

        #endregion

        #region Unity Methods

        private void Start()
        {
            InitializeTab();
        }

        private void OnDestroy()
        {
            CleanupAnimations();
        }

        #endregion

        #region UI Methods

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

        // Enhanced overloaded methods for configuration-based updates
        public void UpdateImageColor(bool isActive, TabEffectConfig config)
        {
            if (_img != null && config != null)
                _img.color = isActive ? config.activeImageColor : config.inactiveImageColor;
        }

        public void UpdateTextColor(bool isActive, TabEffectConfig config)
        {
            if (_txt != null && config != null)
                _txt.color = isActive ? config.activeTextColor : config.inactiveTextColor;
        }

        // Enhanced animation methods for smooth color transitions
        public void AnimateImageColor(bool isActive, TabEffectConfig config)
        {
            if (_img == null || config == null) return;

            var targetColor = isActive ? config.activeImageColor : config.inactiveImageColor;

            _colorAnimationHandle.TryCancel();
            _colorAnimationHandle = LMotion.Create(_img.color, targetColor, config.duration)
                .WithEase(config.easeType)
                .BindToColor(_img);
        }

        public void AnimateTextColor(bool isActive, TabEffectConfig config)
        {
            if (_txt == null || config == null) return;

            var targetColor = isActive ? config.activeTextColor : config.inactiveTextColor;

            LMotion.Create(_txt.color, targetColor, config.duration)
                .WithEase(config.easeType)
                .BindToColor(_txt);
        }

        // Direct color setting methods (used in maintain effects and hover)
        public void SetImageColor(Color color)
        {
            if (_img != null)
                _img.color = color;
        }

        public void SetTextColor(Color color)
        {
            if (_txt != null)
                _txt.color = color;
        }

        // Enhanced visual effect methods
        public void SetBackgroundColor(Color color)
        {
            if (_backgroundImg != null)
                _backgroundImg.color = color;
        }

        public void AnimateBackgroundColor(Color fromColor, Color toColor, float duration, Ease easeType = Ease.OutQuad)
        {
            if (_backgroundImg == null) return;

            LMotion.Create(fromColor, toColor, duration)
                .WithEase(easeType)
                .BindToColor(_backgroundImg);
        }

        public void SetGlowEffect(bool enable, Color glowColor = default)
        {
            if (_glowImg == null) return;

            if (enable)
            {
                _glowImg.gameObject.SetActive(true);
                _glowImg.color = glowColor == default ? Color.white : glowColor;
            }
            else
            {
                _glowImg.gameObject.SetActive(false);
            }
        }

        public void AnimateGlowEffect(bool enable, Color glowColor, float duration)
        {
            if (_glowImg == null) return;

            _glowAnimationHandle.TryCancel();

            if (enable)
            {
                _glowImg.gameObject.SetActive(true);
                var startColor = new Color(glowColor.r, glowColor.g, glowColor.b, 0f);
                var endColor = glowColor;

                _glowAnimationHandle = LMotion.Create(startColor, endColor, duration)
                    .WithEase(Ease.OutQuad)
                    .BindToColor(_glowImg);
            }
            else
            {
                var startColor = _glowImg.color;
                var endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

                _glowAnimationHandle = LMotion.Create(startColor, endColor, duration)
                    .WithEase(Ease.OutQuad)
                    .WithOnComplete(() => _glowImg.gameObject.SetActive(false))
                    .BindToColor(_glowImg);
            }
        }

        public void SetAlpha(float alpha)
        {
            if (_canvasGroup != null)
                _canvasGroup.alpha = alpha;
        }

        public void AnimateAlpha(float fromAlpha, float toAlpha, float duration, Ease easeType = Ease.OutQuad)
        {
            if (_canvasGroup == null) return;

            LMotion.Create(fromAlpha, toAlpha, duration)
                .WithEase(easeType)
                .BindToAlpha(_canvasGroup);
        }

        public void PlayParticleEffect(bool play)
        {
            if (_particleEffect == null || !enableParticleEffects) return;

            if (play)
                _particleEffect.Play();
            else
                _particleEffect.Stop();
        }

        public void SetState(bool isActive, bool animate = true)
        {
            _isActive = isActive;

            if (animate)
            {
                // Use smooth animations for state changes
                AnimateImageColor(isActive, GetDefaultConfig());
                AnimateTextColor(isActive, GetDefaultConfig());
            }
            else
            {
                // Immediate state change
                UpdateImageColor(isActive);
                UpdateTextColor(isActive);
            }

            UpdateImageSprite(isActive);
            UpdateTextString(isActive);

            // Play particle effect for active state
            if (enableParticleEffects)
                PlayParticleEffect(isActive);
        }

        public void SetHoverState(bool isHovered)
        {
            _isHovered = isHovered;
        }

        // Gradient color support
        public void ApplyGradientColor(Gradient gradient, float time = 0.5f)
        {
            if (_img == null) return;

            var color = gradient.Evaluate(time);
            SetImageColor(color);
        }

        public void AnimateGradientColor(Gradient gradient, float duration, int samples = 10)
        {
            if (_img == null) return;

            var sequence = LSequence.Create();
            var stepDuration = duration / samples;

            for (int i = 0; i <= samples; i++)
            {
                var time = (float)i / samples;
                var color = gradient.Evaluate(time);

                if (i == 0)
                {
                    SetImageColor(color);
                }
                else
                {
                    sequence.Append(LMotion.Create(_img.color, color, stepDuration)
                        .WithEase(Ease.Linear)
                        .BindToColor(_img));
                }
            }

            sequence.Run();
        }

        #endregion

        #region Utility Methods

        private void InitializeTab()
        {
            _tabParent = GetComponentInParent<TabParent>();

            if (_btn != null)
                _btn.onClick.AddListener(OnClick);

            StoreOriginalValues();
            InitializeComponents();
        }

        private void StoreOriginalValues()
        {
            if (_img != null)
                _originalImageColor = _img.color;

            if (_txt != null)
                _originalTextColor = _txt.color;

            _originalScale = transform.localScale;
        }

        private void InitializeComponents()
        {
            // Auto-setup advanced visual components if enabled
            if (enableAdvancedVisuals)
            {
                if (_canvasGroup == null)
                    _canvasGroup = GetComponent<CanvasGroup>();

                if (_canvasGroup == null)
                    _canvasGroup = gameObject.AddComponent<CanvasGroup>();

                // Setup glow image if not assigned
                if (_glowImg == null)
                {
                    var glowObj = transform.Find("GlowImage");
                    if (glowObj != null)
                        _glowImg = glowObj.GetComponent<Image>();
                }

                // Setup particle effect if not assigned
                if (_particleEffect == null && enableParticleEffects)
                {
                    _particleEffect = GetComponentInChildren<ParticleSystem>();
                }
            }
        }

        private TabEffectConfig GetDefaultConfig()
        {
            return new TabEffectConfig
            {
                duration = 0.3f,
                easeType = Ease.OutQuad,
                activeImageColor = _activeImgColor,
                inactiveImageColor = _deactiveImgColor,
                activeTextColor = _activeTxtColor,
                inactiveTextColor = _deactiveTxtColor
            };
        }

        private void CleanupAnimations()
        {
            _colorAnimationHandle.TryCancel();
            _scaleAnimationHandle.TryCancel();
            _glowAnimationHandle.TryCancel();
        }

        private void OnClick()
        {
            _tabParent?.SetActive(this, !_isActive);
        }

        #endregion
    }
}
