using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using SoulRunProject.Framework;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class EnterBossStageState : State
    {
        private PlayingBossStageState _playingBossStageState;
        
        public EnterBossStageState(PlayingBossStageState playingBossStageState)
        {
            _playingBossStageState = playingBossStageState;
        }
        
        protected override void OnEnter(State currentState)
        {
            // todo ボス戦スタート演出処理
            DebugClass.Instance.ShowLog("ボスステージ開始前ステート開始");
            StateChange();
        }
    }
}
