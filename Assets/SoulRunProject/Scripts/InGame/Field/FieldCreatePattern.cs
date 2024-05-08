using System;
using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.Runtime;
using UnityEngine;

namespace SoulRunProject.InGame
{
    [Serializable]
    public class FieldCreatePattern
    {
        /// <summary>生成モード</summary>
        [SerializeField] FieldMoverMode _mode;
        /// <summary>流す秒数</summary>
        [SerializeField] float _seconds;
        /// <summary>一定に生成するためのタイルの組み合わせ</summary>
        [SerializeField, ShowWhenEnum(nameof(_mode), FieldMoverMode.Order)]
        private ListWrapper<FieldSegment> _fieldSegments;
        /// <summary>ランダム生成のためのタイルの隣接関係</summary>
        [SerializeField, ShowWhenEnum(nameof(_mode), FieldMoverMode.Random)] AdjacentGraph _adjacentGraph;
        public FieldMoverMode Mode => _mode;
        public float Seconds => _seconds;
        public List<FieldSegment> FieldSegments => _fieldSegments.List;
        public AdjacentGraph AdjacentGraph => _adjacentGraph;
    }
    [Serializable]
    public struct ListWrapper<T>
    {
        public List<T> List;
    }
}
