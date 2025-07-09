using UnityEngine;
using LitMotion;

namespace ZuyZuy.Workspace
{
    public static class TabEffectPresets
    {
        // Subtle Effects
        public static readonly TabEffectConfig WHISPER_SCALE = new TabEffectConfig
        {
            effectType = TabEffectType.Scale,
            duration = 0.2f,
            easeType = Ease.OutQuad,
            scaleAmount = new Vector3(1.05f, 1.05f, 1f),
            activeImageColor = Color.white,
            inactiveImageColor = new Color(0.8f, 0.8f, 0.8f, 1f),
            activeTextColor = new Color(0.2f, 0.2f, 0.2f, 1f),
            inactiveTextColor = new Color(0.6f, 0.6f, 0.6f, 1f)
        };

        public static readonly TabEffectConfig SILK_GLOW = new TabEffectConfig
        {
            effectType = TabEffectType.Glow,
            duration = 0.3f,
            easeType = Ease.OutSine,
            glowColor = new Color(1f, 1f, 1f, 0.3f),
            activeImageColor = Color.white,
            inactiveImageColor = new Color(0.7f, 0.7f, 0.7f, 1f),
            activeTextColor = new Color(0.1f, 0.1f, 0.1f, 1f),
            inactiveTextColor = new Color(0.5f, 0.5f, 0.5f, 1f)
        };

        public static readonly TabEffectConfig VELVET_FADE = new TabEffectConfig
        {
            effectType = TabEffectType.Fade,
            duration = 0.25f,
            easeType = Ease.OutQuart,
            fadeAlpha = 0.6f,
            activeImageColor = Color.white,
            inactiveImageColor = new Color(0.6f, 0.6f, 0.6f, 0.8f),
            activeTextColor = Color.black,
            inactiveTextColor = new Color(0.4f, 0.4f, 0.4f, 1f)
        };

        public static readonly TabEffectConfig PEARL_SLIDE = new TabEffectConfig
        {
            effectType = TabEffectType.Slide,
            duration = 0.2f,
            easeType = Ease.OutCubic,
            slideOffset = new Vector2(0, 5f),
            activeImageColor = Color.white,
            inactiveImageColor = new Color(0.75f, 0.75f, 0.75f, 1f),
            activeTextColor = new Color(0.15f, 0.15f, 0.15f, 1f),
            inactiveTextColor = new Color(0.55f, 0.55f, 0.55f, 1f)
        };

        // Medium Effects
        public static readonly TabEffectConfig SPRING_BOUNCE = new TabEffectConfig
        {
            effectType = TabEffectType.Bounce,
            duration = 0.4f,
            easeType = Ease.OutBounce,
            scaleAmount = new Vector3(1.15f, 1.15f, 1f),
            activeImageColor = Color.white,
            inactiveImageColor = new Color(0.7f, 0.7f, 0.7f, 1f),
            activeTextColor = new Color(0.1f, 0.3f, 0.6f, 1f),
            inactiveTextColor = new Color(0.5f, 0.5f, 0.5f, 1f)
        };

        public static readonly TabEffectConfig SMOOTH_PULSE = new TabEffectConfig
        {
            effectType = TabEffectType.Pulse,
            duration = 0.3f,
            easeType = Ease.InOutSine,
            pulseScale = 1.08f,
            maintainEffectWhileActive = true,
            maintainEffectPulseSpeed = 1.5f,
            activeImageColor = new Color(0.9f, 0.95f, 1f, 1f),
            inactiveImageColor = new Color(0.65f, 0.65f, 0.65f, 1f),
            activeTextColor = new Color(0.1f, 0.2f, 0.4f, 1f),
            inactiveTextColor = new Color(0.5f, 0.5f, 0.5f, 1f)
        };

        public static readonly TabEffectConfig GENTLE_SHAKE = new TabEffectConfig
        {
            effectType = TabEffectType.Shake,
            duration = 0.2f,
            easeType = Ease.OutQuad,
            shakeStrength = new Vector3(3f, 0f, 0f),
            activeImageColor = Color.white,
            inactiveImageColor = new Color(0.8f, 0.8f, 0.8f, 1f),
            activeTextColor = new Color(0.2f, 0.2f, 0.2f, 1f),
            inactiveTextColor = new Color(0.6f, 0.6f, 0.6f, 1f)
        };

        public static readonly TabEffectConfig FLOW_TRANSITION = new TabEffectConfig
        {
            effectType = TabEffectType.ColorTransition,
            duration = 0.35f,
            easeType = Ease.InOutQuad,
            activeImageColor = new Color(0.85f, 0.9f, 1f, 1f),
            inactiveImageColor = new Color(0.6f, 0.6f, 0.6f, 1f),
            activeTextColor = new Color(0.1f, 0.25f, 0.5f, 1f),
            inactiveTextColor = new Color(0.45f, 0.45f, 0.45f, 1f)
        };

