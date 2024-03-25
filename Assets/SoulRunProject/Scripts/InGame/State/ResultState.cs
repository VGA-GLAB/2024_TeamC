using System.Collections;
using SoulRunProject.Common;
using SoulRunProject.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ゲームクリア時に行われるステート
    /// </summary>
    public class ResultState : State
    {
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("ゲームクリアステート開始");
        }

        public void ExitToTitle()
        {
            // タイトルへ遷移
            SceneManager.LoadScene(0);
        }

        public void RetryStage()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
