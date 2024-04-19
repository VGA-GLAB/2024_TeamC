using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class ShieldSkillLevelUpEventList : ILevelUpEventList
    {
        [SerializeReference, SubclassSelector, EnumDrawer(typeof(SkillLevelEventLabel))] 
        List<ShieldLevelUpEvent> _levelUpEventList;
        public List<ILevelUpEvent> LevelUpEventList => _levelUpEventList.OfType<ILevelUpEvent>().ToList();
    }
}