using UnityEngine;
using LitMotion;
using TriInspector;

namespace ZuyZuy.Workspace
{
    [System.Serializable]
    public class ButtonEffectConfig
    {
        [Title("✨ Effect Configuration")]
        public ButtonClickEffect effectType = ButtonClickEffect.Scale;

        [Group("Timing")]
        [Range(0.05f, 2f)]
        public float duration = 0.2f;
        [Group("Timing")]
        public Ease easeType = Ease.OutQuad;

        [Group("Scale Effects")]
        [ShowIf(nameof(effectType), ButtonClickEffect.Scale)]
        public Vector3 scaleAmount = Vector3.one * 0.9f;

        [Group("Scale Effects")]
        [ShowIf(nameof(effectType), ButtonClickEffect.Bounce)]
        [Range(1f, 2f)]
        public float bounceScale = 1.2f;

        [Group("Scale Effects")]
        [ShowIf(nameof(effectType), ButtonClickEffect.Squeeze)]
        public Vector3 squeezeScale = new Vector3(1.1f, 0.9f, 1f);

        [Group("Scale Effects")]
        [ShowIf(nameof(effectType), ButtonClickEffect.Pulse)]
        [Range(1f, 1.5f)]
        public float pulseScale = 1.1f;

        [Group("Motion Effects")]
        [ShowIf(nameof(effectType), ButtonClickEffect.Punch)]
        public Vector3 punchStrength = Vector3.one * 0.1f;

        [Group("Motion Effects")]
        [ShowIf(nameof(effectType), ButtonClickEffect.Shake)]
        public Vector3 shakeStrength = Vector3.one * 5f;

        [Group("Motion Effects")]
        [ShowIf(nameof(effectType), ButtonClickEffect.Rotation)]
        public Vector3 rotationAmount = new Vector3(0, 0, 15f);

        [Group("Color Effects")]
        [ShowIf(nameof(effectType), ButtonClickEffect.ColorTint)]
        public Color tintColor = Color.gray;

        [Group("Color Effects")]
        [ShowIf(nameof(effectType), ButtonClickEffect.Flash)]
        public Color flashColor = Color.white;
    }

    public static class ButtonEffectPresets
    {
        #region 🌟 Elegant & Subtle Presets

        public static readonly ButtonEffectConfig WHISPER_SCALE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Scale,
            duration = 0.12f,
            easeType = Ease.OutCubic,
            scaleAmount = Vector3.one * 0.97f
        };

