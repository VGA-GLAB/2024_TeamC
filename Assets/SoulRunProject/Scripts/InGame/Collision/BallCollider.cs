using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 球体衝突情報クラス
    /// </summary>
    public class BallCollider : ColliderBase
    {
        [SerializeField] Vector3 _center;
        [SerializeField] float _radius;
        public Vector3 Center => transform.position + _center;
        public float Radius => _radius;
        public override bool CheckContacts(ColliderBase other)
        {
            return other switch
            {
                BallCollider sphere => CollisionCalculation.CheckContacts(this, sphere),
                CubeCollider box => CollisionCalculation.CheckContacts(this, box),
                _ => false
            };
        }
        #if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + _center, _radius);
        }
        #endif
    }
}