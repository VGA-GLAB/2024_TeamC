using System;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Enemyの移動処理の抽象クラス
    /// </summary>
    public abstract class EntityMover
    {
        [SerializeField, CustomLabel("移動速度")] protected float _moveSpeed;
        public virtual void OnStart(Transform myTransform = null, PlayerManager pm = null){}
        public virtual void OnUpdateMove(Transform myTransform, Transform playerTransform){}
        public Action Despawn;
    }
}
