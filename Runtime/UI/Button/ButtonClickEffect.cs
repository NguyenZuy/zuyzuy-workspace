using System;
using TriInspector;
using UnityEngine;

namespace ZuyZuy.Workspace
{
    [Serializable]
    public enum ButtonClickEffect
    {
        [InspectorName("🚫 None")]
        None,

        [InspectorName("📏 Scale")]
        Scale,

        [InspectorName("👊 Punch")]
        Punch,

        [InspectorName("📳 Shake")]
        Shake,

        [InspectorName("🔄 Rotation")]
        Rotation,

        [InspectorName("🎨 Color Tint")]
        ColorTint,

        [InspectorName("🏀 Bounce")]
        Bounce,

        [InspectorName("🤏 Squeeze")]
        Squeeze,

        [InspectorName("✨ Flash")]
        Flash,

        [InspectorName("💗 Pulse")]
        Pulse
    }
}