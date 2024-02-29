#if  UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRunProject.Common
{
    public class DebugClass : AbstractSingleton<DebugClass>
    {
        [Header("設定値")]
        [SerializeField, Header("デバックモードを付けるか")] private bool _isDebugMode;
        [SerializeField, Header("FPSを表示するか")] private bool _isShowFPS;
        [SerializeField, Header("設定フレームレート")] private int _targetFrameRate;
        [SerializeField, Header("FPS警告表示の値")] private float _fpsThreshold1 = 30f; // この値を下回ると黄色
        [SerializeField, Header("FPSエラー表示の値")] private float _fpsThreshold2 = 15f; // この値を下回ると赤色
        [SerializeField, Header("VsycnをOFFにするか")] private bool _isVsyncOff;
        [SerializeField, Header("ログの表示時間")] private float _displayTime = 5f; // テキストが表示される時間（秒）
        [SerializeField, Header("ログの最大表示行数")] private int _maxLines = 5; // 表示する最大行数
        [Header("参照用")]
        [SerializeField] private Text _fpsText;
        [SerializeField] private Text timerText; // 時間を表示するUI Textへの参照
        [SerializeField] private Text _logText; // UI Text コンポーネントへの参照
        private float _startTime;
        private float _deltaTime;
        private Queue<string> _textLines = new Queue<string>(); // 表示するテキストの行を保持するキュー

        private void Awake()
        {
            if (!_isDebugMode)
            {
                Destroy(this.gameObject);
                return;
            }

            QualitySettings.vSyncCount = _isVsyncOff ?  0 : 1;
            Application.targetFrameRate = _targetFrameRate;
            _startTime = Time.time;
        }

        private void Update()
        {
            if (!_isDebugMode) return;
            ShowFPS();
            ShowTime();
        }

        private void ShowFPS()
        {
            if (!_isShowFPS || _fpsText == null) return;

            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
            float fps = 1.0f / _deltaTime;

            // FPSに応じた色の選択
            string color = "green"; // デフォルトは白色
            if (fps < _fpsThreshold2)
            {
                color = "red";
            }
            else if (fps < _fpsThreshold1)
            {
                color = "yellow";
            }

            // リッチテキストを使用して色を適用
            _fpsText.text = $"<color={color}>FPS: {fps:0.}</color>";
        }

        #region タイマー
        private void ShowTime()
        {
            if (timerText == null) return;
            float t = Time.time - _startTime; // 開始からの経過時間を計算

            int hours = (int)t / 3600;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            timerText.text = string.Format("PlayTime: {0:D2}:{1:D2}:{2:00.00}", hours, minutes, seconds);
        }
        #endregion
        
        #region ログ
        private void AddText(string text, Color logColor)
        {
            // 色情報をリッチテキスト形式でテキストに追加
            string coloredText = $"<color=#{ColorUtility.ToHtmlStringRGB(logColor)}>{text}</color>";

            if (_textLines.Count >= _maxLines)
            {
                _textLines.Dequeue(); // 最大行数に達したら、一番古い行を削除
            }

            _textLines.Enqueue(coloredText); // 新しいテキスト行を追加
            UpdateTextDisplay();
            StartCoroutine(RemoveTextAfterTime(_displayTime)); // 指定時間後にテキストを削除するコルーチンを開始
        }
        
        // テキスト表示を更新するメソッド
        private void UpdateTextDisplay()
        {
            if (_logText ==  null) return;
            _logText.text = string.Join("\n", _textLines.ToArray()); // キュー内の全テキスト行を改行で結合して表示
        }

        // 指定時間後にテキストを削除するコルーチン
        private IEnumerator RemoveTextAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            if (_textLines.Count > 0)
            {
                _textLines.Dequeue(); // 一番古いテキスト行を削除
                UpdateTextDisplay();
            }
        }
        public void ShowErrorLog(string message)
        {
            AddText(message, Color.red);
        }
        public void ShowWarningLog(string message)
        {
            AddText(message, Color.yellow);
        }
        public void ShowLog(string message)
        {
            AddText(message, Color.green); // または、デフォルトのテキスト色を使いたい場合はこの引数を省略してもよい
        }
        #endregion
    }
}
#endif