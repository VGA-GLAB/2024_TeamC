using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace SoulRunProject.Common
{
    /// <summary>
    /// インゲームの進行を管理するクラス
    /// </summary>
    public class InGameManager : MonoBehaviour
    {
        [Inject]
        private State _currenState;

        private void Start()
        {
            DebugClass.Instance.ShowLog("InGameManager起動");
            var token = this.GetCancellationTokenOnDestroy();
            _currenState.Enter(null, token).Forget();
        }
    }
}
