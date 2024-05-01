using System;
using SoulRunProject.Common.Interface;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("範囲外に出ると起動")]
    public class SpawnerOnExit : ISpawnerEnableType
    {
        [SerializeField, CustomLabel("範囲 (青の範囲)"), Range(0, 100)]
        float _spawnerEnableRange;

        bool _playerOnEnter;

        public bool IsEnable(Vector3 playerPosition, Vector3 spawnerPosition)
        {
            bool CheckDistance() => _spawnerEnableRange > Vector3.Distance(playerPosition, spawnerPosition);
            
            if (CheckDistance())
            {
                _playerOnEnter = true;
            }

            if (_playerOnEnter)
            {
                return !CheckDistance();
            }

            return false;
        }

        public void DrawSpawnerArea(Vector3 pos)
        {
            GizmoDrawWireDisk.DrawWireDisk(pos, _spawnerEnableRange, Color.blue);
        }
    }
}