using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using SoulRunProject.Framework;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class PlayingBossStageState : State
    {
        private PlayerManager _playerManager;
        
        public PlayingBossStageState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
        
        public bool IsBossDefeated { get; private set; } = false;
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("ボスステージステート開始");
            _playerManager.SetPlayerForwardMover(true);
            //TODO：ボスステージの処理を入れる
            //ボスが倒されたらIsBossDefeatedをtrueにしてStateChangeを呼ぶ
            IsBossDefeated = true;
            StateChange();
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
