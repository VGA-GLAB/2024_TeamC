using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SoulRunProject.InGame.Field
{
    /// <summary>
    /// フィールドを動かすクラス
    /// </summary>
    public class FieldMover : MonoBehaviour
    {
        [SerializeField] [Tooltip("フィールドのデータ")] FieldData _fieldData;
        [SerializeField] [Tooltip("スクロール速度")] float _scrollSpeed = 5f;
        [SerializeField] [Tooltip("実行時にマップを生成するかどうか")]
        bool _playOnAwake = true;
        /// <summary>UpdateAsObservableをDisposeするためのリスト</summary>
        readonly List<IDisposable> _subscriptions = new();

        /// <summary>マップのスクロールが止まるべき場所</summary>
        Vector3 _endPosition;

        /// <summary>スクロール速度</summary>
        public float ScrollSpeed
        {
            get => _scrollSpeed;
            set => _scrollSpeed = value;
        }

        void Awake()
        {
            if (_playOnAwake) CreateField();
        }

        /// <summary>
        /// マップをスクロールさせるためのメソッド
        /// </summary>
        void UpdateMapPosition(Unit _)
        {
            var current = transform.position;
            if (_endPosition.z > current.z)
            {
                current.z += _scrollSpeed * Time.deltaTime;
                transform.position = current;
            }
            else
            {
                _subscriptions.ForEach(s => s.Dispose());
            }
        }
        /// <summary>
        /// フィールドを生成する
        /// </summary>
        public void CreateField()
        {
            var prevPos = Vector3.zero;
            foreach (var datum in _fieldData.FieldParts)
            {
                var parts = Instantiate(datum, transform);
                parts.transform.position = prevPos;
                prevPos = parts.EndAnchor.position;
            }

            _endPosition = new Vector3(Math.Abs(prevPos.x), Math.Abs(prevPos.y), Math.Abs(prevPos.z));
            this.UpdateAsObservable().Subscribe(UpdateMapPosition).AddTo(_subscriptions);
        }
    }
}