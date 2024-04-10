using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// 与ノックバック処理のクラス
    /// </summary>
    public class SkillKnockBackController : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] float _power;

        public float Power => _power;
    }
}