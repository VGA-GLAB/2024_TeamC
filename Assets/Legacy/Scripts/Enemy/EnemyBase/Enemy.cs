using System;
using UnityEngine;
using UniRx;

namespace SoulRun.InGame
{
    /// <summary>
    /// ゲーム中に現れる敵の基底クラス
    /// 攻撃ルーチン、移動ルーチンとステータスを持つ
    /// </summary>
    public class Enemy : MonoBehaviour, IDamage, IPausable
    {
        [SerializeField] private string _name = "";
        private int _score = 100;
        private int _exp = 1;
        private HitPoint _hitPoint;
        private EnemyAttackRoutineBase[] _attackRoutine;
        private EnemyMoveRoutineBase[] _moveRoutine;
        public event Action OnReturnMethod;
        public event Action OnDeathMethod;
        
        public string Name => _name;
        public int Score => _score;
        public int Exp => _exp;
        public HitPoint HitPoint => _hitPoint;

        private void Awake()
        {
            LoadFieldValue();
        }

        /// <summary>
        /// 変数の格納と初期化を行う
        /// </summary>
        private void LoadFieldValue()
        {
            _hitPoint = new HitPoint(ObjectDamageType.Enemy, 1);
            _attackRoutine = GetComponents<EnemyAttackRoutineBase>();
            _moveRoutine = GetComponents<EnemyMoveRoutineBase>();

            _hitPoint.IsDeath.Where(x => x == true).Subscribe(_ =>
            {   //死亡時の処理
                OnReturnMethod?.Invoke();
                OnDeathMethod?.Invoke();
            }).AddTo(this);
        }

        public void Active()
        {
            foreach (var attackRoutine in _attackRoutine)
            {
                attackRoutine?.ActivateAttackRoutine();
            }
            foreach (var moveRoutine in _moveRoutine)
            {
                moveRoutine?.ActivateMoveRoutine();
            }
        }

        public void Pause()
        {
            foreach (var attackRoutine in _attackRoutine)
            {
                attackRoutine?.DeactivateAttackRoutine();
            }
            foreach (var moveRoutine in _moveRoutine)
            {
                moveRoutine?.DeactivateMoveRoutine();
            }
        }
    }
}
