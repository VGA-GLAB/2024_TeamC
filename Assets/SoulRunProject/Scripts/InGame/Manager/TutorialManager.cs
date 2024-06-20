using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UniRx;

namespace SoulRunProject.InGame
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Image _fadePanel;
        [SerializeReference, SubclassSelector] private List<TutorialContentBase> _tutorialContents;
        [SerializeField] private EndTutorial _endTutorial;

        private void Start()
        {
            foreach (var tutorialContent in _tutorialContents)
            {
                tutorialContent.TutorialUI.SetActive(false);
            }

            _fadePanel.gameObject.SetActive(false);
            _ = TakeTutorial();
        }

        /// <summary>
        /// チュートリアル再生
        /// </summary>
        async UniTask TakeTutorial()
        {
            _playerInput.MoveInputActive = false;
            _playerInput.JumpInputActive = false;
            _playerInput.ShiftInputActive = false;
            
            foreach (var tutorialContent in _tutorialContents)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(tutorialContent.StandbyTime));
                _fadePanel.gameObject.SetActive(true);
                tutorialContent.TutorialUI.SetActive(true);
                await tutorialContent.WaitAction(_playerInput);
                _fadePanel.gameObject.SetActive(false);
                tutorialContent.TutorialUI.SetActive(false);
            }
            
            _endTutorial.Initialize(_fadePanel);
            await _endTutorial.WaitAction();
        }
    }

    [Serializable, Name("移動")]
    public class MoveTutorial : TutorialContentBase
    {
        public override async UniTask WaitAction(PlayerInput input)
        {
            PauseManager.Pause(true);
            input.MoveInputActive = true;
            input.ReflectInput();
            await input.MoveInput;
            await UniTask.WaitUntil(() => input.MoveInput.Value != Vector2.zero);
            PauseManager.Pause(false);
        }
    }

    [Serializable, Name("ジャンプ")]
    public class JumpTutorial : TutorialContentBase
    {
        public override async UniTask WaitAction(PlayerInput input)
        {
            PauseManager.Pause(true);
            input.JumpInputActive = true;
            input.ReflectInput();
            await UniTask.WaitUntil(() => input.JumpInput.Value);
            PauseManager.Pause(false);
        }
    }

    [Serializable, Name("ソウル技")]
    public class SoulSkillTutorial : TutorialContentBase
    {
        public override async UniTask WaitAction(PlayerInput input)
        {
            PauseManager.Pause(true);
            input.ShiftInputActive = true;
            input.ReflectInput();
            await input.ShiftInput;
            await UniTask.WaitUntil(() => input.ShiftInput.Value);
            PauseManager.Pause(false);
        }
    }

    [Serializable, Name("チュートリアル終了")]
    public class EndTutorial
    {
        [SerializeField] private float _standbyTime;
        [SerializeField] private float _fadeTime;
        [SerializeField] private string _loadSceneName;
        
        private Image _fadePanel;
        
        public void Initialize(Image fadePanel)
        {
            _fadePanel = fadePanel;
        }
        
        public async UniTask WaitAction()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_standbyTime));
            _fadePanel.gameObject.SetActive(true);
            Color color = _fadePanel.color;
            color.a = 0;
            _fadePanel.color = color;
            await _fadePanel.DOFade(1, _fadeTime);
            SceneManager.LoadScene(_loadSceneName);
        }
    }

    [Serializable, Name("基底クラス")]
    public class TutorialContentBase
    {
        [SerializeField] private float _standbyTime;
        [SerializeField] private GameObject _tutorialUI;

        public float StandbyTime => _standbyTime;
        public GameObject TutorialUI => _tutorialUI;

        public virtual async UniTask WaitAction(PlayerInput input){}
    }
}
