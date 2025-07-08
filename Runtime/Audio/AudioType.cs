namespace ZuyZuy.Workspace
{
    /// <summary>
    /// Defines the different categories of audio that can be played in the game.
    /// </summary>
    public enum AudioType
    {
        /// <summary>
        /// Represents an unassigned or null audio type. Used as a default.
        /// </summary>
        None,

        /// <summary>
        /// Background Music (BGM). These are typically long, looping tracks
        /// that set the overall mood or theme of a level, menu, or scene.
        /// </summary>
        BGM,

        /// <summary>
        /// Sound Effects (SFX). These are short, non-looping sounds triggered by
        /// specific game events, such as player actions (e.g., jumping, shooting),
        /// environmental interactions, or impacts.
        /// </summary>
        SFX,

        /// <summary>
        /// Ambient sounds. These are looping background sounds that create
        /// the atmosphere of an environment, such as wind, birds chirping,
        /// a bustling city, or the hum of machinery.
        /// </summary>
        Ambience,

        /// <summary>
        /// Spoken dialogue or narration from characters. This category is for
        /// any voice lines that convey story or information.
        /// </summary>
        Voiceover,

        ButtonClick,
    }
}