using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Common
{
    public static class PauseManager
    {
        private static List<IPausable> _pausables = new();
        private static bool _isPause;

        public static void RegisterPausableObject(IPausable pausable)
        {
            _pausables.Add(pausable);
        }
        public static void UnRegisterPausableObject(IPausable pausable)
        {
            _pausables.Remove(pausable);
        }

        public static void Pause(bool isPause)
        {
            // foreach (var pausable in _pausables)
            // {
            //     pausable.Pause(isPause);
            // }

            if (isPause)
            {
                CriAudioManager.Instance.PauseAll();
                Time.timeScale = 0;
            }
            else
            {
                CriAudioManager.Instance.ResumeAll();
                Time.timeScale = 1;
            }

            _isPause = isPause;
        }
        /// <summary>
        /// ポーズに対応した待機処理、キャンセルされるとfalseを返す
        /// </summary>
        /// <returns>キャンセルされるとfalse, 設定した時間を超えるとtrue</returns>
        public static async UniTask<bool> TryWaitForSeconds(float seconds, CancellationToken ct)
        {
            var timer = 0f;
            //  タイマーループ開始
            while (true)
                try
                {
                    //  Pauseフラグがfalseなら1フレーム待つ(ちゃんと1フレーム待ってるか分からない)、
                    //  trueならfalseになるまで通さない
                    await UniTask.WaitUntil(() => !_isPause, PlayerLoopTiming.Update, ct);
                    timer += Time.deltaTime;
                    if (timer >= seconds)
                    {
                        return true;
                    }
                }
                catch
                {
                    Debug.Log("キャンセルされた");
                    return false;
                }
        }
    }
}