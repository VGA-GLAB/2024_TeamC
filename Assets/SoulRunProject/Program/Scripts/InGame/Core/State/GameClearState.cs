using System.Collections;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ゲームクリア時に行われるステート
    /// </summary>
    public class GameClearState : State
    {
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("ゲームクリアステート開始");
        }
    }
}
