using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TriInspector;
using UnityEngine;
using LitMotion;
using LitMotion.Extensions;
using ZuyZuy.Workspace.Editor;

namespace ZuyZuy.Workspace
{
    public class AudioManager : MonoBehaviour
    {
        #region Variables

        [Title("Audio Sources")]
        [SerializeField] private AudioSource _backgroundMusicSource;
        [SerializeField] private AudioSource _uiSource;
        [SerializeField] private AudioSource[] _sfxSources;
        [SerializeField] private AudioSource _ambienceSource;
        [SerializeField] private AudioSource _voiceoverSource;

        [Title("Data")]
        [SerializeField] private AudioClipBatchSO _audioClipBatch;

        [Title("Volume Settings")]
        [SerializeField, Range(0f, 1f)] private float _masterVolume = 1f;
        [SerializeField, Range(0f, 1f)] private float _backgroundMusicVolume = 0.7f;
        [SerializeField, Range(0f, 1f)] private float _uiVolume = 1f;
        [SerializeField, Range(0f, 1f)] private float _sfxVolume = 1f;
        [SerializeField, Range(0f, 1f)] private float _ambienceVolume = 0.5f;
        [SerializeField, Range(0f, 1f)] private float _voiceoverVolume = 1f;

        [Title("Fade Settings")]
        [SerializeField] private float _defaultFadeDuration = 1f;

