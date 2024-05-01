using System;
using System.Collections.Generic;
using SoulRunProject.Common;
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
        /// <summary>ランダムに流すかどうか</summary>
        /// <summary>一定に生成するためのタイルの組み合わせ</summary>
        [SerializeField] List<FieldSegment> _fieldSegments;
        /// <summary>ランダム生成のためのタイルの隣接関係</summary>
        [SerializeField] AdjacentGraph _adjacentGraph;
        /// <summary>ランダム生成で一番最初に流すタイル</summary>
        [SerializeField] FieldSegment _randomFirstSegment;
        /// <summary>ランダム生成で一番最後に流すタイル</summary>
        [SerializeField] FieldSegment _randomLastSegment;

        public FieldMoverMode Mode => _mode;
        public float Seconds => _seconds;
        public List<FieldSegment> FieldSegments => _fieldSegments;
        public AdjacentGraph AdjacentGraph => _adjacentGraph;
    }
}
