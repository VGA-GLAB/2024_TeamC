using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// SubclassSelectorでカウント方法を選べるようにするためのインターフェース。
    /// </summary>
    public interface ICount
    {
        int GetCount { get; }
    }
    /// <summary>
    /// 通常のカウント
    /// </summary>
    public class BasicCount : ICount
    {
        [SerializeField] int _count;
        public int GetCount => _count;
    }
    /// <summary>
    /// 最小値と最大値を指定してランダムな値を出力する。(UnityEngine.Random.Range)
    /// </summary>
    public class RandomCount : ICount
    {
        [SerializeField] int _min;
        [SerializeField] int _max;
        public int GetCount => Random.Range(_min, _max);
    }
}