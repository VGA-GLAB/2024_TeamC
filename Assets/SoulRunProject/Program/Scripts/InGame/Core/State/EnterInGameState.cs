using System.Threading;
using Cysharp.Threading.Tasks;
using SoulRunProject.InGame.Field;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// インゲーム開始時に最初に一度呼ばれるステート
    /// 初期化処理を行う。
    /// 終わり次第ステージ開始ステートへ遷移する
    /// </summary>
    public class EnterInGameState : State
    {
        EnterStageState _enterStageState;
        PlayerCamera _playerCamera;
        public EnterInGameState(EnterStageState enterStageState, PlayerCamera camera)
        {
            _enterStageState = enterStageState;
            _playerCamera = camera;
        }
        
        protected override async UniTask OnEnter(State currentState, CancellationToken cts)
        {
            DebugClass.Instance.ShowLog("初期化ステート開始");
            await _playerCamera.DoStartIngameMove(_playerCamera.GetCancellationTokenOnDestroy());
            _playerCamera.StartFollowPlayer();
            Exit(_enterStageState);
        }

        protected override void OnExit(State nextState)
        {
            _enterStageState.Enter(this);
        }
    }
}
