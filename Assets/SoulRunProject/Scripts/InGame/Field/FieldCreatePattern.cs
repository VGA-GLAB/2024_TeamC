using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.InGame
{
    [Serializable]
    public class FieldCreatePattern
    {
        [SerializeField, Header("無限に流すかどうか")] bool _isInfinity;
        [SerializeField, Header("流す秒数")] float _seconds;
        [SerializeField, Header("ランダムに流すかどうか")] bool _isRandom;
        [SerializeField, Header("流すタイルの組み合わせ")] List<FieldSegment> _fieldSegments;

        public bool IsInfinity => _isInfinity;
        public float Seconds => _seconds;
        public bool IsRandom => _isRandom;
        public List<FieldSegment> FieldSegments => _fieldSegments;
    }
}
