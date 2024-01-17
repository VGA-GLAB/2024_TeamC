using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRun.InGame
{
    /// <summary>
    /// インゲームでのバッグ用のクラス
    /// </summary>
    public class InGameDebugUtility : MonoBehaviour
    {
        [Tooltip("FPSの表示")]
        [SerializeField] private bool showFPS = false;
        private float _prevTime = 0f;
        private int _frameCount = 0;
        [Tooltip("FPSを表示するText")]
        [SerializeField] private Text fpsUI;

        // Update is called once per frame
        void Update()
        {
            ShowDebugInfo();
        }


        private void ShowDebugInfo()
        {
            if (showFPS)
            {
                _frameCount++;
                float timeElapsed = Time.realtimeSinceStartup - _prevTime;

                if (timeElapsed >= 0.5f)
                {
                    float fps = (_frameCount / timeElapsed);
                    fpsUI.text = $"FPS: {fps}";
                    _frameCount = 0;
                    _prevTime = Time.realtimeSinceStartup;
                }
            }
        }
    }
}
