using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    ///     フィールドを動かすクラス
    /// </summary>
    public class FieldMover : MonoBehaviour, IPausable
    {
        [SerializeField] [CustomLabel("最大タイル数")] private int _maxSegmentCount = 5;
        [SerializeField] [CustomLabel("スクロール速度")] private float _scrollSpeed = 5f;
        private bool _isPause;
        public List<FieldSegment> MoveSegments { get; private set; } = new();
        /// <summary>現在空いているタイルの生成数</summary>
        public int FreeMoveSegmentsCount => Mathf.Clamp(_maxSegmentCount - MoveSegments.Count, 0, _maxSegmentCount);

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

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }
    }
}