        public static readonly ButtonEffectConfig SILK_SQUEEZE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Squeeze,
            duration = 0.18f,
            easeType = Ease.OutQuart,
            squeezeScale = new Vector3(1.03f, 0.97f, 1f)
        };

        public static readonly ButtonEffectConfig VELVET_PULSE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Pulse,
            duration = 0.25f,
            easeType = Ease.InOutSine,
            pulseScale = 1.04f
        };

        public static readonly ButtonEffectConfig PEARL_FLASH = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Flash,
            duration = 0.15f,
            easeType = Ease.OutExpo,
            flashColor = new Color(1f, 1f, 1f, 0.6f)
        };

        #endregion

        #region 💫 Dynamic & Energetic Presets

        public static readonly ButtonEffectConfig SPRING_BOUNCE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Bounce,
            duration = 0.35f,
            easeType = Ease.OutBack,
            bounceScale = 1.15f
        };

        public static readonly ButtonEffectConfig NOVA_FLASH = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Flash,
            duration = 0.2f,
            easeType = Ease.OutQuart,
            flashColor = new Color(0.95f, 0.85f, 0.3f, 0.9f)
        };

        public static readonly ButtonEffectConfig ELECTRIC_SHAKE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Shake,
            duration = 0.25f,
            easeType = Ease.OutQuad,
            shakeStrength = new Vector3(6f, 6f, 0f)
        };

        public static readonly ButtonEffectConfig COSMIC_SPIN = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Rotation,
            duration = 0.4f,
            easeType = Ease.OutBack,
            rotationAmount = new Vector3(0, 0, 180f)
        };

        #endregion

        #region 🎭 Dramatic & Impactful Presets

        public static readonly ButtonEffectConfig THUNDER_PUNCH = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Punch,
            duration = 0.6f,
            easeType = Ease.OutElastic,
            punchStrength = new Vector3(0.2f, 0.2f, 0f)
        };

        public static readonly ButtonEffectConfig METEOR_BOUNCE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Bounce,
            duration = 0.5f,
            easeType = Ease.OutBounce,
            bounceScale = 1.4f
        };

        public static readonly ButtonEffectConfig ECLIPSE_TINT = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.ColorTint,
            duration = 0.3f,
            easeType = Ease.InOutQuad,
            tintColor = new Color(0.2f, 0.15f, 0.4f, 1f)
        };

        public static readonly ButtonEffectConfig SUPERNOVA_PULSE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Pulse,
            duration = 0.4f,
            easeType = Ease.InOutBack,
            pulseScale = 1.25f
        };

        #endregion

        #region 🌸 Themed Color Presets

        public static readonly ButtonEffectConfig ROSE_GOLD_FLASH = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Flash,
            duration = 0.22f,
            easeType = Ease.OutQuart,
            flashColor = new Color(1f, 0.76f, 0.8f, 0.85f)
        };

        public static readonly ButtonEffectConfig OCEAN_TINT = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.ColorTint,
            duration = 0.25f,
            easeType = Ease.OutQuad,
            tintColor = new Color(0.3f, 0.7f, 0.9f, 1f)
        };

        public static readonly ButtonEffectConfig FOREST_SQUEEZE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Squeeze,
            duration = 0.2f,
            easeType = Ease.OutQuart,
            squeezeScale = new Vector3(1.08f, 0.92f, 1f)
        };

        public static readonly ButtonEffectConfig SUNSET_FLASH = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Flash,
            duration = 0.28f,
            easeType = Ease.OutExpo,
            flashColor = new Color(1f, 0.6f, 0.2f, 0.75f)
        };

        #endregion

        #region 🎮 Gaming & Interactive Presets

        public static readonly ButtonEffectConfig POWER_UP_BOUNCE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Bounce,
            duration = 0.3f,
            easeType = Ease.OutBack,
            bounceScale = 1.2f
        };

        public static readonly ButtonEffectConfig COMBO_SHAKE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Shake,
            duration = 0.15f,
            easeType = Ease.OutQuad,
            shakeStrength = new Vector3(8f, 3f, 0f)
        };

        public static readonly ButtonEffectConfig LEVEL_UP_SPIN = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Rotation,
            duration = 0.45f,
            easeType = Ease.OutElastic,
            rotationAmount = new Vector3(0, 0, 270f)
        };

        public static readonly ButtonEffectConfig CRYSTAL_PULSE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Pulse,
            duration = 0.35f,
            easeType = Ease.InOutCubic,
            pulseScale = 1.12f
        };

        #endregion

        #region 🧸 Cute & Playful Presets

        public static readonly ButtonEffectConfig BUBBLE_BOUNCE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Bounce,
            duration = 0.22f,
            easeType = Ease.OutBack,
            bounceScale = 1.18f
        };

        public static readonly ButtonEffectConfig JELLY_SQUEEZE = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Squeeze,
            duration = 0.19f,
            easeType = Ease.OutElastic,
            squeezeScale = new Vector3(1.12f, 0.88f, 1f)
        };

        public static readonly ButtonEffectConfig POP_FLASH = new ButtonEffectConfig
        {
            effectType = ButtonClickEffect.Flash,
            duration = 0.13f,
            easeType = Ease.OutQuad,
            flashColor = new Color(1f, 0.95f, 0.7f, 0.8f)
        };

        #endregion

        /// <summary>
        /// 🎯 Apply a preset configuration to a UIButton component
        /// </summary>
        /// <param name="button">The UIButton to configure</param>
        /// <param name="preset">The preset configuration to apply</param>
        public static void ApplyPreset(UIButton button, ButtonEffectConfig preset)
        {
            if (button == null || preset == null) return;

            button.SetClickEffect(preset.effectType);
            // Note: Enhanced version would require public setters for all effect parameters
        }

        /// <summary>
        /// 🔍 Get a preset by name with enhanced search capabilities
        /// </summary>
        /// <param name="presetName">Name of the preset (case-insensitive)</param>
        /// <returns>The preset configuration or null if not found</returns>
        public static ButtonEffectConfig GetPreset(string presetName)
        {
            return presetName.ToLower().Replace("_", "").Replace(" ", "") switch
            {
                // Elegant & Subtle
                "whisper" or "whisperscale" => WHISPER_SCALE,
                "silk" or "silksqueeze" => SILK_SQUEEZE,
                "velvet" or "velvetpulse" => VELVET_PULSE,
                "pearl" or "pearlflash" => PEARL_FLASH,

                // Dynamic & Energetic
                "spring" or "springbounce" => SPRING_BOUNCE,
                "nova" or "novaflash" => NOVA_FLASH,
                "electric" or "electricshake" => ELECTRIC_SHAKE,
                "cosmic" or "cosmicspin" => COSMIC_SPIN,

                // Dramatic & Impactful
                "thunder" or "thunderpunch" => THUNDER_PUNCH,
                "meteor" or "meteorbounce" => METEOR_BOUNCE,
                "eclipse" or "eclipsetint" => ECLIPSE_TINT,
                "supernova" or "supernovapulse" => SUPERNOVA_PULSE,

                // Themed Colors
                "rosegold" or "rosegoldflash" => ROSE_GOLD_FLASH,
                "ocean" or "oceantint" => OCEAN_TINT,
                "forest" or "forestsqueeze" => FOREST_SQUEEZE,
                "sunset" or "sunsetflash" => SUNSET_FLASH,

                // Gaming & Interactive
                "powerup" or "powerupbounce" => POWER_UP_BOUNCE,
                "combo" or "comboshake" => COMBO_SHAKE,
                "levelup" or "levelupspin" => LEVEL_UP_SPIN,
                "crystal" or "crystalpulse" => CRYSTAL_PULSE,

                // Legacy support
                "subtle" => WHISPER_SCALE,
                "bounce" => SPRING_BOUNCE,
                "pulse" => VELVET_PULSE,
                "flash" => PEARL_FLASH,
                "squeeze" => SILK_SQUEEZE,
                "spin" => COSMIC_SPIN,
                "punch" => THUNDER_PUNCH,
                "shake" => ELECTRIC_SHAKE,
                "tint" => OCEAN_TINT,

                _ => null
            };
        }

        /// <summary>
        /// 📚 Get all available presets organized by category
        /// </summary>
        /// <returns>Dictionary of preset categories and their configurations</returns>
        public static System.Collections.Generic.Dictionary<string, ButtonEffectConfig[]> GetAllPresetsByCategory()
        {
            return new System.Collections.Generic.Dictionary<string, ButtonEffectConfig[]>
            {
                ["✨ Elegant & Subtle"] = new[] { WHISPER_SCALE, SILK_SQUEEZE, VELVET_PULSE, PEARL_FLASH },
                ["💫 Dynamic & Energetic"] = new[] { SPRING_BOUNCE, NOVA_FLASH, ELECTRIC_SHAKE, COSMIC_SPIN },
                ["🎭 Dramatic & Impactful"] = new[] { THUNDER_PUNCH, METEOR_BOUNCE, ECLIPSE_TINT, SUPERNOVA_PULSE },
                ["🌸 Themed Colors"] = new[] { ROSE_GOLD_FLASH, OCEAN_TINT, FOREST_SQUEEZE, SUNSET_FLASH },
                ["🎮 Gaming & Interactive"] = new[] { POWER_UP_BOUNCE, COMBO_SHAKE, LEVEL_UP_SPIN, CRYSTAL_PULSE },
                ["🧸 Cute & Playful"] = new[] { BUBBLE_BOUNCE, JELLY_SQUEEZE, POP_FLASH }
            };
        }
    }

    [System.Serializable]
    public enum ButtonPresetType
    {
        [InspectorName("🎨 Custom")]
        Custom,

        [InspectorName("🔲 Unity Default")]
        UnityDefault,

        [InspectorName("✨ Whisper Scale")]
        WhisperScale,
        [InspectorName("✨ Silk Squeeze")]
        SilkSqueeze,
        [InspectorName("✨ Velvet Pulse")]
        VelvetPulse,
        [InspectorName("✨ Pearl Flash")]
        PearlFlash,

        [InspectorName("💫 Spring Bounce")]
        SpringBounce,
        [InspectorName("💫 Nova Flash")]
        NovaFlash,
        [InspectorName("💫 Electric Shake")]
        ElectricShake,
        [InspectorName("💫 Cosmic Spin")]
        CosmicSpin,

        [InspectorName("🎭 Thunder Punch")]
        ThunderPunch,
        [InspectorName("🎭 Meteor Bounce")]
        MeteorBounce,
        [InspectorName("🎭 Eclipse Tint")]
        EclipseTint,
        [InspectorName("🎭 Supernova Pulse")]
        SupernovaPulse,

        [InspectorName("🌸 Rose Gold Flash")]
        RoseGoldFlash,
        [InspectorName("🌸 Ocean Tint")]
        OceanTint,
        [InspectorName("🌸 Forest Squeeze")]
        ForestSqueeze,
        [InspectorName("🌸 Sunset Flash")]
        SunsetFlash,

        [InspectorName("🎮 Power Up Bounce")]
        PowerUpBounce,
        [InspectorName("🎮 Combo Shake")]
        ComboShake,
        [InspectorName("🎮 Level Up Spin")]
        LevelUpSpin,
        [InspectorName("🎮 Crystal Pulse")]
        CrystalPulse,

        [InspectorName("🧸 Bubble Bounce")]
        BubbleBounce,
        [InspectorName("🧸 Jelly Squeeze")]
        JellySqueeze,
        [InspectorName("🧸 Pop Flash")]
        PopFlash
    }
}