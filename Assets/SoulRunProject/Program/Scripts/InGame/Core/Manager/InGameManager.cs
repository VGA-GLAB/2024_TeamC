using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// インゲームの進行を管理するクラス
    /// </summary>
    public class InGameManager : MonoBehaviour
    {
        private State _firstState;

        private void Start()
        {
            var cts = this.GetCancellationTokenOnDestroy();
            _firstState.Enter(cts).Forget();
        }
    }
}
