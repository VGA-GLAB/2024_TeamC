using System;
using DG.Tweening;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ドロップした経験値やアイテムの基底クラス
    /// </summary>
    public abstract class DropBase : MonoBehaviour, IPausable
    {
        [SerializeField, CustomLabel("跳ねるアニメーションの乗数(増やすとより大きく跳ねる)")] float _multiplier = 1f;
        [SerializeField, CustomLabel("跳ねるアニメーションの時間")] float _projectileMotionTime = 0.5f;
        [SerializeField, CustomLabel("1回転する時間")] float _rotateTime = 0.5f;
        [SerializeField, CustomLabel("出現する時の高さ。ワールド座標。")] float _dropHeight = 0.7f;
        Sequence _projectileMotionSequence;
        Tween _rotateTween;
        private bool _isPause;
        protected readonly Subject<Unit> FinishedSubject = new();
        public IObservable<Unit> OnFinishedAsync => FinishedSubject.Take(1);
        public PlayerManager Player { get; set; }
        public FieldMover FieldMover { get; set; }

        /// <summary>プレイヤーがドロップ品を拾った時に呼ぶ処理</summary>
        protected abstract void PickUp(PlayerManager playerManager);
        
        /// <summary>
        /// ランダムな方向に斜方投射するメソッド
        /// </summary>
        public void RandomProjectileMotion()
        {
            // 現在の斜方投射が終わるまで次の投射を行わない
            if (_projectileMotionSequence != null)　return;
            //  最終的に同じ高さで止まるので空中に出現しても地面から始めるようにする
            Vector3 newPosition = transform.position;
            newPosition.y = _dropHeight;
            transform.position = newPosition;
            
            Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), Random.Range(-1f, 1f));
            randomDir *= _multiplier;
            _projectileMotionSequence = DOTween.Sequence();
            _projectileMotionSequence.Append(transform.DOBlendableMoveBy(new Vector3(randomDir.x, 0f ,randomDir.z), _projectileMotionTime));
            _projectileMotionSequence.Insert(0f , transform.DOBlendableMoveBy(new Vector3(0f , randomDir.y ,0f), _projectileMotionTime / 2));
            _projectileMotionSequence.Insert(_projectileMotionTime / 2 , transform.DOBlendableMoveBy(new Vector3(0f , - randomDir.y ,0f), _projectileMotionTime / 2));
            _projectileMotionSequence.SetLink(gameObject).OnComplete(()=>_projectileMotionSequence = null);
        }

        private void Update()
        {
            if (_isPause) return;
            //  プレイヤーより後ろに行ったら吸引処理を行わない
            if (Player.transform.position.z > transform.position.z) return;
            
            var absorptionPower = Player.CurrentStatus.SoulAbsorption;
            var distance = Player.transform.position - transform.position;
            float moveSpeed = 10f + FieldMover.ScrollSpeed;
            if (distance.sqrMagnitude < absorptionPower * absorptionPower)
            {
                transform.position += distance.normalized * (moveSpeed * Time.deltaTime);
            }
        }

        void OnEnable()
        {
            _rotateTween = transform.DORotate(new Vector3(0,360,0),_rotateTime, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart).SetLink(gameObject);
        }

        void OnDisable()
        {
            _rotateTween?.Kill();
            _rotateTween = null;
            _projectileMotionSequence?.Kill();
            _projectileMotionSequence = null;
        }

        public void ForceFinish()
        {
            FinishedSubject.OnNext(Unit.Default);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerManager playerManager))
            {
                PickUp(playerManager);
            }
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
            if (isPause)
            {
                _projectileMotionSequence?.Pause();
                _rotateTween?.Pause();
            }
            else
            {
                _projectileMotionSequence?.Restart();
                _rotateTween?.Restart();
            }
        }
    }
}
