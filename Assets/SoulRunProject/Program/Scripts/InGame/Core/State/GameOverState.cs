using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.Framework;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class GameOverState : State
    {
        PlayerManager _playerManager;
        
        public GameOverState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("ゲームオーバーステート開始");
            _playerManager.SwitchPause(true);
        }
    }
}
