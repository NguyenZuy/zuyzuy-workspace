using TriInspector;
using UnityEngine;

namespace ZuyZuy.Workspace
{
    [System.Serializable]
    public class AudioClipData
    {
        public AudioType type;
        [Required]
        public AudioClip clip;
        [Range(0, 1)]
        public float volume = 1f;
        [Range(0.5f, 2f)]
        public float pitch = 1f; // can be used to change the speed of the audio
        public bool loop = false;
    }
}