using SoulRunProject.Common;
using UnityEngine;
using System;

namespace SoulRunProject.InGame
{
    [Name("炎の床生成")]
    public class BossFlameFloor : BossBehaviorBase
    {
        [SerializeField, CustomLabel("X軸移動範囲")] private float _moveRangeX;
        [SerializeField, CustomLabel("Y軸移動範囲")] private float _moveRangeY;
        [SerializeField, CustomLabel("発射位置")] private Transform _firingPosition;
        [SerializeField, CustomLabel("火炎弾プレハブ")] private BossFlameBallController _flameBallPrefab;
        [Header("性能")]
        [SerializeField, CustomLabel("行動時間 ∞字一周の時間")] private float _actionTime;
        [SerializeField, CustomLabel("火炎弾スピード")] private float _flameBallSpeed;
        [SerializeField, CustomLabel("一回の行動の火炎弾の数")] private float _flameBallNum;
        [SerializeField, CustomLabel("延焼ダメージ(秒)")] private float _flameDamage;
        [SerializeField, CustomLabel("着弾地点のプレイヤーとの距離"), Tooltip("ブレス発射時のプレイヤーの位置からの着弾地点の距離")]
        private float _deviationDistance;
        [SerializeField, CustomLabel("着弾地点の最大のずれ幅")] private float _flameBallAccuracy;
        [Header("強化内容"), EnumDrawer(typeof(PowerUpName)), SerializeReference, SubclassSelector]
        PowerUpBehaviorBase[] _powerUpBehaviors;

        // 移動
        private float _actionTimer;
        private Transform _bossTransform;
        private Vector3 _defaultBossPos;
        private Transform _playerTransform;
        // flame
        private float _flameBallTimer;
        
        public override void Initialize(BossController bossController)
        {
            _bossTransform = bossController.transform;
            _defaultBossPos = _bossTransform.position;
            _playerTransform = GameObject.FindObjectOfType<PlayerManager>().transform;
            
            // 行動パワーアップの代入
            foreach (var powerUpBehavior in _powerUpBehaviors)
            {
                powerUpBehavior.Initialize(this);
                PowerUpBejaviors.Add(powerUpBehavior.PowerUpBehavior);
            }
        }

        public override void BeginAction()
        {
            _actionTimer = 0;
        }

        public override void UpdateAction(float deltaTime)
        {
            // flame
            _flameBallTimer += deltaTime;

            if (_flameBallTimer >= _actionTime / _flameBallNum)
            {
                _flameBallTimer = 0;
                
                // flame ball 生成と向きの調整
                BossFlameBallController flameBall =
                    GameObject.Instantiate(_flameBallPrefab, _firingPosition.position, Quaternion.identity);
                Vector3 impactPos = _playerTransform.position;
                impactPos.x += UnityEngine.Random.Range(-_flameBallAccuracy, _flameBallAccuracy);
                impactPos.y = 0; // fieldの高さ
                impactPos.z += _deviationDistance;
                flameBall.transform.LookAt(impactPos);
                flameBall.Initialize(_flameBallSpeed, _flameDamage);
            }
            
            // 時間と移動
            _actionTimer += deltaTime;
            
            if (_actionTimer < _actionTime)
            {
                Vector3 newPos = _defaultBossPos;
                newPos.x = _defaultBossPos.x + _moveRangeX * Mathf.Sin(_actionTimer / _actionTime * Mathf.PI * 2) / 2;
                newPos.y = _defaultBossPos.y + _moveRangeY * -Mathf.Sin(_actionTimer / _actionTime * Mathf.PI * 4) / 2;
                _bossTransform.position = newPos;
            }
            else
            {
                // 終了処理
                _bossTransform.position = _defaultBossPos;
                OnFinishAction?.Invoke();
            }
        }

        [Serializable, Name("抽象クラス")]
        abstract class PowerUpBehaviorBase
        {
            protected BossFlameFloor _instance;
            
            public void Initialize(BossFlameFloor bossFlameFloor)
            {
                _instance = bossFlameFloor;
            }

            public abstract void PowerUpBehavior(BossController controller);
        }

        [Serializable, Name("スピードアップ")]
        class SpeedPowerUp : PowerUpBehaviorBase
        {
            [SerializeField, CustomLabel("時間短縮割合")] private float _timeReductionRatio;
            [SerializeField, CustomLabel("火炎弾のスピード上昇割合")] private float _speedUpRatio;
            
            public override void PowerUpBehavior(BossController controller)
            {
                _instance._actionTime *= _timeReductionRatio;
                _instance._flameBallSpeed *= _speedUpRatio;
            }
        }
    }
}
