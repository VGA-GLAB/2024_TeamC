using System;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Enemyの通常移動処理の実装クラス
    /// </summary>
    [Serializable]
    [Name("通常移動")]
    public class EntityNormalMover : EntityMover
    {
        [SerializeField, CustomLabel("移動方向(ワールド基準)")]
        private Direction _direction;

        [SerializeField, CustomLabel("スポーン位置からデスポーンまでの距離")]
        private float _despawnRange;
        
        private bool _isPaused;
        private Vector3 _spawnPos;
        private PlayerManager _pm;

        public override void OnStart(Transform myTransform = null, PlayerManager pm = null)
        {
            _spawnPos = myTransform.position;
            _pm = pm;
        }

        public override void OnUpdateMove(Transform self, Transform target = default)
        {
            if (_isPaused) return;
            self.position += DirectionToVector3(_direction) * ((_moveSpeed + _pm.CurrentPlayerStatus.MoveSpeed) * Time.deltaTime);

            var dis = _spawnPos - self.position;
            if (Math.Abs(dis.x) <= _despawnRange || Math.Abs(dis.y) <= _despawnRange || Math.Abs(dis.z) <= _despawnRange)
            {
                Despawn();
            }
        }

        private Vector3 DirectionToVector3(Direction direction)
        {
            return direction switch
            {
                Direction.PositiveX => Vector3.right,
                Direction.NegativeX => Vector3.left,
                Direction.PositiveZ => Vector3.forward,
                Direction.NegativeZ => Vector3.back,
                _ => Vector3.zero
            };
        }

        private enum Direction
        {
            [InspectorName("x正方向")] PositiveX,
            [InspectorName("x負方向")] NegativeX,
            [InspectorName("z正方向")] PositiveZ,
            [InspectorName("z負方向")] NegativeZ
        }
    }
}