using System;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    [Name("ビーム薙ぎ払い")]
    public class BossBeamCleave : BossBehaviorBase
    {
        [SerializeField, CustomLabel("エフェクトプレハブ")] private GameObject _razer;
        [SerializeField, CustomLabel("ビーム原点")] private GameObject _beamOrigin;
        [SerializeField, CustomLabel("ビーム開始時着弾地点")] private Vector3 _startImpactPosition;
        [SerializeField, CustomLabel("ビーム終了時着弾地点")] private Vector3 _finishImpactPosition;
        [Header("性能")]
        [SerializeField, CustomLabel("薙ぎ払い時間")] private float _beamTime;
        [SerializeField, CustomLabel("ダメージ")] private float _damage;
        [SerializeField] private int _hitCount; // 多段ヒットなのか1回だけなのか

        private GameObject _laserInstance;
        private Vector3 _startVector;
        private Vector3 _finishVector;
        private float _timer;
        private int _hitCounter;

        public override void Initialize(BossController bossController)
        {
            // エフェクトインスタンスの初期化
            _laserInstance = GameObject.Instantiate(_razer, _beamOrigin.transform);
            _laserInstance.SetActive(false);
            
            // 角度の初期化
            _startVector = _startImpactPosition - _beamOrigin.transform.position;
            _finishVector = (_finishImpactPosition - _beamOrigin.transform.position);
            
            // 行動パワーアップの代入
            PowerUpBejaviors = new Action<BossController>[] { SpeedPowerUp };
        }
        
        public override void BeginAction()
        {
            _timer = 0;
            _hitCounter = 0;
            _laserInstance.SetActive(true);
            _beamOrigin.transform.rotation = Quaternion.LookRotation(_startVector);
        }

        public override void UpdateAction(float deltaTime)
        {
            // 角度とタイマーの計算
            _timer += deltaTime;

            if (_timer < _beamTime)
            {
                _beamOrigin.transform.rotation = 
                    Quaternion.LookRotation(Vector3.Slerp(_startVector, _finishVector, _timer / _beamTime));
            }
            else
            {
                // 終了処理
                _laserInstance.SetActive(false);
                OnFinishAction?.Invoke();
            }
            
            // 当たり判定
            if (Physics.Raycast(_beamOrigin.transform.position, _beamOrigin.transform.forward, out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out PlayerManager playerManager))
                {
                    if (_hitCounter < _hitCount)
                    {
                        _hitCounter++;
                        playerManager.Damage(_damage);
                    }
                }
            }
        }

        private void SpeedPowerUp(BossController bossController)
        {
            _beamTime /= 2;
        }
    }
}
