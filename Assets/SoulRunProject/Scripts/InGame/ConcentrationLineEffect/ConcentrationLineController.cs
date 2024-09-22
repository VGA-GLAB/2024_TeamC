using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunProject.InGame
{
    public class ConcentrationLineController : MonoBehaviour
    {
        [SerializeField] private Material _concentrationLineMaterial; // ShaderGraphで使用するマテリアル
        [SerializeField] private PlayerMovement _playerMovement; // プレイヤーの移動スクリプト

        private float _noiseBlend;
        private float _speed;
        private const float MinNoiseBlend = 0.0f;
        private const float MaxNoiseBlend = 0.5f;
        private const float MinSpeed = 10f;
        private const float MaxSpeed = 20f;

        private const float MinMoveSpeed = 0f;
        private const float MaxMoveSpeed = 20f;

        void Update()
        {
            float currentSpeed = _playerMovement.MoveSpeed; // プレイヤーの移動速度を取得

            // 移動速度に応じて集中線の強さを調整
            float t = Mathf.InverseLerp(MinMoveSpeed, MaxMoveSpeed, currentSpeed);

            // NoiseBlendとSpeedを制御
            _noiseBlend = Mathf.Lerp(MinNoiseBlend, MaxNoiseBlend, t);
            _speed = Mathf.Lerp(MinSpeed, MaxSpeed, t);

            // マテリアルのプロパティに値をセット
            _concentrationLineMaterial.SetFloat("_NoiseBlend", _noiseBlend);
            _concentrationLineMaterial.SetFloat("_Speed", _speed);

            // 他のパラメータも同様に設定
            _concentrationLineMaterial.SetFloat("_SmoothEdge1", 0.5f);
            _concentrationLineMaterial.SetFloat("_SmoothEdge2", 0.5f);
            _concentrationLineMaterial.SetFloat("_AngleScale", 3.62f);
            _concentrationLineMaterial.SetFloat("_Randomness", 17f);
        }
    }
}