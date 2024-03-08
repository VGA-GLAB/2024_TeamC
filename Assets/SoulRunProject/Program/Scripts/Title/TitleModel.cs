using SoulRunProject.Framework;
using UnityEngine;

namespace SoulRunProject.Title
{
    /// <summary>
    /// タイトルのロジック処理を行うクラス
    /// </summary>
    public class TitleModel : MonoBehaviour
    {
        public void StartGame()
        {
            DebugClass.Instance.ShowLog("ゲーム開始");
        }
        public void Option()
        {
            DebugClass.Instance.ShowLog("オプション画面表示");
        }
        public void Exit()
        {
            DebugClass.Instance.ShowLog("ゲーム終了");
        }
    }
}
