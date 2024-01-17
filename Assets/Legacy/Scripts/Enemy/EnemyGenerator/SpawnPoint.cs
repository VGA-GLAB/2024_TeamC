using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// 生成位置と生成すべき敵の情報を格納する
    /// </summary>
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _generateEnemy;
        public Vector3 GeneratePos => transform.position;
        public GameObject GenerateEnemy => _generateEnemy;

#if UNITY_EDITOR
        private float _gizmosSize = 1f;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _gizmosSize);
            if (_generateEnemy != null)
                Handles.Label(transform.position + new Vector3(0, 1.5f, 0), $"{_generateEnemy.GetComponent<Enemy>().Name}");
            else
                Handles.Label(transform.position + new Vector3(0, 1.5f, 0), $"敵が設定されていません");
        }
#endif
    }
}
