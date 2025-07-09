using UnityEngine;
using LitMotion;

namespace ZuyZuy.Workspace
{
    public enum TabEffectType
    {
        None,
        Scale,
        Bounce,
        Glow,
        Slide,
        Fade,
        Pulse,
        Shake,
        ColorTransition,
        SpriteSwap
    }

    public enum TabPresetType
    {
        Custom,

        // Subtle Effects
        WhisperScale,
        SilkGlow,
        VelvetFade,
        PearlSlide,

        // Medium Effects  
        SpringBounce,
        SmoothPulse,
        GentleShake,
        FlowTransition,

        // Dynamic Effects
        PowerScale,
        NovaGlow,
        QuantumFade,
        LightningSlide,

        // Elegant Effects
        GoldenGlow,
        SapphireFade,
        EmeraldScale,
        RubyPulse,

        // Gaming Effects
        LevelUpScale,
        QuestGlow,
        InventorySlide,
        SkillFade
    }

    [System.Serializable]
    public class TabEffectConfig
    {
        public TabEffectType effectType = TabEffectType.Scale;
        public float duration = 0.3f;
        public Ease easeType = Ease.OutQuad;
        public bool useUnscaledTime = false;

        // Scale settings
        public Vector3 scaleAmount = new Vector3(1.1f, 1.1f, 1f);
        public Vector3 inactiveScale = Vector3.one;

        // Color settings
        public Color activeImageColor = Color.white;
        public Color inactiveImageColor = new Color(0.7f, 0.7f, 0.7f, 1f);
        public Color activeTextColor = Color.white;
        public Color inactiveTextColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        public Color glowColor = new Color(1f, 1f, 0f, 0.5f);

        // Animation settings
        public Vector2 slideOffset = new Vector2(0, 10f);
        public float fadeAlpha = 0.5f;
        public float pulseScale = 1.15f;
        public Vector3 shakeStrength = new Vector3(2f, 0f, 0f);

        // Sprite settings
        public bool useActiveSpriteAnimation = false;
        public bool useInactiveSpriteAnimation = false;

        // Advanced settings
        public bool animateOnSelect = true;
        public bool animateOnDeselect = true;
        public bool maintainEffectWhileActive = false;
        public float maintainEffectPulseSpeed = 2f;
    }
}