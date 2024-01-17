using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun
{
    public enum CurrentState
    {
        Title,
        StageSelect,
    }

    /// <summary>
    /// タイトルシーンのマネージャクラス
    /// </summary>
    public class TitleSceneManager : MonoBehaviour
    {
        CurrentState _currentState = CurrentState.Title;
        [SerializeField]
        private Canvas _titleCanvas;
        [SerializeField]
        private Canvas _stageSelectCanvas;

        private Action _onNextStateLoad;

        private void Start()
        {
            ChanageState(CurrentState.Title);
        }

        private void ChanageState(CurrentState nextState)
        {
            _onNextStateLoad?.Invoke();
            _currentState = nextState;
            switch(_currentState)
            {
                case CurrentState.Title:
                    TitleState();
                    break;
                case CurrentState.StageSelect:
                    StageSelectState();
                    break;
            }
        }

        private void TitleState()
        {
            _onNextStateLoad = () => _titleCanvas.gameObject.SetActive(false);
            _titleCanvas.gameObject.SetActive(true);
        }

        private void StageSelectState()
        {
            _onNextStateLoad = () => _stageSelectCanvas.gameObject.SetActive(false);
            _stageSelectCanvas.gameObject.SetActive(true);
        }
    }
}
