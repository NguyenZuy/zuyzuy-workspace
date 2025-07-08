using TriInspector;
using UnityEngine;

namespace ZuyZuy.Workspace
{
    [CreateAssetMenu(fileName = "AudioClipBatchSO", menuName = "ZuyZuy/AudioClipBatchSO")]
    public class AudioClipBatchSO : ScriptableObject
    {
        [Title("Background Music")]
        public AudioClipData[] backgroundMusic;

        [Title("UI")]
        public AudioClipData[] ui;

        [Title("SFX")]
        public AudioClipData[] sfx;

        [Title("Ambience")]
        public AudioClipData[] ambience;

        [Title("Voiceover")]
        public AudioClipData[] voiceover;

        void OnValidate()
        {
            if (backgroundMusic != null && backgroundMusic.Length > 0)
                foreach (var clip in backgroundMusic)
                    clip.type = AudioType.BGM;

            if (ui != null && ui.Length > 0)
                foreach (var clip in ui)
                    clip.type = AudioType.UI;

            if (sfx != null && sfx.Length > 0)
                foreach (var clip in sfx)
                    clip.type = AudioType.SFX;

            if (ambience != null && ambience.Length > 0)
                foreach (var clip in ambience)
                    clip.type = AudioType.Ambience;

            if (voiceover != null && voiceover.Length > 0)
                foreach (var clip in voiceover)
                    clip.type = AudioType.Voiceover;
        }
    }
}