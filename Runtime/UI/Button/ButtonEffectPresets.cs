using UnityEngine;
using LitMotion;
using TriInspector;

namespace ZuyZuy.Workspace
{
    [System.Serializable]
    public class ButtonEffectConfig
    {
        [Title("Effect Configuration")]
        public ButtonClickEffect effectType = ButtonClickEffect.Scale;
        public float duration = 0.2f;
        public Ease easeType = Ease.OutQuad;

        [ShowIf(nameof(effectType), ButtonClickEffect.Scale)]
        public Vector3 scaleAmount = Vector3.one * 0.9f;

        [ShowIf(nameof(effectType), ButtonClickEffect.Punch)]
        public Vector3 punchStrength = Vector3.one * 0.1f;

        [ShowIf(nameof(effectType), ButtonClickEffect.Shake)]
        public Vector3 shakeStrength = Vector3.one * 5f;

        [ShowIf(nameof(effectType), ButtonClickEffect.Rotation)]
        public Vector3 rotationAmount = new Vector3(0, 0, 15f);

        [ShowIf(nameof(effectType), ButtonClickEffect.ColorTint)]
        public Color tintColor = Color.gray;

        [ShowIf(nameof(effectType), ButtonClickEffect.Bounce)]
        public float bounceScale = 1.2f;

        [ShowIf(nameof(effectType), ButtonClickEffect.Squeeze)]
        public Vector3 squeezeScale = new Vector3(1.1f, 0.9f, 1f);

        [ShowIf(nameof(effectType), ButtonClickEffect.Flash)]
        public Color flashColor = Color.white;

        [ShowIf(nameof(effectType), ButtonClickEffect.Pulse)]
        public float pulseScale = 1.1f;
    }

    public static class ButtonEffectPresets
    {
        public static readonly ButtonEffectConfig SUBTLE_SCALE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Scale,
            duration = 0.15f,
            easeType = Ease.OutQuad,
            scaleAmount = Vector3.one * 0.95f
        };

        public static readonly ButtonEffectConfig DRAMATIC_BOUNCE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Bounce,
            duration = 0.4f,
            easeType = Ease.OutBack,
            bounceScale = 1.3f
        };

        public static readonly ButtonEffectConfig GENTLE_PULSE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Pulse,
            duration = 0.3f,
            easeType = Ease.InOutSine,
            pulseScale = 1.05f
        };

        public static readonly ButtonEffectConfig QUICK_FLASH = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Flash,
            duration = 0.2f,
            easeType = Ease.OutQuad,
            flashColor = new Color(1f, 1f, 1f, 0.8f)
        };

        public static readonly ButtonEffectConfig SOFT_SQUEEZE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Squeeze,
            duration = 0.25f,
            easeType = Ease.OutQuad,
            squeezeScale = new Vector3(1.05f, 0.95f, 1f)
        };

        public static readonly ButtonEffectConfig SPINNING_CLICK = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Rotation,
            duration = 0.3f,
            easeType = Ease.OutBack,
            rotationAmount = new Vector3(0, 0, 360f)
        };

        public static readonly ButtonEffectConfig IMPACT_PUNCH = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Punch,
            duration = 0.5f,
            easeType = Ease.OutElastic,
            punchStrength = Vector3.one * 0.15f
        };

        public static readonly ButtonEffectConfig VIBRANT_SHAKE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Shake,
            duration = 0.3f,
            easeType = Ease.OutQuad,
            shakeStrength = Vector3.one * 8f
        };

        public static readonly ButtonEffectConfig DARK_TINT = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.ColorTint,
            duration = 0.2f,
            easeType = Ease.OutQuad,
            tintColor = new Color(0.6f, 0.6f, 0.6f, 1f)
        };

        /// <summary>
        /// Apply a preset configuration to a UIButton component
        /// </summary>
        /// <param name="button">The UIButton to configure</param>
        /// <param name="preset">The preset configuration to apply</param>
        public static void ApplyPreset(UIButton button, ButtonEffectConfig preset)
        {
            if (button == null || preset == null) return;

            button.SetClickEffect(preset.effectType);

            // Note: This would require adding public setters to UIButton for the effect parameters
            // For now, users can manually copy the values from the preset
        }

        /// <summary>
        /// Get a preset by name for easy access
        /// </summary>
        /// <param name="presetName">Name of the preset</param>
        /// <returns>The preset configuration or null if not found</returns>
        public static ButtonEffectConfig GetPreset(string presetName)
        {
            return presetName.ToLower() switch
            {
                "subtle" or "subtlescale" => SUBTLE_SCALE,
                "bounce" or "dramaticbounce" => DRAMATIC_BOUNCE,
                "pulse" or "gentlepulse" => GENTLE_PULSE,
                "flash" or "quickflash" => QUICK_FLASH,
                "squeeze" or "softsqueeze" => SOFT_SQUEEZE,
                "spin" or "spinning" or "spinningclick" => SPINNING_CLICK,
                "punch" or "impact" or "impactpunch" => IMPACT_PUNCH,
                "shake" or "vibrant" or "vibrantshake" => VIBRANT_SHAKE,
                "tint" or "dark" or "darktint" => DARK_TINT,
                _ => null
            };
        }
    }

    [System.Serializable]
    public enum ButtonPresetType
    {
        Custom,
        SubtleScale,
        DramaticBounce,
        GentlePulse,
        QuickFlash,
        SoftSqueeze,
        SpinningClick,
        ImpactPunch,
        VibrantShake,
        DarkTint
    }
}