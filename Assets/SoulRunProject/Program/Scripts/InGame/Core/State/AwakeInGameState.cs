using Cysharp.Threading.Tasks;
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
        //ToDo: ソウルのロード、フィールドのロード、プレイヤーステータスのロードを行う。
        protected override void OnEnter()
        {
            Debug.Log("AwakeInGameState");
        }
        
        protected override void OnExit()
        {
            Debug.Log("AwakeInGameState Exit");
            var cts = this.GetCancellationTokenOnDestroy();
            _nextState.Enter(cts).Forget();
        }
    }
}
