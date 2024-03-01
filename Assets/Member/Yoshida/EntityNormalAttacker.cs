using UnityEngine;
using SoulRunProject.SoulMixScene;

namespace SoulRun.InGame.Enemy
{
    /// <summary>
    /// Enemyの通常攻撃処理の実装クラス
    /// </summary>
    public class EntityNormalAttacker : IEntityAttacker
    {
        int _attack;
        float _coolTime;
        float _range;
        
        /// <summary>
        /// ステータス入手メソッド
        /// </summary>
        /// <param name="status">ステータスのScriptableObject</param>
        public void GetAttackStatus(Status status)
        {
            _attack = status.Attack;
            _coolTime = status.CoolTime;
            _range = status.Range;
        }

        /// <summary>
        /// 攻撃処理メソッド(仮)
        /// </summary>
        public void Attack()
        {
            Debug.Log($"Attack! | atk = {_attack} | ct = {_coolTime} | rg = {_range}");
        }
    }
}