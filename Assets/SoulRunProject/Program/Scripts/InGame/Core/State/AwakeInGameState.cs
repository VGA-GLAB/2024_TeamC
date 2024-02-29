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
    public class AwakeInGameState : State
    {
        public AwakeInGameState(EnterStageState enterStageState)
        {
            _enterStageState = enterStageState;
        }
        
        EnterStageState _enterStageState;
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("初期化ステート開始");
            Exit(_enterStageState);
        }

        protected override void OnExit(State nextState)
        {
            Debug.Log("AwakeInGameState Exit");
            _enterStageState.Enter(this);
        }
    }
}