        private int _nextSfxSourceIndex = 0;
        private readonly Dictionary<string, AudioClipData> _audioClipLookup = new();
        private MotionHandle _backgroundMusicFadeHandle;
        private MotionHandle _ambienceFadeHandle;
        private MotionHandle _voiceoverFadeHandle;

        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                _masterVolume = Mathf.Clamp01(value);
                UpdateAllSourceVolumes();
            }
        }

        public float BackgroundMusicVolume
        {
            get => _backgroundMusicVolume;
            set
            {
                _backgroundMusicVolume = Mathf.Clamp01(value);
                UpdateBackgroundMusicVolume();
            }
        }

        public float UIVolume
        {
            get => _uiVolume;
            set
            {
                _uiVolume = Mathf.Clamp01(value);
                UpdateUIVolume();
            }
        }

        public float SFXVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = Mathf.Clamp01(value);
                UpdateSFXVolume();
            }
        }

        public float AmbienceVolume
        {
            get => _ambienceVolume;
            set
            {
                _ambienceVolume = Mathf.Clamp01(value);
                UpdateAmbienceVolume();
            }
        }

        public float VoiceoverVolume
        {
            get => _voiceoverVolume;
            set
            {
                _voiceoverVolume = Mathf.Clamp01(value);
                UpdateVoiceoverVolume();
            }
        }

        #endregion

        #region Unity Methods

        void Awake()
        {
            BuildAudioClipLookup();
        }

        void Start()
        {
#if UNITY_EDITOR
            ValidateSources();
#endif
            InitializeAudioSources();
        }

        void OnDestroy()
        {
            StopAllFades();
        }

        #endregion

        #region Audio Playback Methods

        /// <summary>
        /// Plays an audio clip by name with specified parameters
        /// </summary>
        public void Play(string clipName, AudioType audioType, float volumeMultiplier = 1f, float pitchMultiplier = 1f, bool overrideLoop = false, bool? forceLoop = null)
        {
            if (!TryGetAudioClipData(clipName, out AudioClipData clipData))
                return;

            AudioSource targetSource = GetAudioSourceForType(audioType);
            if (targetSource == null)
                return;

            PlayOnSource(targetSource, clipData, audioType, volumeMultiplier, pitchMultiplier, forceLoop ?? (overrideLoop ? true : clipData.loop));
        }

        /// <summary>
        /// Plays an audio clip as a one-shot (non-looping, doesn't interrupt current audio)
        /// </summary>
        public void PlayOneShot(string clipName, AudioType audioType, float volumeMultiplier = 1f, float pitchMultiplier = 1f)
        {
            if (!TryGetAudioClipData(clipName, out AudioClipData clipData))
                return;

            AudioSource targetSource = GetAudioSourceForType(audioType);
            if (targetSource == null)
                return;

            float finalVolume = CalculateFinalVolume(clipData.volume * volumeMultiplier, audioType);
            targetSource.pitch = clipData.pitch * pitchMultiplier;
            targetSource.PlayOneShot(clipData.clip, finalVolume);
        }

        /// <summary>
        /// Plays background music with fade in effect
        /// </summary>
        public void PlayBackgroundMusic(string clipName, float fadeDuration = -1f, bool forceRestart = false)
        {
            if (!TryGetAudioClipData(clipName, out AudioClipData clipData))
                return;

            if (!forceRestart && _backgroundMusicSource.isPlaying && _backgroundMusicSource.clip == clipData.clip)
                return;

            fadeDuration = fadeDuration < 0 ? _defaultFadeDuration : fadeDuration;

            StopBackgroundMusicFade();

            if (_backgroundMusicSource.isPlaying)
            {
                FadeOutAndPlay(_backgroundMusicSource, clipData, AudioType.BGM, fadeDuration);
            }
            else
            {
                PlayOnSource(_backgroundMusicSource, clipData, AudioType.BGM, 1f, 1f, clipData.loop);
                FadeIn(_backgroundMusicSource, AudioType.BGM, fadeDuration);
            }
        }

        /// <summary>
        /// Stops background music with fade out effect
        /// </summary>
        public void StopBackgroundMusic(float fadeDuration = -1f)
        {
            fadeDuration = fadeDuration < 0 ? _defaultFadeDuration : fadeDuration;
            StopBackgroundMusicFade();
            FadeOut(_backgroundMusicSource, fadeDuration);
        }

        /// <summary>
        /// Plays ambience audio with fade in effect
        /// </summary>
        public void PlayAmbience(string clipName, float fadeDuration = -1f)
        {
            if (!TryGetAudioClipData(clipName, out AudioClipData clipData))
                return;

            fadeDuration = fadeDuration < 0 ? _defaultFadeDuration : fadeDuration;

            StopAmbienceFade();
            PlayOnSource(_ambienceSource, clipData, AudioType.Ambience, 1f, 1f, true);
            FadeIn(_ambienceSource, AudioType.Ambience, fadeDuration);
        }

        /// <summary>
        /// Stops ambience audio with fade out effect
        /// </summary>
        public void StopAmbience(float fadeDuration = -1f)
        {
            fadeDuration = fadeDuration < 0 ? _defaultFadeDuration : fadeDuration;
            StopAmbienceFade();
            FadeOut(_ambienceSource, fadeDuration);
        }

        /// <summary>
        /// Plays voiceover audio
        /// </summary>
        public void PlayVoiceover(string clipName, float fadeDuration = -1f)
        {
            if (!TryGetAudioClipData(clipName, out AudioClipData clipData))
                return;

            fadeDuration = fadeDuration < 0 ? _defaultFadeDuration : fadeDuration;

            StopVoiceoverFade();
            PlayOnSource(_voiceoverSource, clipData, AudioType.Voiceover, 1f, 1f, clipData.loop);

            if (fadeDuration > 0)
                FadeIn(_voiceoverSource, AudioType.Voiceover, fadeDuration);
        }

        /// <summary>
        /// Stops voiceover audio
        /// </summary>
        public void StopVoiceover(float fadeDuration = -1f)
        {
            fadeDuration = fadeDuration < 0 ? _defaultFadeDuration : fadeDuration;
            StopVoiceoverFade();

            if (fadeDuration > 0)
                FadeOut(_voiceoverSource, fadeDuration);
            else
                _voiceoverSource.Stop();
        }

        /// <summary>
        /// Stops all audio immediately
        /// </summary>
        public void StopAllAudio()
        {
            StopAllFades();

            _backgroundMusicSource.Stop();
            _uiSource.Stop();
            _ambienceSource.Stop();
            _voiceoverSource.Stop();

            foreach (var sfxSource in _sfxSources)
                sfxSource.Stop();
        }

        /// <summary>
        /// Pauses all audio
        /// </summary>
        public void PauseAllAudio()
        {
            _backgroundMusicSource.Pause();
            _uiSource.Pause();
            _ambienceSource.Pause();
            _voiceoverSource.Pause();

            foreach (var sfxSource in _sfxSources)
                sfxSource.Pause();
        }

        /// <summary>
        /// Resumes all paused audio
        /// </summary>
        public void ResumeAllAudio()
        {
            _backgroundMusicSource.UnPause();
            _uiSource.UnPause();
            _ambienceSource.UnPause();
            _voiceoverSource.UnPause();

            foreach (var sfxSource in _sfxSources)
                sfxSource.UnPause();
        }

        #endregion

        #region Utility Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryGetAudioClipData(string clipName, out AudioClipData clipData)
        {
            if (_audioClipLookup.TryGetValue(clipName, out clipData))
                return true;

#if UNITY_EDITOR
            ZuyLogger.LogError("AudioManager", $"Audio clip '{clipName}' not found in audio batch");
#endif
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private AudioSource GetAudioSourceForType(AudioType audioType)
        {
            return audioType switch
            {
                AudioType.BGM => _backgroundMusicSource,
                AudioType.UI => _uiSource,
                AudioType.SFX => GetNextAvailableSFXSource(),
                AudioType.Ambience => _ambienceSource,
                AudioType.Voiceover => _voiceoverSource,
                _ => null
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private AudioSource GetNextAvailableSFXSource()
        {
            // Round-robin selection of SFX sources
            AudioSource selectedSource = _sfxSources[_nextSfxSourceIndex];
            _nextSfxSourceIndex = (_nextSfxSourceIndex + 1) % _sfxSources.Length;
            return selectedSource;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void PlayOnSource(AudioSource source, AudioClipData clipData, AudioType audioType, float volumeMultiplier, float pitchMultiplier, bool loop)
        {
            source.clip = clipData.clip;
            source.volume = CalculateFinalVolume(clipData.volume * volumeMultiplier, audioType);
            source.pitch = clipData.pitch * pitchMultiplier;
            source.loop = loop;
            source.Play();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float CalculateFinalVolume(float baseVolume, AudioType audioType)
        {
            float typeVolume = audioType switch
            {
                AudioType.BGM => _backgroundMusicVolume,
                AudioType.UI => _uiVolume,
                AudioType.SFX => _sfxVolume,
                AudioType.Ambience => _ambienceVolume,
                AudioType.Voiceover => _voiceoverVolume,
                _ => 1f
            };

            return baseVolume * typeVolume * _masterVolume;
        }

        private void BuildAudioClipLookup()
        {
            _audioClipLookup.Clear();

            if (_audioClipBatch == null)
                return;

            AddClipsToLookup(_audioClipBatch.backgroundMusic);
            AddClipsToLookup(_audioClipBatch.ui);
            AddClipsToLookup(_audioClipBatch.sfx);
            AddClipsToLookup(_audioClipBatch.ambience);
            AddClipsToLookup(_audioClipBatch.voiceover);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddClipsToLookup(AudioClipData[] clips)
        {
            if (clips == null) return;

            foreach (var clipData in clips)
            {
                if (clipData?.clip != null)
                    _audioClipLookup[clipData.clip.name] = clipData;
            }
        }

        private void InitializeAudioSources()
        {
            UpdateAllSourceVolumes();
        }

        private void UpdateAllSourceVolumes()
        {
            UpdateBackgroundMusicVolume();
            UpdateUIVolume();
            UpdateSFXVolume();
            UpdateAmbienceVolume();
            UpdateVoiceoverVolume();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateBackgroundMusicVolume()
        {
            if (_backgroundMusicSource != null)
                _backgroundMusicSource.volume = CalculateFinalVolume(1f, AudioType.BGM);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateUIVolume()
        {
            if (_uiSource != null)
                _uiSource.volume = CalculateFinalVolume(1f, AudioType.UI);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateSFXVolume()
        {
            if (_sfxSources != null)
            {
                foreach (var source in _sfxSources)
                {
                    if (source != null)
                        source.volume = CalculateFinalVolume(1f, AudioType.SFX);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateAmbienceVolume()
        {
            if (_ambienceSource != null)
                _ambienceSource.volume = CalculateFinalVolume(1f, AudioType.Ambience);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateVoiceoverVolume()
        {
            if (_voiceoverSource != null)
                _voiceoverSource.volume = CalculateFinalVolume(1f, AudioType.Voiceover);
        }

        #endregion

        #region Fade Effects

        private void FadeIn(AudioSource source, AudioType audioType, float duration)
        {
            float targetVolume = CalculateFinalVolume(1f, audioType);
            source.volume = 0f;

            LMotion.Create(0f, targetVolume, duration)
                .WithEase(Ease.OutCubic)
                .Bind(x => source.volume = x);
        }

        private void FadeOut(AudioSource source, float duration)
        {
            LMotion.Create(source.volume, 0f, duration)
                .WithEase(Ease.InCubic)
                .WithOnComplete(() => source.Stop())
                .Bind(x => source.volume = x);
        }

        private void FadeOutAndPlay(AudioSource source, AudioClipData newClipData, AudioType audioType, float duration)
        {
            float halfDuration = duration * 0.5f;

            LMotion.Create(source.volume, 0f, halfDuration)
                .WithEase(Ease.InCubic)
                .WithOnComplete(() =>
                {
                    PlayOnSource(source, newClipData, audioType, 1f, 1f, newClipData.loop);
                    FadeIn(source, audioType, halfDuration);
                })
                .Bind(x => source.volume = x);
        }

        private void StopAllFades()
        {
            StopBackgroundMusicFade();
            StopAmbienceFade();
            StopVoiceoverFade();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void StopBackgroundMusicFade()
        {
            if (_backgroundMusicFadeHandle.IsActive())
                _backgroundMusicFadeHandle.Cancel();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void StopAmbienceFade()
        {
            if (_ambienceFadeHandle.IsActive())
                _ambienceFadeHandle.Cancel();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void StopVoiceoverFade()
        {
            if (_voiceoverFadeHandle.IsActive())
                _voiceoverFadeHandle.Cancel();
        }

        #endregion

        #region Editor Testing Methods

#if UNITY_EDITOR
        [Button("Test Play SFX")]
        private void TestPlaySFX()
        {
            if (_audioClipBatch?.sfx != null && _audioClipBatch.sfx.Length > 0)
                PlayOneShot(_audioClipBatch.sfx[0].clip.name, AudioType.SFX);
        }

        [Button("Test Play BGM")]
        private void TestPlayBGM()
        {
            if (_audioClipBatch?.backgroundMusic != null && _audioClipBatch.backgroundMusic.Length > 0)
                PlayBackgroundMusic(_audioClipBatch.backgroundMusic[0].clip.name);
        }

        [Button("Stop All Audio")]
        private void EditorStopAllAudio()
        {
            StopAllAudio();
        }

        private void ValidateSources()
        {
            if (_backgroundMusicSource == null)
                ZuyLogger.LogError("AudioManager", "Background music source is not assigned");

            if (_uiSource == null)
                ZuyLogger.LogError("AudioManager", "UI source is not assigned");

            if (_sfxSources == null || _sfxSources.Length == 0)
                ZuyLogger.LogError("AudioManager", "SFX sources are not assigned");

            if (_ambienceSource == null)
                ZuyLogger.LogError("AudioManager", "Ambience source is not assigned");

            if (_voiceoverSource == null)
                ZuyLogger.LogError("AudioManager", "Voiceover source is not assigned");

            if (_audioClipBatch == null)
                ZuyLogger.LogError("AudioManager", "Audio clip batch is not assigned");
        }
#endif

        #endregion
    }
}