        // Dynamic Effects
        public static readonly TabEffectConfig POWER_SCALE = new TabEffectConfig
        {
            effectType = TabEffectType.Scale,
            duration = 0.15f,
            easeType = Ease.OutBack,
            scaleAmount = new Vector3(1.2f, 1.2f, 1f),
            activeImageColor = new Color(1f, 0.95f, 0.8f, 1f),
            inactiveImageColor = new Color(0.6f, 0.6f, 0.6f, 1f),
            activeTextColor = new Color(0.8f, 0.4f, 0.1f, 1f),
            inactiveTextColor = new Color(0.4f, 0.4f, 0.4f, 1f)
        };

        public static readonly TabEffectConfig NOVA_GLOW = new TabEffectConfig
        {
            effectType = TabEffectType.Glow,
            duration = 0.25f,
            easeType = Ease.OutQuint,
            glowColor = new Color(1f, 0.8f, 0.2f, 0.6f),
            maintainEffectWhileActive = true,
            maintainEffectPulseSpeed = 2f,
            activeImageColor = new Color(1f, 0.9f, 0.7f, 1f),
            inactiveImageColor = new Color(0.5f, 0.5f, 0.5f, 1f),
            activeTextColor = new Color(0.9f, 0.5f, 0.1f, 1f),
            inactiveTextColor = new Color(0.4f, 0.4f, 0.4f, 1f)
        };

        public static readonly TabEffectConfig QUANTUM_FADE = new TabEffectConfig
        {
            effectType = TabEffectType.Fade,
            duration = 0.18f,
            easeType = Ease.OutExpo,
            fadeAlpha = 0.3f,
            activeImageColor = new Color(0.7f, 0.9f, 1f, 1f),
            inactiveImageColor = new Color(0.4f, 0.4f, 0.4f, 0.7f),
            activeTextColor = new Color(0.1f, 0.4f, 0.8f, 1f),
            inactiveTextColor = new Color(0.3f, 0.3f, 0.3f, 1f)
        };

        public static readonly TabEffectConfig LIGHTNING_SLIDE = new TabEffectConfig
        {
            effectType = TabEffectType.Slide,
            duration = 0.12f,
            easeType = Ease.OutCirc,
            slideOffset = new Vector2(0, 15f),
            activeImageColor = new Color(0.9f, 0.9f, 1f, 1f),
            inactiveImageColor = new Color(0.5f, 0.5f, 0.5f, 1f),
            activeTextColor = new Color(0.2f, 0.3f, 0.9f, 1f),
            inactiveTextColor = new Color(0.4f, 0.4f, 0.4f, 1f)
        };

        // Elegant Effects
        public static readonly TabEffectConfig GOLDEN_GLOW = new TabEffectConfig
        {
            effectType = TabEffectType.Glow,
            duration = 0.4f,
            easeType = Ease.InOutSine,
            glowColor = new Color(1f, 0.843f, 0f, 0.4f),
            maintainEffectWhileActive = true,
            maintainEffectPulseSpeed = 1.2f,
            activeImageColor = new Color(1f, 0.9f, 0.6f, 1f),
            inactiveImageColor = new Color(0.7f, 0.7f, 0.7f, 1f),
            activeTextColor = new Color(0.8f, 0.6f, 0.1f, 1f),
            inactiveTextColor = new Color(0.5f, 0.5f, 0.5f, 1f)
        };

        public static readonly TabEffectConfig SAPPHIRE_FADE = new TabEffectConfig
        {
            effectType = TabEffectType.Fade,
            duration = 0.35f,
            easeType = Ease.InOutQuart,
            fadeAlpha = 0.4f,
            activeImageColor = new Color(0.7f, 0.8f, 1f, 1f),
            inactiveImageColor = new Color(0.6f, 0.6f, 0.6f, 0.8f),
            activeTextColor = new Color(0.1f, 0.3f, 0.7f, 1f),
            inactiveTextColor = new Color(0.4f, 0.4f, 0.4f, 1f)
        };

        public static readonly TabEffectConfig EMERALD_SCALE = new TabEffectConfig
        {
            effectType = TabEffectType.Scale,
            duration = 0.3f,
            easeType = Ease.OutElastic,
            scaleAmount = new Vector3(1.12f, 1.12f, 1f),
            activeImageColor = new Color(0.7f, 1f, 0.8f, 1f),
            inactiveImageColor = new Color(0.65f, 0.65f, 0.65f, 1f),
            activeTextColor = new Color(0.1f, 0.6f, 0.2f, 1f),
            inactiveTextColor = new Color(0.45f, 0.45f, 0.45f, 1f)
        };

