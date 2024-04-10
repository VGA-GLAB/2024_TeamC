using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class AoESkillLevelUpEventList : ILevelUpEventList
    {
        [SerializeReference, SubclassSelector, Header("範囲スキルレベルアップイベント")] 
        List<AoELevelUpEvent> _levelUpEventList;
        public List<ILevelUpEvent> LevelUpEventList => _levelUpEventList.OfType<ILevelUpEvent>().ToList();
    }
}