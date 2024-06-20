using System;
using SoulRunProject.Common.Interface;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("ラインを越えたら起動")]
    public class SpawnerOnPassThroughLine : ISpawnerEnableType
    {
        [SerializeReference, CustomLabel("起動ライン (赤のライン)"), Range(-50, 0)]
        float _throughLinePos;

        public bool IsEnable(Vector3 playerPosition, Vector3 spawnerPosition) => spawnerPosition.z + _throughLinePos < playerPosition.z;

        public void DrawSpawnerArea(Vector3 pos)
        {
            var line = new Vector3(pos.x, pos.y, pos.z + _throughLinePos);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(line.x - 10, line.y, line.z), new Vector3(line.x + 10, line.y, line.z));
        }
    }
}