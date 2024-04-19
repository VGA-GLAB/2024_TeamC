using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Enemyの移動処理のインターフェース
    /// </summary>
    public abstract class EntityMover
    {
        [SerializeField] protected float _moveSpeed;
        public virtual void OnStart(){}
        public virtual void OnUpdateMove(Transform myTransform , Transform playerTransform){}
        public virtual void Pause(){}
        public virtual void Resume(){}
    }
}
