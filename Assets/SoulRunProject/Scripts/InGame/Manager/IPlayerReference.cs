using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// プレイヤーの参照を提供するインターフェース
    /// </summary>
    public interface IPlayerReference
    {
        Transform Player { get; }
    }
}