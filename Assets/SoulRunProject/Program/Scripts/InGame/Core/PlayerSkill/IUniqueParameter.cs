using UnityEngine;

namespace SoulRunProject.InGame.PlayerSkill
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
        [field: SerializeField, Tooltip("跳ね返り回数")] public int ReboundCount;
    }
}
