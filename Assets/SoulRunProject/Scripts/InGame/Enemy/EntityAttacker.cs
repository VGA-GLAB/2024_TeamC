using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Enemyの攻撃処理の抽象クラス
    /// </summary>
    public abstract class EntityAttacker
    {
        [SerializeField, CustomLabel("攻撃力")] protected int _attack;
        [SerializeField, CustomLabel("クールタイム")] protected float _coolTime;
        [SerializeField, CustomLabel("サイズ")] protected float _range;
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
