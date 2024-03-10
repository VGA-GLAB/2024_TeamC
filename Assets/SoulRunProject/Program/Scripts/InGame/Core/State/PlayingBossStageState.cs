using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using SoulRunProject.Framework;
using UnityEngine;

namespace SoulRunProject.Common
{
    public class PlayingBossStageState : State
    {
        public bool IsBossDefeated { get; private set; } = false;
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("ボスステージ開始ステート開始");
        }
        
        protected override void OnUpdate()
        {
            // ボスステージのプレイ中の処理
            if (IsBossDefeated)
            {
                StateChange();
            }
        }
    }
}
