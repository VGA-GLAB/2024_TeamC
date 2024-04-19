using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Enemyの攻撃処理のインターフェース
    /// </summary>
    public abstract class EntityAttacker
    {
        [SerializeField] protected int _attack;
        [SerializeField] protected float _coolTime;
        [SerializeField] protected float _range;
        /// <summary>
        /// 起動時に一度のみ呼ばれる
        /// </summary>
        public virtual void OnStart(){}
        /// <summary>
        /// Updateで呼ばれる攻撃処理
        /// </summary>
        public virtual void OnUpdateAttack(Transform myTransform , Transform playerTransform){}
        public virtual void Pause(){}
        public virtual void Resume(){}
    }
}
