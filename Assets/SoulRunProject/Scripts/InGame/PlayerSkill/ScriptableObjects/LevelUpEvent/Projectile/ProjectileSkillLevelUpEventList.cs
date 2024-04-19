using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class ProjectileSkillLevelUpEventList : ILevelUpEventList
    {
        [SerializeReference, SubclassSelector , EnumDrawer(typeof(SkillLevelEventLabel))] 
        List<ProjectileLevelUpEvent> _levelUpEventList;
        public List<ILevelUpEvent> LevelUpEventList => _levelUpEventList.OfType<ILevelUpEvent>().ToList();
    }
}