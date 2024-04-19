using System;
using UnityEngine;
using SoulRunProject.SoulMixScene;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Enemyの通常攻撃処理の実装クラス
    /// </summary>
    [Serializable]
    public class EntityNormalAttacker : EntityAttacker
    {

        /// <summary>
        /// 攻撃処理メソッド(仮)
        /// </summary>
        public void OnStart()
        {
            Debug.Log($"Attack! | atk = {_attack} | ct = {_coolTime} | rg = {_range}");
        }

        public void OnUpdateAttack()
        {
            
        }

        public void Pause()
        {
            
        }

        public void Resume()
        {
            
        }
    }
}