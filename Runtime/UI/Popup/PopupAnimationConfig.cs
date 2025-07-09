using UnityEngine;
using LitMotion;

namespace ZuyZuy.Workspace
{
    [System.Serializable]
    public class PopupAnimationConfig
    {
        [Header("🎭 Animation Settings")]
        public PopupAppearanceAnim animationType;
        public float duration;
        public Ease easeType;

        [Header("📏 Scale Settings")]
        public Vector3 scaleStart;

        [Header("📍 Slide Settings")]
        public Vector2 slideOffset;

        [Header("🌫️ Fade Settings")]
        public float fadeStartAlpha;

        public PopupAnimationConfig()
        {
            animationType = PopupAppearanceAnim.Fade;
            duration = 0.3f;
            easeType = Ease.OutQuad;
            scaleStart = new Vector3(0.8f, 0.8f, 1f);
            slideOffset = new Vector2(0, 100f);
            fadeStartAlpha = 0f;
        }

        public PopupAnimationConfig(PopupAppearanceAnim type, float dur, Ease ease, Vector3 scale, Vector2 slide, float fadeAlpha)
        {
            animationType = type;
            duration = dur;
            easeType = ease;
            scaleStart = scale;
            slideOffset = slide;
            fadeStartAlpha = fadeAlpha;
        }
    }
}