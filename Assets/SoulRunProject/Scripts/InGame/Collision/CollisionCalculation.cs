using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// 衝突計算クラス
    /// </summary>
    public static class CollisionCalculation
    {
        /// <summary>
        /// 球体と球体の当たり判定
        /// </summary>
        public static bool CheckContacts(BallCollider sphere1, BallCollider sphere2)
        {
            var subV = sphere1.Center - sphere2.Center;
            var dr = subV.sqrMagnitude;
            var ar = sphere1.Radius + sphere2.Radius;
            //  重なり判定
            bool result = dr <= ar * ar + 0.01f;
            if (result && !sphere1.IsTrigger && !sphere2.IsTrigger)
            {
                var overlap = ar - Mathf.Sqrt(dr);
                var nv = subV.normalized;
                var solveV = nv * overlap / 2;
                sphere1.transform.position += solveV;
                sphere2.transform.position -= solveV;
            }
            return result;
        }
        /// <summary>
        /// 直方体と直方体の当たり判定
        /// </summary>
        public static bool CheckContacts(CubeCollider cube1, CubeCollider cube2)
        {
            var result = cube1.Left <= cube2.Right &&
                         cube1.Right >= cube2.Left &&
                         cube1.Down <= cube2.Up &&
                         cube1.Up >= cube2.Down &&
                         cube1.Back <= cube2.Forward &&
                         cube1.Forward >= cube2.Back;
            
            if (result && !cube1.IsTrigger && !cube2.IsTrigger)
            {
                var distance = cube1.Center - cube2.Center;
                distance.x = Mathf.Abs(distance.x);
                distance.y = Mathf.Abs(distance.y);
                distance.z = Mathf.Abs(distance.z);
                var overlap = (cube1.Size / 2 + cube2.Size / 2 - distance) / 2;
                float min = float.MaxValue;
                int minIndex = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (!(overlap[i] < min)) continue;
                    min = overlap[i];
                    minIndex = i;
                }

                Vector3 solveV = Vector3.zero;
                solveV[minIndex] = min;
                cube1.transform.position += solveV;
                cube2.transform.position -= solveV;
            }
            return result;
        }
        /// <summary>
        /// 球体と直方体の当たり判定
        /// </summary>
        public static bool CheckContacts(BallCollider ball, CubeCollider cube)
        {
            var subV = ball.Center - cube.ClosestPoint(ball.Center);
            var dr = subV.sqrMagnitude;
            var isInside = cube.CheckInside(ball.Center);
            bool result = dr < ball.Radius * ball.Radius + 0.01f || isInside;
            
            //  接触判定が通っている && どちらもトリガーではない && 両方Kinematicではない
            if (!result || ball.IsTrigger || cube.IsTrigger || ball.IsKinematic && cube.IsKinematic) return result;
            var overlap = ball.Radius - subV.magnitude;
            var nv = subV.normalized;
            Vector3 solveV;
            if (isInside)
            {
                var subVInside = cube.Center - cube.ClosestPoint(ball.Center);
                var overlapInside = subVInside.magnitude + ball.Radius;
                var nvInside = subVInside.normalized;
                solveV = nvInside * overlapInside;
            }
            else
            {
                solveV = nv * overlap;
            }
                
            if (!ball.IsKinematic && !cube.IsKinematic)
            {
                //  どちらもKinematicではないなら
                //  均等に押し合う
                ball.transform.position += solveV / 2;
                cube.transform.position -= solveV / 2;
            }
            else if (ball.IsKinematic && !cube.IsKinematic)
            {
                //  どちらか片方がKinematicならKinematicじゃない方だけ移動させる。
                cube.transform.position -= solveV;
            }
            else
            {
                ball.transform.position += solveV;
            }
            return result;
        }
    }
}
