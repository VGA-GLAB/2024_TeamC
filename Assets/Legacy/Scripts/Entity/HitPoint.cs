using System;
using UniRx;
using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// ヒットポイントを管理するクラス
    /// 自身のダメージ属性を持つ
    /// </summary>
    public class HitPoint
    {
        private readonly IntReactiveProperty _hp = new IntReactiveProperty();
        private int _dfe;
        private ObjectDamageType _damageType;
        private BoolReactiveProperty _isDeath = new BoolReactiveProperty(false);
        public IReadOnlyReactiveProperty<bool> IsDeath => _isDeath;
        public IObservable<int> HPChanged => _hp;
        public int HP => _hp.Value;

        /// <summary> 防御力 </summary>
        public float DFE => _dfe;
        /// <summary> このインスタンスを保有するオブジェクトのタイプ </summary>
        public ObjectDamageType DamageType => _damageType;

        public HitPoint(ObjectDamageType damageType, int hp = 1, int dfe = 0)
        {
            if (_hp.Value == 0) _hp.Value = 1;
            _hp.Value = hp;
            _dfe = dfe;
            _damageType = damageType;
        }

        /// <summary>
        /// ダメージを適用する
        /// </summary>
        /// <param name="damage"></param>
        public void ApplyDamage(int damage)
        {
            _hp.Value -= Mathf.Max(damage - _dfe, 0);
            if (_hp.Value <= 0)
            {
                _hp.Value = 0;
                _isDeath.Value = true;
            }
        }

    }
    /// <summary>
    /// 自身のタイプを決める
    /// PlayerはPlayer
    /// EnemyはEnemy
    /// </summary>
    [Flags]
    public enum ObjectDamageType
    {
        None = 0,
        Enemy = 1,
        Obstacle = 1 << 1,
        Player = 1 << 3,

        Hostile = Enemy | Obstacle, //敵性オブジェクト
    }
}
