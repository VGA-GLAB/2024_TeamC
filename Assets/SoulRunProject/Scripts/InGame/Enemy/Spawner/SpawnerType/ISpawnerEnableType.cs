using UnityEngine;

namespace SoulRunProject.Common.Interface
{
    public interface ISpawnerEnableType
    {
        public bool IsEnable(Vector3 playerPosition, Vector3 spawnerPosition);
        public void DrawSpawnerArea(Vector3 pos);
    }
}