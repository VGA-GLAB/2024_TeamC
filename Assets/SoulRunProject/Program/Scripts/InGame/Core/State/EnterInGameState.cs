using System.Threading;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using SoulRunProject.Framework;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// インゲーム開始時に最初に一度呼ばれるステート
    /// 初期化処理を行う。
    /// 終わり次第ステージ開始ステートへ遷移する
    /// </summary>
    public class EnterInGameState : State
    {
        PlayerCamera _playerCamera;
        PlayerManager _playerManager;
        
        public EnterInGameState(PlayerCamera camera, PlayerManager playerManager)
        {
            _playerCamera = camera;
            _playerManager = playerManager;
        }
        
        protected override async UniTask OnEnterAsync(State currentState, CancellationToken cts)
        {
            DebugClass.Instance.ShowLog("インゲーム開始ステート開始");
            _playerManager.SwitchPause(true);
            await _playerCamera.DoStartIngameMove(_playerCamera.GetCancellationTokenOnDestroy());
            _playerCamera.StartFollowPlayer();
            StateChange();
        }

        protected override void OnExit(State nextState)
        {
            
        }
    }
}
