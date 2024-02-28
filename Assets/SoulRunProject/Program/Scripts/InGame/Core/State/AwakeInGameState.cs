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
        //ToDo: ソウルのロード、フィールドのロード、プレイヤーステータスのロードを行う。
        private FieldMover _fieldMover;
        protected override void OnEnter()
        {
            _fieldMover.CreateField();
        }
        
        protected override void OnExit()
        {
            Debug.Log("AwakeInGameState Exit");
            var cts = _owner.GetCancellationTokenOnDestroy();
            _nextState.Enter(cts).Forget();
        }
    }
}
