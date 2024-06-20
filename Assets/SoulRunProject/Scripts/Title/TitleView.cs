using System;
using System.Collections;
using UnityEngine;
using SoulRun.InGame;
using UnityEngine.Rendering.Universal;

namespace SoulRunProject.Title
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private InputUIButton _startButton;
        [SerializeField] private InputUIButton _optionButton;
        [SerializeField] private InputUIButton _exitSimButton;
        [SerializeField] private Light2D _soulLight2D;
        [SerializeField] private ParticleSystem _particleSystem;

        public InputUIButton StartButton => _startButton;
        public InputUIButton OptionButton => _optionButton;
        public InputUIButton ExitButton => _exitSimButton;
        public float minFalloffIntensity = 0.5f;
        public float maxFalloffIntensity = 1f;
        public float duration = 2.0f; 

        private void Start()
        {
            Debug.Log(_particleSystem);
            _particleSystem.Play();
            StartCoroutine(AnimateLightFalloff());
        }

        private IEnumerator AnimateLightFalloff()
        {
            while (true)
            {
                float time = 0;
                while (time < duration)
                {
                    _soulLight2D.intensity = Mathf.Lerp(minFalloffIntensity, maxFalloffIntensity, time / duration);
                    time += Time.deltaTime;
                    yield return null;
                }

                time = 0;
                while (time < duration)
                {
                    _soulLight2D.intensity = Mathf.Lerp(maxFalloffIntensity, minFalloffIntensity, time / duration);
                    time += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}