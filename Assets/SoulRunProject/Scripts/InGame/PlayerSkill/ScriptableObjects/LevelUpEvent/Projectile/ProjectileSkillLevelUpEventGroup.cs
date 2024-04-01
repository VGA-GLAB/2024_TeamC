using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class ProjectileSkillLevelUpEventGroup : ILevelUpEventGroup
    {
        [SerializeReference, SubclassSelector, Header("投射物レベルアップイベント")] 
        List<ProjectileLevelUpEvent> _levelUpEventType;
        public List<ILevelUpEvent> LevelUpType => _levelUpEventType.OfType<ILevelUpEvent>().ToList();
    }
}