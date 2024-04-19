using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace SoulRunProject.Common
{
    [Serializable]
    public class HealingSkillLevelUpEventList : ILevelUpEventList
    {
        [SerializeReference, SubclassSelector, Header("ヒールスキルレベルアップイベント")] 
        List<HealingLevelUpEvent> _levelUpEventList;
        public List<ILevelUpEvent> LevelUpEventList => _levelUpEventList.OfType<ILevelUpEvent>().ToList();
    }
}
