using Cysharp.Threading.Tasks;
using SoulRunProject.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulRunProject.Title
{
    /// <summary>
    /// タイトルのロジック処理を行うクラス
    /// </summary>
    public class TitleModel : MonoBehaviour
    {
        [SerializeField] float _transitionTime = 1.0f;
        
        public async void StartGame()
        {
            DebugClass.Instance.ShowLog($"ゲーム開始:{_transitionTime}秒後にインゲーム画面に遷移します");
            //ここで実行
            SceneManager.LoadScene("InGame");
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
