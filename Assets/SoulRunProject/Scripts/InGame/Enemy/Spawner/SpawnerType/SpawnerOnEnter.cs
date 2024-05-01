using System;
using SoulRunProject.Common.Interface;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("範囲内に入れば起動")]
    public class SpawnerOnEnter : ISpawnerEnableType
    {
        [SerializeField, CustomLabel("生成開始距離 (緑の範囲)"), Range(0, 100)]
        float _spawnerEnableRange;

        public bool IsEnable(Vector3 playerPosition, Vector3 spawnerPosition) =>
            _spawnerEnableRange > Vector3.Distance(playerPosition, spawnerPosition);

        public void DrawSpawnerArea(Vector3 pos)
        {
            GizmoDrawWireDisk.DrawWireDisk(pos, _spawnerEnableRange, Color.green);
        }
    }
}