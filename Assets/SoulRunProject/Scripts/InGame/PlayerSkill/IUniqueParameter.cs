using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// 独自パラメーターをSubclassSelectorで選べるようにするためのインターフェース
    /// </summary>
    public interface IUniqueParameter
    {
    }
    
    /// <summary>
    /// 跳ね返り回数
    /// </summary>
    public class ReboundParameter : IUniqueParameter
    {
        [SerializeField, Tooltip("跳ね返り回数")] int _reboundCount;
        public int ReboundCount => _reboundCount;
    }
}
