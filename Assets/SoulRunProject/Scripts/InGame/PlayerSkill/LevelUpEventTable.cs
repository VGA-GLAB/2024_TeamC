using System;
using System.Collections.Generic;
using SoulRunProject.Runtime;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class LevelUpEventTable<T> where T : ILevelUpEvent
    {
        [SerializeField, CustomElementLabel("レベル", 2)] public List<LevelUpEventList<T>> List;
    }
    [Serializable]
    public class LevelUpEventList<T> where T : ILevelUpEvent
    {
        [SerializeField, CustomElementLabel("イベント", 1)] public List<T> List;
    }
}