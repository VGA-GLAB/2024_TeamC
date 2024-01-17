using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// 敵が持つ攻撃ルーチンのインターフェース
    /// </summary>
    public abstract class EnemyAttackRoutineBase : MonoBehaviour
    {
        [SerializeField] protected float _atk = 1;

        /// <summary>
        /// 敵の攻撃の中身を書く
        /// 通常は一度のみ呼ばれる
        /// </summary>
        public abstract void ActivateAttackRoutine();

        /// <summary>
        /// 攻撃ルーチンの非アクティブ処理の中身を書く
        /// </summary>
        public abstract void DeactivateAttackRoutine();
    }
}
