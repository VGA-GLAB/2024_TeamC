using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace SoulRun.InGame
{
    /// <summary>
    /// 障害物を生成するクラス
    /// </summary>
    public class ObstacleGenerater : MonoBehaviour
    {
        /// <summary>生成物一覧 </summary>
        [SerializeField] List<GameObject> _obstacles = new();
        /// <summary> 生成間隔 </summary>
        [SerializeField] float _generateDur;
        /// <summary> オブジェクトの拡大率 </summary>
        //[SerializeField] float _addScaleNum = 0.5f;
        /// <summary> 生成地点の横の範囲 </summary>
        [SerializeField] NumRange _xNumRange;
        /// <summary> 生成地点の縦の範囲 </summary>
        [SerializeField] NumRange _yNumRange;
        /// <summary> 生成地点のZ座標 </summary>
        [SerializeField] float _z;

        private void Start()
        {
            SetObstacleExpandScaleNum();
            Observable.Interval(System.TimeSpan.FromSeconds(_generateDur)).Subscribe(x =>
            {
                GenerateEnemy();
            }).AddTo(this);
        }

        /// <summary>
        /// 敵が近づくに連れてどれだけ拡大するかの値を決める
        /// </summary>
        private void SetObstacleExpandScaleNum()
        {
            foreach (var obs in _obstacles)
            {
                obs.TryGetComponent(out ObstacleMover obstacle);
            }
        }

        /// <summary>
        /// 敵を生成する
        /// </summary>
        private void GenerateEnemy()
        {
            if (_obstacles.Count == 0) return;
            var obs = Instantiate(_obstacles[0], GetRandomGenerateNum(), Quaternion.identity);
            obs.TryGetComponent(out ObstacleStatusManager obstacleStatus);
        }

        /// <summary>
        /// 指定した範囲からランダムな生成地点を取得する
        /// </summary>
        /// <returns></returns>
        private Vector3 GetRandomGenerateNum()
        {
            float x = Random.Range(_xNumRange.Min, _xNumRange.Max);
            float y = Random.Range(_yNumRange.Min, _yNumRange.Max);
            float z = _z;
            return new Vector3(x, y, z);
        }
    }

    /// <summary>
    /// 最小と最大の値を持つクラス
    /// </summary>
    [System.Serializable]
    public class NumRange
    {
        [SerializeField] float _max = 0;
        [SerializeField] float _min = 0;

        public float Max => _max;
        public float Min => _min;

        public NumRange(float min, float max)
        {
            if (max < min)
            {
                _min = max; 
                _max = min;
            }
            else
            {
                _max = max;
                _min = min;
            }
        }
    }
}
