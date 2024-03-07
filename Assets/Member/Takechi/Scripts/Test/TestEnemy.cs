using SoulRunProject.Common;
using SoulRunProject.InGame;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.TakechiTest
{
    /// <summary>
    /// デバッグ用エネミークラス
    /// </summary>
    public class TestEnemy : MonoBehaviour
    {
        [SerializeField] Status _status;
        [SerializeReference, SubclassSelector] IEntityMover _mover;
        Rigidbody _rb;
        public IEntityMover Mover => _mover;
        void Start()
        {
            _mover.GetMoveStatus(_status);
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            _mover.Move(transform, _rb);
        }
    }
}