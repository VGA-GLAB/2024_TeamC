using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public interface IPlayerPausable
    {
        /// <summary>
        /// Trueでとめる、Falseでうごかす
        /// </summary>
        /// <param name="isPause"></param>
        void Pause(bool isPause);
    }
}
