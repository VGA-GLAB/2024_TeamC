using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    ///     フィールドを動かすクラス
    /// </summary>
    public class FieldMover : MonoBehaviour, IPausable
    {
        [SerializeField] [CustomLabel("最大タイル数")] private int _maxSegmentCount = 5;
        [SerializeField] [CustomLabel("最大速度に戻る補完値")][Range(0 ,10f)] private float _lerpPower = 5f;
        private bool _isPause;
        private float _scrollSpeed;
        public List<FieldSegment> MoveSegments { get; private set; } = new();
        /// <summary>現在空いているタイルの生成数</summary>
        public int FreeMoveSegmentsCount => Mathf.Clamp(_maxSegmentCount - MoveSegments.Count, 0, _maxSegmentCount);
        public PlayerStatus CurrentPlayerStatus { private get; set; }

        public void DownSpeed(float speed)
        {
            _scrollSpeed -= speed;
            if (_scrollSpeed < 0)
            {
                _scrollSpeed = 0f;
            }
        }

        private void Awake()
        {
            Register();
        }

        private void OnDestroy()
        {
            UnRegister();
        }

        private void Update()
        {
            if (_isPause) return;
            List<FieldSegment> list = new();


            //  フィールドタイル移動処理
            for (var i = 0; i < MoveSegments.Count; i++)
            {
                MoveSegments[i].transform.position += Vector3.back * (_scrollSpeed * Time.deltaTime);
                //  FieldMoverのz座標より後ろに行ったら消す
                if (MoveSegments[i].transform.TransformPoint(MoveSegments[i].EndPos).z < transform.position.z)
                {
                    foreach (var entity in MoveSegments[i].transform.GetComponentsInChildren<DamageableEntity>())
                    {
                        entity.Finish();
                    }
                    //  破棄する。
                    Destroy(MoveSegments[i].gameObject);
                }
                else
                {
                    list.Add(MoveSegments[i]);
                }
            }

            //  Destroyした要素を取り除く
            MoveSegments = list;
        }

        private void FixedUpdate()
        {
            //  現在のスピードが、ほぼ最大スピードでなければ
            if (!Mathf.Approximately(CurrentPlayerStatus.MoveSpeed , _scrollSpeed))
            {
                _scrollSpeed = Mathf.Lerp(_scrollSpeed, CurrentPlayerStatus.MoveSpeed, _lerpPower * Time.fixedDeltaTime);
            }
        }

        public void Register()
        {
            PauseManager.RegisterPausableObject(this);
        }

        public void UnRegister()
        {
            PauseManager.UnRegisterPausableObject(this);
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }
    }
}