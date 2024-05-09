using SoulRunProject.SoulMixScene;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤーのステータスを管理
    /// </summary>
    public class PlayerStatusManager
    {
        private readonly Status _status;

        public Status CurrentStatus => _status;
        public FloatReactiveProperty CurrentHp { get; } = new FloatReactiveProperty();
        public bool CanHeal => CurrentHp.Value < _status.Hp;

        public PlayerStatusManager(Status baseStatus)
        {
            _status = new Status(baseStatus);
            CurrentHp.Value = _status.Hp;
        }

        /// <param name="damage">計算前ダメージ</param>
        /// <param name="criticalRate">クリティカル率</param>
        /// <param name="criticalDamageRate">クリティカルダメージ倍率</param>
        /// <returns>IsDead</returns>
        public bool Damage(float damage)
        {
            CurrentHp.Value -= Calculator.CalcDamage(damage, _status.Defence, 0, 1);
            return CurrentHp.Value <= 0;
        }

        public void Heal(float healValue)
        {
            CurrentHp.Value += healValue;
            CurrentHp.Value = Mathf.Min(CurrentHp.Value, CurrentStatus.Hp);
        }

        public void IncreaseBaseHp(float increaseValue)
        {
            _status.Hp += increaseValue;
            CurrentHp.Value += increaseValue;
        }
    }
}
