using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine; 

namespace SoulRunProject.Common
{
    /// <summary>
    /// ステージ開始処理を行うステート
    /// </summary>
    public class EnterStageState : State
    {
        protected override async UniTask OnEnter(State current, CancellationToken cts)
        {
            Debug.Log("EnterStageState");
            //TODO：カメラ移動、ステージ名表示
            await UniTask.Delay(1000, cancellationToken: cts);
            
        }
    }
}
