using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace SoulRun.InGame
{
    /// <summary>
    /// インゲームの通常プレイ時のステート
    /// スタートステートから遷移してくる
    /// プレイヤーが一定の地点に到達したらボスステートに遷移させる
    /// </summary>
    public class InGamePlayingState : IInGameState
    {
        private PlayerManager _playerManager;
        private float _playerMoveVerticalMoveLimit = 1500;


        public event Func<UniTask> OnStateStart;
        public event Func<UniTask> OnStateEnd;
        public event Action<IInGameState> OnStateExit;

        public InGamePlayingState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void OnEnterState()
        {
            _playerManager.Active();
        }

        public void OnExitState()
        {
            throw new NotImplementedException();
        }

        public void OnUpdateState()
        {
            if (_playerManager.transform.position.z >= _playerMoveVerticalMoveLimit)
            {
                _playerManager.transform.position = 
                    new Vector3(_playerManager.transform.position.x, _playerManager.transform.position.y, _playerMoveVerticalMoveLimit);
                _playerManager.StopForwardMove();
            }
        }
    }
}
