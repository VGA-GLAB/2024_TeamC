using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SoulRunProject.Common
{
    [Serializable]
    public class HealingSkillLevelUpEventListList : ILevelUpEventListList
    {
        [SerializeField, EnumDrawer(typeof(SkillLevelLabel))] 
        List<HealingSkillLevelUpEventList> _levelUpEventListList;
        public List<ILevelUpEventList> LevelUpEventListList => 
            _levelUpEventListList.OfType<ILevelUpEventList>().ToList();
        public void RefreshElement()
        {
            _levelUpEventListList[^1] = new();
        }
    }
}
