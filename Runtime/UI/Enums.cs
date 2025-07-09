namespace ZuyZuy.Workspace
{
    public enum PopupAppearanceAnim
    {
        Fade,
        Scale,
        SlideFromTop,
        SlideFromBottom,
        SlideFromLeft,
        SlideFromRight,
        SlideFromTopLeft,
        SlideFromTopRight,
        SlideFromBottomLeft,
        SlideFromBottomRight,
        Bounce
    }

    public enum DialogType
    {
        Alert,
        Confirm,
        Input
    }

    public enum PopupPresetType
    {
        Custom,

        // Gentle tier
        SilkFade,
        VelvetScale,
        WhisperSlide,
        GentleBounce,

        // Moderate tier
        SpringScale,
        QuickSlide,
        SmoothFade,
        BouncyEntry,

        // Dynamic tier
        PowerScale,
        RapidSlide,
        SnapFade,
        ImpactBounce,

        // Ethereal tier
        DreamyFade,
        FloatingScale,
        GlidingSlide,
        CloudBounce,

        // Gaming tier
        GameOverScale,
        VictorySlide,
        LevelUpFade,
        AchievementBounce,

        // Diagonal Slide tier
        MeteorStrike,      // SlideFromTopRight
        CometEntry,        // SlideFromTopLeft  
        RisingMoon,        // SlideFromBottomRight
        FallingLeaf,       // SlideFromBottomLeft
        DiagonalSwipe,     // SlideFromTopRight (faster)
        CornerPeek         // SlideFromBottomLeft (gentle)
    }

    // public enum ButtonClickEffect
    // {
    //     None,
    //     Scale,
    //     Punch,
    //     Shake,
    //     Rotation,
    //     ColorTint,
    //     Bounce,
    //     Squeeze,
    //     Flash,
    //     Pulse
}