using UnityEngine;
using LitMotion;

namespace ZuyZuy.Workspace
{
    public static class PopupAnimationPresets
    {
        #region Gentle Tier

        public static readonly PopupAnimationConfig SILK_FADE = new PopupAnimationConfig(
            PopupAppearanceAnim.Fade,
            0.4f,
            Ease.OutSine,
            Vector3.one,
            Vector2.zero,
            0f
        );

        public static readonly PopupAnimationConfig VELVET_SCALE = new PopupAnimationConfig(
            PopupAppearanceAnim.Scale,
            0.35f,
            Ease.OutQuart,
            new Vector3(0.85f, 0.85f, 1f),
            Vector2.zero,
            0f
        );

        public static readonly PopupAnimationConfig WHISPER_SLIDE = new PopupAnimationConfig(
            PopupAppearanceAnim.SlideFromTop,
            0.45f,
            Ease.OutCubic,
            Vector3.one,
            new Vector2(0, 120f),
            0f
        );

        public static readonly PopupAnimationConfig GENTLE_BOUNCE = new PopupAnimationConfig(
            PopupAppearanceAnim.Bounce,
            0.5f,
            Ease.OutQuad,
            new Vector3(0.9f, 0.9f, 1f),
            Vector2.zero,
            0f
        );

        #endregion

        #region Moderate Tier

        public static readonly PopupAnimationConfig SPRING_SCALE = new PopupAnimationConfig(
            PopupAppearanceAnim.Scale,
            0.3f,
            Ease.OutBack,
            new Vector3(0.7f, 0.7f, 1f),
            Vector2.zero,
            0f
        );

        public static readonly PopupAnimationConfig QUICK_SLIDE = new PopupAnimationConfig(
            PopupAppearanceAnim.SlideFromBottom,
            0.25f,
            Ease.OutQuad,
            Vector3.one,
            new Vector2(0, 120f),
            0f
        );

        public static readonly PopupAnimationConfig SMOOTH_FADE = new PopupAnimationConfig(
            PopupAppearanceAnim.Fade,
            0.3f,
            Ease.OutQuad,
            Vector3.one,
            Vector2.zero,
            0f
        );

        public static readonly PopupAnimationConfig BOUNCY_ENTRY = new PopupAnimationConfig(
            PopupAppearanceAnim.Bounce,
            0.4f,
            Ease.OutElastic,
            new Vector3(0.8f, 0.8f, 1f),
            Vector2.zero,
            0f
        );

        #endregion

        #region Dynamic Tier

        public static readonly PopupAnimationConfig POWER_SCALE = new PopupAnimationConfig(
            PopupAppearanceAnim.Scale,
            0.2f,
            Ease.OutExpo,
            new Vector3(0.5f, 0.5f, 1f),
            Vector2.zero,
            0f
        );

        public static readonly PopupAnimationConfig RAPID_SLIDE = new PopupAnimationConfig(
            PopupAppearanceAnim.SlideFromLeft,
            0.15f,
            Ease.OutCirc,
            Vector3.one,
            new Vector2(200f, 0),
            0f
        );

        public static readonly PopupAnimationConfig SNAP_FADE = new PopupAnimationConfig(
            PopupAppearanceAnim.Fade,
            0.2f,
            Ease.OutExpo,
            Vector3.one,
            Vector2.zero,
            0f
        );

        public static readonly PopupAnimationConfig IMPACT_BOUNCE = new PopupAnimationConfig(
            PopupAppearanceAnim.Bounce,
            0.25f,
            Ease.OutBounce,
            new Vector3(0.6f, 0.6f, 1f),
            Vector2.zero,
            0f
        );

        #endregion

        #region Ethereal Tier

        public static readonly PopupAnimationConfig DREAMY_FADE = new PopupAnimationConfig(
            PopupAppearanceAnim.Fade,
            0.8f,
            Ease.InOutSine,
            Vector3.one,
            Vector2.zero,
            0f
        );

        public static readonly PopupAnimationConfig FLOATING_SCALE = new PopupAnimationConfig(
            PopupAppearanceAnim.Scale,
            0.6f,
            Ease.InOutQuart,
            new Vector3(0.9f, 0.9f, 1f),
            Vector2.zero,
            0f
        );

        public static readonly PopupAnimationConfig GLIDING_SLIDE = new PopupAnimationConfig(
            PopupAppearanceAnim.SlideFromRight,
            0.7f,
            Ease.InOutCubic,
            Vector3.one,
            new Vector2(250f, 0),
            0f
        );

        public static readonly PopupAnimationConfig CLOUD_BOUNCE = new PopupAnimationConfig(
            PopupAppearanceAnim.Bounce,
            0.9f,
            Ease.InOutElastic,
            new Vector3(0.95f, 0.95f, 1f),
            Vector2.zero,
            0f
        );

        #endregion

        #region Gaming Tier

        public static readonly PopupAnimationConfig GAME_OVER_SCALE = new PopupAnimationConfig(
            PopupAppearanceAnim.Scale,
            0.6f,
            Ease.OutBounce,
            new Vector3(0.3f, 0.3f, 1f),
            Vector2.zero,
            0f
        );

        public static readonly PopupAnimationConfig VICTORY_SLIDE = new PopupAnimationConfig(
            PopupAppearanceAnim.SlideFromTop,
            0.5f,
            Ease.OutElastic,
            Vector3.one,
            new Vector2(0, 250f),
            0f
        );