        public static readonly TabEffectConfig RUBY_PULSE = new TabEffectConfig
        {
            effectType = TabEffectType.Pulse,
            duration = 0.25f,
            easeType = Ease.InOutSine,
            pulseScale = 1.1f,
            maintainEffectWhileActive = true,
            maintainEffectPulseSpeed = 1.8f,
            activeImageColor = new Color(1f, 0.7f, 0.7f, 1f),
            inactiveImageColor = new Color(0.6f, 0.6f, 0.6f, 1f),
            activeTextColor = new Color(0.8f, 0.1f, 0.1f, 1f),
            inactiveTextColor = new Color(0.4f, 0.4f, 0.4f, 1f)
        };

        // Gaming Effects
        public static readonly TabEffectConfig LEVEL_UP_SCALE = new TabEffectConfig
        {
            effectType = TabEffectType.Scale,
            duration = 0.5f,
            easeType = Ease.OutBounce,
            scaleAmount = new Vector3(1.25f, 1.25f, 1f),
            activeImageColor = new Color(1f, 1f, 0.6f, 1f),
            inactiveImageColor = new Color(0.6f, 0.6f, 0.6f, 1f),
            activeTextColor = new Color(0.9f, 0.7f, 0.1f, 1f),
            inactiveTextColor = new Color(0.4f, 0.4f, 0.4f, 1f)
        };

        public static readonly TabEffectConfig QUEST_GLOW = new TabEffectConfig
        {
            effectType = TabEffectType.Glow,
            duration = 0.3f,
            easeType = Ease.InOutSine,
            glowColor = new Color(0.5f, 1f, 0.5f, 0.5f),
            maintainEffectWhileActive = true,
            maintainEffectPulseSpeed = 2.5f,
            activeImageColor = new Color(0.8f, 1f, 0.8f, 1f),
            inactiveImageColor = new Color(0.55f, 0.55f, 0.55f, 1f),
            activeTextColor = new Color(0.2f, 0.7f, 0.2f, 1f),
            inactiveTextColor = new Color(0.4f, 0.4f, 0.4f, 1f)
        };

        public static readonly TabEffectConfig INVENTORY_SLIDE = new TabEffectConfig
        {
            effectType = TabEffectType.Slide,
            duration = 0.2f,
            easeType = Ease.OutBack,
            slideOffset = new Vector2(0, 8f),
            activeImageColor = new Color(0.9f, 0.9f, 1f, 1f),
            inactiveImageColor = new Color(0.6f, 0.6f, 0.6f, 1f),
            activeTextColor = new Color(0.3f, 0.3f, 0.8f, 1f),
            inactiveTextColor = new Color(0.45f, 0.45f, 0.45f, 1f)
        };

        public static readonly TabEffectConfig SKILL_FADE = new TabEffectConfig
        {
            effectType = TabEffectType.Fade,
            duration = 0.25f,
            easeType = Ease.OutQuint,
            fadeAlpha = 0.5f,
            activeImageColor = new Color(1f, 0.8f, 1f, 1f),
            inactiveImageColor = new Color(0.5f, 0.5f, 0.5f, 0.8f),
            activeTextColor = new Color(0.7f, 0.2f, 0.7f, 1f),
            inactiveTextColor = new Color(0.4f, 0.4f, 0.4f, 1f)
        };

        public static TabEffectConfig GetPreset(string presetName)
        {
            return presetName switch
            {
                "WhisperScale" => WHISPER_SCALE,
                "SilkGlow" => SILK_GLOW,
                "VelvetFade" => VELVET_FADE,
                "PearlSlide" => PEARL_SLIDE,
                "SpringBounce" => SPRING_BOUNCE,
                "SmoothPulse" => SMOOTH_PULSE,
                "GentleShake" => GENTLE_SHAKE,
                "FlowTransition" => FLOW_TRANSITION,
                "PowerScale" => POWER_SCALE,
                "NovaGlow" => NOVA_GLOW,
                "QuantumFade" => QUANTUM_FADE,
                "LightningSlide" => LIGHTNING_SLIDE,
                "GoldenGlow" => GOLDEN_GLOW,
                "SapphireFade" => SAPPHIRE_FADE,
                "EmeraldScale" => EMERALD_SCALE,
                "RubyPulse" => RUBY_PULSE,
                "LevelUpScale" => LEVEL_UP_SCALE,
                "QuestGlow" => QUEST_GLOW,
                "InventorySlide" => INVENTORY_SLIDE,
                "SkillFade" => SKILL_FADE,
                _ => WHISPER_SCALE
            };
        }
    }
}