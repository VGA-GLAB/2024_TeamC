using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// 敵が持つ移動ルーチンのインターフェース
    /// </summary>
    public abstract class EnemyMoveRoutineBase : MonoBehaviour
    {
        [SerializeField] protected float _speed = 1;

        /// <summary>
        /// 敵の移動ルーチンの起動処理
        /// </summary>
        public abstract void ActivateMoveRoutine();

        /// <summary>
        /// 敵の移動ルーチンの非アクティブ処理
        /// </summary>
        public abstract void DeactivateMoveRoutine();
    }
}
