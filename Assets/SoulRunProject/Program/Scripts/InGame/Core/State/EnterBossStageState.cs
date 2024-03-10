using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using SoulRunProject.Framework;
using UnityEngine;

namespace SoulRunProject.Common
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
            DebugClass.Instance.ShowLog("ボスステージ開始前ステート開始");
        }
    }
}
