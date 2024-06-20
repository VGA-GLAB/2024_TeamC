using System;
using DG.Tweening;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    [Name("ビーム薙ぎ払い")]
    public class BossBeamCleave : BossBehaviorBase
    {
        [SerializeField, CustomLabel("エフェクトプレハブ")] private GameObject _razer;
        [SerializeField, CustomLabel("ビーム原点")] private Transform _beamOrigin;
        [SerializeField, CustomLabel("ボス相対ビーム開始時着弾地点")] private Vector3 _startImpactPosition;
        [SerializeField, CustomLabel("ボス相対ビーム終了時着弾地点")] private Vector3 _finishImpactPosition;
        [Header("性能")]
        [SerializeField, CustomLabel("薙ぎ払い時間")] private float _beamTime;
        [SerializeField, CustomLabel("ダメージ")] private float _damage;
        [SerializeField] private int _hitCount; // 多段ヒットなのか1回だけなのか
        [Header("強化内容"), EnumDrawer(typeof(PowerUpName)), SerializeReference, SubclassSelector]
        PowerUpBehaviorBase[] _powerUpBehaviors;

        private GameObject _laserInstance;
        private int _hitCounter;
        private int _beamSoundIndex;

        public override void Initialize(BossController bossController, Transform playerTf)
        {
            // エフェクトインスタンスの初期化
            _laserInstance = GameObject.Instantiate(_razer, _beamOrigin);
            _laserInstance.SetActive(false);
            
            // 行動パワーアップの代入
            foreach (var powerUpBehavior in _powerUpBehaviors)
            {
                powerUpBehavior.Initialize(this);
                PowerUpBejaviors.Add(powerUpBehavior.PowerUpBehavior);
            }
            
            // sound
            _beamSoundIndex = CriAudioManager.Instance.PlaySE("SE_Laser");
            CriAudioManager.Instance.PauseSE(_beamSoundIndex);
        }
        
        public override void BeginAction()
        {
            _hitCounter = 0;
            _laserInstance.SetActive(true);
            _beamOrigin.rotation = Quaternion.LookRotation(_startImpactPosition);
            CriAudioManager.Instance.ResumeSE(_beamSoundIndex);
        }

        public override void UpdateAction(float deltaTime)
        {
            _beamOrigin.DOLocalRotateQuaternion(Quaternion.LookRotation(_finishImpactPosition), _beamTime)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    // 終了処理
                    _laserInstance.SetActive(false);
                    CriAudioManager.Instance.PauseSE(_beamSoundIndex);
                    OnFinishAction?.Invoke();
                });
            
            // 当たり判定
            if (Physics.Raycast(_beamOrigin.position, _beamOrigin.forward, out RaycastHit hit))
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

        [Serializable, Name("抽象クラス")]
        abstract class PowerUpBehaviorBase
        {
            protected BossBeamCleave _instance;
            
            public void Initialize(BossBeamCleave bossBeamCleave)
            {
                _instance = bossBeamCleave;
            }
            
            public abstract void PowerUpBehavior(BossController controller);
        }
        
        [Serializable, Name("スピードアップ")]
        class SpeedPowerUp : PowerUpBehaviorBase
        {
            [SerializeField, CustomLabel("時間短縮割合")] private float _timeReductionRatio;
            
            public override void PowerUpBehavior(BossController controller)
            {
                _instance._beamTime *= _timeReductionRatio;
            }
        }
    }
}
