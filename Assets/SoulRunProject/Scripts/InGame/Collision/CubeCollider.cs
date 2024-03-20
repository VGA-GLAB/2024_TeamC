using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 直方体衝突情報クラス
    /// </summary>
    public class CubeCollider : ColliderBase
    {
        [SerializeField] Vector3 _center;
        [SerializeField] Vector3 _size;
        public Vector3 Center => transform.position + _center;
        public Vector3 Size => _size;
        public float Right => transform.position.x + _center.x + _size.x / 2;
        public float Left => transform.position.x + _center.x - _size.x / 2;
        public float Up => transform.position.y + _center.y + _size.y / 2;
        public float Down => transform.position.y + _center.y - _size.y / 2;
        public float Forward => transform.position.z + _center.z + _size.z / 2;
        public float Back => transform.position.z + _center.z - _size.z / 2;
        
        public override bool CheckContacts(ColliderBase other)
        {
            return other switch
            {
                BallCollider sphere => CollisionCalculation.CheckContacts(sphere, this),
                CubeCollider box => CollisionCalculation.CheckContacts(this, box),
                _ => false
            };
        }
        /// <summary>
        /// 指定したポイントに一番近い面の点の座標を返す。
        /// 参考 https://gdbooks.gitbooks.io/3dcollisions/content/Chapter1/closest_point_aabb.html
        /// </summary>
        public Vector3 ClosestPoint(Vector3 point)
        {
             Vector3[] closestPoints = new Vector3[6];
             int closestPointIndex = 0;
             closestPoints[0] = new Vector3(Mathf.Clamp(point.x, Left, Right)
                 , Up, Mathf.Clamp(point.z, Back, Forward));
             closestPoints[1] = new Vector3(Mathf.Clamp(point.x, Left, Right)
                 , Down, Mathf.Clamp(point.z, Back, Forward));
             closestPoints[2] = new Vector3(Right, Mathf.Clamp(point.y, Down, Up), 
                 Mathf.Clamp(point.z, Back, Forward));
             closestPoints[3] = new Vector3(Left, Mathf.Clamp(point.y, Down, Up),
                 Mathf.Clamp(point.z, Back, Forward));
             closestPoints[4] = new Vector3(Mathf.Clamp(point.x, Left, Right),
                 Mathf.Clamp(point.y, Down, Up), Forward);
             closestPoints[5] = new Vector3(Mathf.Clamp(point.x, Left, Right),
                 Mathf.Clamp(point.y, Down, Up), Back);
             
            float minDistance = float.MaxValue;
            for (int i = 0; i < 6; i++)
            {
                var temp = (closestPoints[i] - point).sqrMagnitude;
                if (temp < minDistance)
                {
                    minDistance = temp;
                    closestPointIndex = i;
                }
            }
            return closestPoints[closestPointIndex];
        }
        /// <summary>
        /// 指定したポイントがボックスの内側かどうかを返す。
        /// </summary>
        public bool CheckInside(Vector3 point)
        {
            var x = Right > point.x && Left < point.x;
            var y = Up > point.y && Down < point.y;
            var z = Forward > point.z && Back < point.z;
            return x && y && z;
        }
        #if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + _center, _size);
        }
        #endif
    }
}