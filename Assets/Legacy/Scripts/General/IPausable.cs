using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// ポーズ処理のインターフェース
    /// </summary>
    public interface IPausable
    {
        public void Active();
        public void Pause();
    }
}
