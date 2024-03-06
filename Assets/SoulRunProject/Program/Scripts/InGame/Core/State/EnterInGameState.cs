using System.Threading;
using Cysharp.Threading.Tasks;
using SoulRunProject.InGame.Field;
using UnityEngine;
using VContainer.Unity;

namespace SoulRunProject.Common
{
    /// <summary>
    /// インゲーム開始時に最初に一度呼ばれるステート
    /// 初期化処理を行う。
    /// 終わり次第ステージ開始ステートへ遷移する
    /// </summary>
    public class EnterInGameState : State
    {
        PlayerCamera _playerCamera;
        
        public EnterInGameState(PlayerCamera camera)
        {
            _playerCamera = camera;
        }
        
        protected override async UniTask OnEnterAsync(State currentState, CancellationToken cts)
        {
            DebugClass.Instance.ShowLog("インゲーム開始ステート開始");
            await _playerCamera.DoStartIngameMove(_playerCamera.GetCancellationTokenOnDestroy());
            _playerCamera.StartFollowPlayer();
            StateChange();
        }

        protected override void OnExit(State nextState)
        {
            nextState.Enter(this);
        }
    }
}
