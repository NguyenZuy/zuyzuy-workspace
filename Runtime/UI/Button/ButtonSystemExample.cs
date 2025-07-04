using UnityEngine;
using TriInspector;

namespace ZuyZuy.Workspace
{
    /// <summary>
    /// Example script demonstrating how to use the UIButton system
    /// This can be attached to any GameObject to control buttons programmatically
    /// </summary>
    public class ButtonSystemExample : MonoBehaviour
    {
        #region Variables

        [Title("Button References")]
        [SerializeField] private UIButton[] testButtons;

        [Title("Effect Testing")]
        [SerializeField] private ButtonClickEffect currentTestEffect = ButtonClickEffect.Scale;
        [SerializeField] private bool cycleEffectsAutomatically = false;
        [SerializeField] private float cycleDuration = 2f;

        private int _currentEffectIndex = 0;
        private float _lastCycleTime;

        #endregion

        #region Unity Methods

        private void Start()
        {
            SetupExampleButtons();
        }

        private void Update()
        {
            if (cycleEffectsAutomatically && Time.time - _lastCycleTime >= cycleDuration)
            {
                CycleToNextEffect();
                _lastCycleTime = Time.time;
            }
        }

        #endregion

        #region UI Methods

        public void ApplyEffectToAllButtons(ButtonClickEffect effect)
        {
            foreach (var button in testButtons)
            {
                if (button != null)
                    button.SetClickEffect(effect);
            }
        }

        public void PlayAllButtonEffects()
        {
            foreach (var button in testButtons)
            {
                if (button != null)
                    button.PlayEffect();
            }
        }

        public void DisableAllButtons()
        {
            foreach (var button in testButtons)
            {
                if (button != null)
                    button.SetInteractable(false);
            }
        }

        public void EnableAllButtons()
        {
            foreach (var button in testButtons)
            {
                if (button != null)
                    button.SetInteractable(true);
            }
        }

        #endregion

        #region OnClick Methods

        public void OnExampleButtonClicked(int buttonIndex)
        {
            Debug.Log($"Example Button {buttonIndex} was clicked!");
        }

        public void OnEffectButtonClicked(string effectName)
        {
            if (System.Enum.TryParse<ButtonClickEffect>(effectName, out var effect))
            {
                ApplyEffectToAllButtons(effect);
                Debug.Log($"Applied {effectName} effect to all buttons");
            }
        }

        #endregion

        #region Utility Methods

        private void SetupExampleButtons()
        {
            // Setup example button configurations
            if (testButtons == null || testButtons.Length == 0) return;

            // Apply different effects to demonstrate variety
            var effects = System.Enum.GetValues(typeof(ButtonClickEffect)) as ButtonClickEffect[];

            for (int i = 0; i < testButtons.Length && i < effects.Length; i++)
            {
                if (testButtons[i] != null)
                {
                    testButtons[i].SetClickEffect(effects[i]);

                    // Add event listeners
                    int buttonIndex = i; // Capture for closure
                    testButtons[i].onButtonClick.AddListener(() => OnExampleButtonClicked(buttonIndex));
                    testButtons[i].onEffectComplete.AddListener(() => Debug.Log($"Button {buttonIndex} effect completed"));
                }
            }
        }

        private void CycleToNextEffect()
        {
            var effects = System.Enum.GetValues(typeof(ButtonClickEffect)) as ButtonClickEffect[];
            _currentEffectIndex = (_currentEffectIndex + 1) % effects.Length;
            currentTestEffect = effects[_currentEffectIndex];

            ApplyEffectToAllButtons(currentTestEffect);
            Debug.Log($"Cycled to effect: {currentTestEffect}");
        }

        [Button("Apply Current Test Effect")]
        private void ApplyCurrentTestEffect()
        {
            ApplyEffectToAllButtons(currentTestEffect);
        }

        [Button("Trigger All Effects")]
        private void TriggerAllEffects()
        {
            PlayAllButtonEffects();
        }

        [Button("Apply Random Effects")]
        private void ApplyRandomEffects()
        {
            var effects = System.Enum.GetValues(typeof(ButtonClickEffect)) as ButtonClickEffect[];

            foreach (var button in testButtons)
            {
                if (button != null)
                {
                    var randomEffect = effects[Random.Range(0, effects.Length)];
                    button.SetClickEffect(randomEffect);
                }
            }
        }

        [Button("Load Preset: Subtle UI")]
        private void LoadSubtlePreset()
        {
            foreach (var button in testButtons)
            {
                if (button != null)
                {
                    button.SetClickEffect(ButtonClickEffect.Scale);
                    // Note: You would need to expose setters in UIButton to fully apply presets
                }
            }
        }

        [Button("Load Preset: Dramatic UI")]
        private void LoadDramaticPreset()
        {
            var dramaticEffects = new ButtonClickEffect[]
            {
                ButtonClickEffect.Bounce,
                ButtonClickEffect.Punch,
                ButtonClickEffect.Shake
            };

            for (int i = 0; i < testButtons.Length; i++)
            {
                if (testButtons[i] != null)
                {
                    var effect = dramaticEffects[i % dramaticEffects.Length];
                    testButtons[i].SetClickEffect(effect);
                }
            }
        }

        #endregion
    }
}