        public static readonly PopupAnimationConfig LEVEL_UP_FADE = new PopupAnimationConfig(
            PopupAppearanceAnim.Fade,
            0.4f,
            Ease.OutQuart,
            Vector3.one,
            Vector2.zero,
            0f
        );

        public static readonly PopupAnimationConfig ACHIEVEMENT_BOUNCE = new PopupAnimationConfig(
            PopupAppearanceAnim.Bounce,
            0.7f,
            Ease.OutBounce,
            new Vector3(0.4f, 0.4f, 1f),
            Vector2.zero,
            0f
        );

        #endregion

        #region Diagonal Slide Tier

        public static readonly PopupAnimationConfig METEOR_STRIKE = new PopupAnimationConfig(
            PopupAppearanceAnim.SlideFromTopRight,
            0.3f,
            Ease.OutCirc,
            Vector3.one,
            new Vector2(150f, 150f),
            0f
        );

        public static readonly PopupAnimationConfig COMET_ENTRY = new PopupAnimationConfig(
            PopupAppearanceAnim.SlideFromTopLeft,
            0.4f,
            Ease.OutQuart,
            Vector3.one,
            new Vector2(180f, 120f),
            0f
        );

        public static readonly PopupAnimationConfig RISING_MOON = new PopupAnimationConfig(
            PopupAppearanceAnim.SlideFromBottomRight,
            0.6f,
            Ease.OutSine,
            Vector3.one,
            new Vector2(120f, 160f),
            0f
        );

        public static readonly PopupAnimationConfig FALLING_LEAF = new PopupAnimationConfig(
            PopupAppearanceAnim.SlideFromBottomLeft,
            0.8f,
            Ease.InOutCubic,
            Vector3.one,
            new Vector2(100f, 140f),
            0f
        );

        public static readonly PopupAnimationConfig DIAGONAL_SWIPE = new PopupAnimationConfig(
            PopupAppearanceAnim.SlideFromTopRight,
            0.2f,
            Ease.OutExpo,
            Vector3.one,
            new Vector2(200f, 120f),
            0f
        );

        public static readonly PopupAnimationConfig CORNER_PEEK = new PopupAnimationConfig(
            PopupAppearanceAnim.SlideFromBottomLeft,
            0.5f,
            Ease.OutBack,
            Vector3.one,
            new Vector2(90f, 110f),
            0f
        );

        #endregion

        #region Utility Methods

        public static PopupAnimationConfig GetPreset(string presetName)
        {
            return presetName.ToUpper() switch
            {
                "SILK_FADE" => SILK_FADE,
                "VELVET_SCALE" => VELVET_SCALE,
                "WHISPER_SLIDE" => WHISPER_SLIDE,
                "GENTLE_BOUNCE" => GENTLE_BOUNCE,

                "SPRING_SCALE" => SPRING_SCALE,
                "QUICK_SLIDE" => QUICK_SLIDE,
                "SMOOTH_FADE" => SMOOTH_FADE,
                "BOUNCY_ENTRY" => BOUNCY_ENTRY,

                "POWER_SCALE" => POWER_SCALE,
                "RAPID_SLIDE" => RAPID_SLIDE,
                "SNAP_FADE" => SNAP_FADE,
                "IMPACT_BOUNCE" => IMPACT_BOUNCE,

                "DREAMY_FADE" => DREAMY_FADE,
                "FLOATING_SCALE" => FLOATING_SCALE,
                "GLIDING_SLIDE" => GLIDING_SLIDE,
                "CLOUD_BOUNCE" => CLOUD_BOUNCE,

                "GAME_OVER_SCALE" => GAME_OVER_SCALE,
                "VICTORY_SLIDE" => VICTORY_SLIDE,
                "LEVEL_UP_FADE" => LEVEL_UP_FADE,
                "ACHIEVEMENT_BOUNCE" => ACHIEVEMENT_BOUNCE,

                "METEOR_STRIKE" => METEOR_STRIKE,
                "COMET_ENTRY" => COMET_ENTRY,
                "RISING_MOON" => RISING_MOON,
                "FALLING_LEAF" => FALLING_LEAF,
                "DIAGONAL_SWIPE" => DIAGONAL_SWIPE,
                "CORNER_PEEK" => CORNER_PEEK,

                _ => SILK_FADE
            };
        }

        public static PopupAnimationConfig[] GetAllPresets()
        {
            return new PopupAnimationConfig[]
            {
                SILK_FADE, VELVET_SCALE, WHISPER_SLIDE, GENTLE_BOUNCE,
                SPRING_SCALE, QUICK_SLIDE, SMOOTH_FADE, BOUNCY_ENTRY,
                POWER_SCALE, RAPID_SLIDE, SNAP_FADE, IMPACT_BOUNCE,
                DREAMY_FADE, FLOATING_SCALE, GLIDING_SLIDE, CLOUD_BOUNCE,
                GAME_OVER_SCALE, VICTORY_SLIDE, LEVEL_UP_FADE, ACHIEVEMENT_BOUNCE,
                METEOR_STRIKE, COMET_ENTRY, RISING_MOON, FALLING_LEAF, DIAGONAL_SWIPE, CORNER_PEEK
            };
        }

        #endregion
    }
}