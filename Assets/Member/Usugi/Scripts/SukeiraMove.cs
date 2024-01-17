using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SoulRun.InGame
{
    /// <summary>
    /// 頭のオブジェクトに付けるスケイラのような動きをさせるscript
    /// </summary>
    [RequireComponent(typeof(EnemyMoveSinCurve))]
    public class SukeiraMove : EnemyMoveRoutineBase
    {
        private int _numberOfSegments = 10;
        private float _segmentDelay = 0.1f;
        private List<GameObject> _segments = new List<GameObject>();
        private List<Vector3> _targetPositions = new List<Vector3>();
        [SerializeField] private NumRange verticalMoveRange;
        [SerializeField] private EnemyMoveSinCurve _enemyMoveSinCurve; 
        [SerializeField] GameObject sukeiraBodyPrefab;

        void Awake()
        {   //変数の宣言、胴体部分の生成を行う
            TryGetComponent(out _enemyMoveSinCurve);
  
            for (int i = 0; i < _numberOfSegments; i++)
            {
                GameObject segment = Instantiate(sukeiraBodyPrefab, transform.position, Quaternion.identity);
                _segments.Add(segment);
            }
        }

        private void Update()
        {
            if (transform.position.z < verticalMoveRange.Min)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, verticalMoveRange.Min);
                _enemyMoveSinCurve.ReverseDirection();
            }
            else if (transform.position.z > verticalMoveRange.Max)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, verticalMoveRange.Max);
                _enemyMoveSinCurve.ReverseDirection();
            }
        }

        public override void ActivateMoveRoutine()
        {
            _enemyMoveSinCurve.ActivateMoveRoutine();
            StartCoroutine(SetTargetPos());
            StartCoroutine(FollowLeader());
        }

        public override void DeactivateMoveRoutine()
        {
            _enemyMoveSinCurve.DeactivateMoveRoutine();
            StopAllCoroutines();
        }

        private IEnumerator FollowLeader()
        {

            while (true)
            {
                // フォロワーセグメントの位置更新
                for (int i = 0; i < _targetPositions.Count; i++)
                {
                    _segments[i].transform.DOMove(_targetPositions[i], _segmentDelay).SetEase(Ease.Linear).SetLink(_segments[i]);
                }

                // 次の更新まで待機
                yield return new WaitForSeconds(_segmentDelay);
            }
        }

        private IEnumerator SetTargetPos()
        {
            while (true)
            {
                _targetPositions.Add(transform.position);

                if (_targetPositions.Count > _numberOfSegments) _targetPositions.RemoveAt(0);
                yield return new WaitForSeconds(_segmentDelay);
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _segments.Count; i++)
            {
                Destroy(_segments[i], 0.1f * i);
            }
        }
    }
}
