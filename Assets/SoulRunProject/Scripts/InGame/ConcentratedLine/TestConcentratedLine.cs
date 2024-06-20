using System;
using UnityEngine;

namespace SoulRunProject
{
    public class TestConcentratedLine : MonoBehaviour
    {
        [SerializeField] private ConcentratedLineController _lineController;
        [SerializeField] private float _speedChangeRate = 1f;
        [SerializeField] private Texture _testTexture;
        [SerializeField] private Color _testColor = Color.white;
        [SerializeField] private bool _isGamingColor = false;
        [SerializeField] private float _testRollSpeed = 1f;
        [SerializeField] private float _testInnerSpace = 0f;
        [SerializeField] private float _testTaper = 0f;
        [SerializeField] private float _testOuterEdge = 0.1f;
        [SerializeField] private float _testOffset = 0.5f;
        [SerializeField] private float _testThresholdL = 0.04f;
        [SerializeField] private float _testThresholdH = 0.07f;
        private float _currentSpeed = 0f;

        private void Start()
        {
            _lineController.SetMainTex(_testTexture);
            _lineController.SetColor(_testColor);
            _lineController.SetIsGamingColor(_isGamingColor);
            _lineController.SetRollSpeed(_testRollSpeed);
            _lineController.SetInnerSpace(_testInnerSpace);
            _lineController.SetTaper(_testTaper);
            _lineController.SetOuterEdge(_testOuterEdge);
            _lineController.SetOffset(_testOffset);
            _lineController.SetThresholdL(_testThresholdL);
            _lineController.SetThresholdH(_testThresholdH);
        }

        void Update()
        {
            // プレイヤーの入力に基づいてスピードを変更する
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _currentSpeed += _speedChangeRate * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                _currentSpeed -= _speedChangeRate * Time.deltaTime;
            }

            // スピードを0から最大値の範囲内に制限する
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, _lineController.MaxSpeed);

            // ConcentratedLineControllerにスピードを設定する
            _lineController.SetSpeed(_currentSpeed);

            // 現在のスピードをデバッグログに表示
            Debug.Log("Current Speed: " + _currentSpeed);
        }
    }
}