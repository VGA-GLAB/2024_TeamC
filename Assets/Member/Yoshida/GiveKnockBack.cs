using System;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// 与ノックバック処理のクラス
    /// </summary>
    [Serializable]
    public class GiveKnockBack
    {
        [SerializeField,CustomLabel("ノックバック力"), Range(0, 1)] float _power;

        public float Power => _power;
    }
}