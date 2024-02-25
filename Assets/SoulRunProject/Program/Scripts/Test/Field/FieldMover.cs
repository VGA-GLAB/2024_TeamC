using System;
using System.Collections.Generic;
using System.IO;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// フィールドを動かすクラス
    /// </summary>
    public class FieldMover : MonoBehaviour
    {
        [SerializeField] [Tooltip("フィールドに使う全ての部品")]
        FieldPartsDB _fieldPartsDB;

        [SerializeField] [Tooltip("フィールドの構成")] TextAsset _fieldStruct;

        [SerializeField] [Tooltip("移動速度")] float _moveSpeed = 5f;

        [SerializeField] [Tooltip("実行時にマップを生成するかどうか")]
        bool _playOnAwake = true;

        /// <summary>読み込んだマップのパーツの要素番号のリスト</summary>
        readonly List<int> _fieldIndexList = new();

        /// <summary>UpdateAsObservableをDisposeするためのリスト</summary>
        readonly List<IDisposable> _subscriptions = new();

        /// <summary>マップのスクロールが止まるべき場所</summary>
        Vector3 _endPosition;

        /// <summary>移動速度</summary>
        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        void Awake()
        {
            if (_playOnAwake) ImportFieldData();
        }

        /// <summary>
        ///     マップをスクロールさせるためのメソッド
        /// </summary>
        void UpdateMapPosition(Unit _)
        {
            var current = transform.position;
            if (_endPosition.z > current.z)
            {
                current.z += _moveSpeed * Time.deltaTime;
                transform.position = current;
            }
            else
            {
                _subscriptions.ForEach(s => s.Dispose());
            }
        }

        /// <summary>
        ///     フィールドデータを読み込んで生成する
        /// </summary>
        public void ImportFieldData()
        {
            if (!_fieldStruct)
            {
#if UNITY_EDITOR
                Debug.LogWarning("フィールドの構成ファイルがアタッチされていません");
#endif
                return;
            }

            //TextAssetのtextを読み込み専用クラスであるStringReaderに格納
            var reader = new StringReader(_fieldStruct.text); //csv
            // , で分割しつつ一行ずつ読み込み
            // リストに追加していく
            while (reader.Peek() != -1) // reader.Peekが-1になるまで
            {
                var line = reader.ReadLine(); // 一行ずつ読み込み
                foreach (var str in line.Split(','))
                    if (int.TryParse(str.Trim(), out var parsed))
                        _fieldIndexList.Add(parsed);
            }

            CreateField();
        }

        /// <summary>
        ///     フィールドを生成する
        /// </summary>
        void CreateField()
        {
            var prevPos = Vector3.zero;
            foreach (var index in _fieldIndexList)
                if (_fieldPartsDB.FieldParts.Length > index)
                {
                    var parts = Instantiate(_fieldPartsDB.FieldParts[index], transform);
                    parts.transform.position = prevPos;
                    prevPos = parts.EndAnchor.position;
                }

            _endPosition = new Vector3(Math.Abs(prevPos.x), Math.Abs(prevPos.y), Math.Abs(prevPos.z));
            this.UpdateAsObservable().Subscribe(UpdateMapPosition).AddTo(_subscriptions);
        }
    }
}