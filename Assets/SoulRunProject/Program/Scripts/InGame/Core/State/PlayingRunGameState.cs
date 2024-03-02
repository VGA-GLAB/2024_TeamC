using System.Collections;
using System.Collections.Generic;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// ランゲームプレイ中の管理を行うクラス
    /// </summary>
    public class PlayingRunGameState : State
    {
        private PlayerMovement _playerMovement;
        private TestPlayerForwardMover _testPlayerForwardMover;
        public PlayingRunGameState(PlayerMovement playerMovement, TestPlayerForwardMover testPlayerForwardMover)
        {
            _playerMovement = playerMovement;
            _testPlayerForwardMover = testPlayerForwardMover;
        }
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("プレイ中ステート開始");
            _playerMovement.enabled = true;
            _testPlayerForwardMover.IsActivate(true);
        }
        
    }
}
