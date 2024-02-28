#if  UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRunProject.Common
{
    public class DebugClass : MonoBehaviour
    {
        [Header("設定値")]
        [SerializeField, Header("デバックモードを付けるか")] private bool _isDebugMode;
        [SerializeField, Header("FPSを表示するか")] private bool _isShowFPS;
        [SerializeField, Header("目標フレームレート")] private int _targetFrameRate;
        [SerializeField, Header("VsycnをOFFにするか")] private bool _isVsyncOff;
        private float _deltaTime;
        [Header("参照用")]
        [SerializeField] private Text _fpsText;

        private void Awake()
        {
            if (!_isDebugMode)
            {
                Destroy(this.gameObject);
                return;
            }

            if (_isVsyncOff)
            {
                QualitySettings.vSyncCount = 0;
            }
            else
            {
                QualitySettings.vSyncCount = 1;
            }
            
            Application.targetFrameRate = _targetFrameRate;
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if (!_isDebugMode) return;
            ShowFPS();
        }

        private void ShowFPS()
        {
            if (!_isShowFPS) return;
            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
            float fps = 1.0f / _deltaTime;
            _fpsText.text = $"FPS: {fps:0.}";
        }
        
        public void ShowLog(string message)
        {
            Debug.Log(message);
        }
        
        public void ShowWarningLog(string message)
        {
            Debug.LogWarning(message);
        }
        
        public void ShowErrorLog(string message)
        {
            Debug.LogError(message);
        }
    }
}
#endif