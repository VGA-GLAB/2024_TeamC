using System;
using UnityEngine;
using UniRx;

namespace SoulRunProject
{
    public class ConcentratedLineController : MonoBehaviour
    {
        [SerializeField] private Material _concentratedLineMaterial;
        [SerializeField] private float _maxSpeed = 10f; // スピードの最大値
        [SerializeField] private float _minDensity = 10f; // 密度の最小値
        [SerializeField] private float _maxDensity = 40f; // 密度の最大値


        public float MaxSpeed => _maxSpeed;
        private float _currentSpeed;
        private static readonly int Density = Shader.PropertyToID("_Density");
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private static readonly int Color1 = Shader.PropertyToID("_Color");
        private static readonly int IsGamingColor = Shader.PropertyToID("_IsGamingColor");
        private static readonly int RollSpeed = Shader.PropertyToID("_RollSpeed");
        private static readonly int InnerSpace = Shader.PropertyToID("_InnerSpace");
        private static readonly int Taper = Shader.PropertyToID("_Taper");
        private static readonly int OuterEdge = Shader.PropertyToID("_OuterEdge");
        private static readonly int Offset = Shader.PropertyToID("_Offset");
        private static readonly int ThresholdL = Shader.PropertyToID("_ThresholdL");
        private static readonly int ThresholdH = Shader.PropertyToID("_ThresholdH");

        // スピードによって集中線の強さを変える
        private void Start()
        {
            // スピードの変化を監視
            Observable.EveryUpdate()
                .Subscribe(_ => UpdateConcentratedLine())
                .AddTo(this);
        }

        private void UpdateConcentratedLine()
        {
            if (!_concentratedLineMaterial) return;
            // スピードに基づいて密度を計算
            float normalizedSpeed = Mathf.InverseLerp(0, _maxSpeed, _currentSpeed);
            float density = Mathf.Lerp(_minDensity, _maxDensity, normalizedSpeed);

            // シェーダープロパティを更新
            _concentratedLineMaterial.SetFloat(Density, density);
        }

        // スピードを設定するメソッド
        public void SetSpeed(float speed)
        {
            _currentSpeed = Mathf.Clamp(speed, 0, _maxSpeed);
        }

        // その他のシェーダープロパティを設定するメソッド
        public void SetMainTex(Texture texture)
        {
            _concentratedLineMaterial.SetTexture(MainTex, texture);
        }

        public void SetColor(Color color)
        {
            _concentratedLineMaterial.SetColor(Color1, color);
        }

        public void SetIsGamingColor(bool isGamingColor)
        {
            _concentratedLineMaterial.SetFloat(IsGamingColor, isGamingColor ? 1.0f : 0.0f);
        }

        public void SetRollSpeed(float rollSpeed)
        {
            _concentratedLineMaterial.SetFloat(RollSpeed, rollSpeed);
        }

        public void SetInnerSpace(float innerSpace)
        {
            _concentratedLineMaterial.SetFloat(InnerSpace, innerSpace);
        }

        public void SetTaper(float taper)
        {
            _concentratedLineMaterial.SetFloat(Taper, taper);
        }

        public void SetOuterEdge(float outerEdge)
        {
            _concentratedLineMaterial.SetFloat(OuterEdge, outerEdge);
        }

        public void SetOffset(float offset)
        {
            _concentratedLineMaterial.SetFloat(Offset, offset);
        }

        public void SetThresholdL(float thresholdL)
        {
            _concentratedLineMaterial.SetFloat(ThresholdL, thresholdL);
        }

        public void SetThresholdH(float thresholdH)
        {
            _concentratedLineMaterial.SetFloat(ThresholdH, thresholdH);
        }
    }
}