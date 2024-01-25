using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoulRun.InGame;
using UnityEngine;
using UnityEngine.UI;


namespace SoulRunProject.SoulMixScene
{
    public class SoulEffectDefine : MonoBehaviour
    {
        // 合成可能なSoulを格納するリスト
        private readonly List<SoulCard> _soulMixList = new List<SoulCard>();


        /// <summary> 合成可能かどうかを判定するメソッド boolを返す </summary>
        private bool CanMixSoul(SoulCard soul1, SoulCard soul2)
        {
            return true;
        }

        /// <summary> 合成可能かどうかを判定するメソッド SoulEffectCompoModeを返す </summary>
        private bool GetSoulEffectCompoMode(SoulCard soul1, SoulCard soul2)
        {
            return true;
        }

        /// <summary> 2つのSoulを合成して新しいSoulを作成するメソッド </summary>
        
        
        /// <summary> Playerが選択した特性を取得するメソッド </summary>
        
    }

}