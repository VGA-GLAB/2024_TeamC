using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class AoESkillLevelUpEventGroup : ILevelUpEventGroup
    {
        [SerializeReference, SubclassSelector, Header("範囲スキルレベルアップイベント")] 
        List<AoELevelUpEvent> _levelUpEventType;
        public List<ILevelUpEvent> LevelUpType => _levelUpEventType.OfType<ILevelUpEvent>().ToList();
    }
}