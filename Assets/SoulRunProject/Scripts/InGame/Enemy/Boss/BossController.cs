using System;
using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ボスの行動を管理するクラス
    /// 行動様式
    /// 最初に入場ムーブ
    /// 一行動を起こすたびに次の行動からランダムで実行する、
    /// </summary>
    public class BossController : MonoBehaviour
    {
        [SerializeField, CustomLabel("初期ワールド座標")] private Vector3 _initialPosition;
        [SerializeField, Tooltip("パワーアップする閾値(%)")] private float[] _powerUpThreshold; 
        [Header("ボスの行動"), CustomLabel("行動の種類"), SerializeReference, SubclassSelector] List<IBossBehavior> _bossBehaviors;
        [SerializeField, CustomLabel("行動待機時間")] private float _behaviorIntervalTime;

        private BossState _currentState = BossState.Animation;
        private int _thresholdIndex;
        private float _intervalTimer;
        /// <summary> 動いている行動 </summary>
        private IBossBehavior _inActionBehavior;

        private void Start()
        {
            transform.position = _initialPosition;
            DamageableEntity bossDamageable = GetComponent<DamageableEntity>();
            bossDamageable.CurrentHp
                .Where(_ => _powerUpThreshold.Length > _thresholdIndex)
                .Subscribe(hp =>
                {
                    while (_powerUpThreshold.Length > _thresholdIndex)
                    {
                        if (bossDamageable.MaxHp * _powerUpThreshold[_thresholdIndex] / 100 >= hp)
                        {
                            foreach (var bossBehavior in _bossBehaviors)
                            {
                                ((BossBehaviorBase)bossBehavior).PowerUpBejaviors[_thresholdIndex]?.Invoke(this);
                            }
                            
                            _thresholdIndex++;
                        }
                        else
                        {
                            break;
                        }
                    }
                })
                .AddTo(this);

            foreach (var behavior in _bossBehaviors)
            {
                behavior.Initialize(this);
                ((BossBehaviorBase)behavior).OnFinishAction += () =>
                {
                    _currentState = BossState.Standby;
                    _intervalTimer = 0;
                };
            }

            _currentState = BossState.Standby; // todo 入場アニメーション
        }
        
        private void Update()
        {
            switch (_currentState)
            {
                case BossState.Animation:
                    break;
                
                case BossState.Standby:
                    _intervalTimer += Time.deltaTime;
                
                    if (_intervalTimer >= _behaviorIntervalTime)
                    {
                        _currentState = BossState.InAction;
                        _inActionBehavior = _bossBehaviors.Where(x => x != _inActionBehavior)
                            .ToList()[Random.Range(0, _bossBehaviors.Count - 1)]; // 直前をのぞいた一つをランダムに選択
                        _inActionBehavior.BeginAction();
                        _inActionBehavior.UpdateAction(Time.deltaTime);
                    }
                    break;
                
                case BossState.InAction:
                    _inActionBehavior.UpdateAction(Time.deltaTime);
                    break;
            }
        }

        private enum BossState
        {
            Animation, // 登場時などのAnimation中
            Standby, // 行動待機中
            InAction // IBossBehaviorのAction中
        }
    }
    
    /// <summary>
    /// ボスの行動が持つインターフェイス
    /// </summary>
    public interface IBossBehavior
    {
        /// <summary> Script初期化処理 </summary>
        public void Initialize(BossController bossController);
        /// <summary> Action開始 </summary>
        public void BeginAction();
        /// <summary> Action中Update </summary>
        public void UpdateAction(float deltaTime);
    }

    /// <summary>
    /// Boss行動の基底クラス
    /// </summary>
    /// SubclassSelectorを使いたいけどフィールドも欲しい
    [Name("抽象クラス")]
    public abstract class BossBehaviorBase : IBossBehavior
    {
        /// <summary> Action終了時に呼ばれる </summary>
        public Action OnFinishAction;
        public Action<BossController>[] PowerUpBejaviors; 

        public abstract void Initialize(BossController bossController);
        public abstract void BeginAction();
        public abstract void UpdateAction(float deltaTime);
    }
}
