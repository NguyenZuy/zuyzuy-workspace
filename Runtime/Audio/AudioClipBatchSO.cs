using TriInspector;
using UnityEngine;

namespace ZuyZuy.Workspace
{
    [CreateAssetMenu(fileName = "AudioClipBatchSO", menuName = "ZuyZuy/AudioClipBatchSO")]
    public class AudioClipBatchSO : ScriptableObject
    {
        [Title("Background Music")]
        public AudioData[] backgroundMusic;

        [Title("SFX")]
        public AudioData[] sfx;

        [Title("Ambience")]
        public AudioData[] ambience;

        [Title("Voiceover")]
        public AudioData[] voiceover;

        [Title("Button Click")]
        public AudioData[] buttonClick;
    }
}