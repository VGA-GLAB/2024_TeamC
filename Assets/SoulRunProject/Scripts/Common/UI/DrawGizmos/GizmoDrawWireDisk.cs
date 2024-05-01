using UnityEngine;

namespace SoulRunProject.Common
{
    public static class GizmoDrawWireDisk
    {
        /// <summary>
        /// 2D円形のGizmosを描画するメソッド
        /// </summary>
        public static void DrawWireDisk(Vector3 position, float radius, Color color)
        {
            const float gizmoDiskThickness = 0.01f;
            // 参考 https://discussions.unity.com/t/draw-2d-circle-with-gizmos/123578/2
            var oldColor = Gizmos.color;
            Gizmos.color = color;
            var oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(position, Quaternion.identity, new Vector3(1, gizmoDiskThickness, 1));
            Gizmos.DrawWireSphere(Vector3.zero, radius);
            Gizmos.matrix = oldMatrix;
            Gizmos.color = oldColor;
        }
    }
}