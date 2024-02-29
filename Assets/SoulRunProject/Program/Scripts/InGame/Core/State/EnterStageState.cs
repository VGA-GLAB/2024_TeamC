using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// ステージ開始前の処理を行う
    /// </summary>
    public class EnterStageState : State
    {
        private PlayingRunGameState _playingRunGameState;
        
        public EnterStageState(PlayingRunGameState playingRunGameState)
        {
            _playingRunGameState = playingRunGameState;
        }
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("ステージ開始ステート開始");
            OnExit(_playingRunGameState);
        }
        
        protected override void OnExit(State nextState)
        {
            _playingRunGameState.Enter(this);
        }
    }
}
