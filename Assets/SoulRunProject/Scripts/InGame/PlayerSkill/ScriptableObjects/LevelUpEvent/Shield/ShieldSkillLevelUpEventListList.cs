using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class ShieldSkillLevelUpEventListList : ILevelUpEventListList
    {
        [SerializeField, EnumDrawer(typeof(SkillLevelLabel))] 
        List<ShieldSkillLevelUpEventList> _levelUpEventListList;
        public List<ILevelUpEventList> LevelUpEventListList => 
            _levelUpEventListList.OfType<ILevelUpEventList>().ToList();
        
        public void RefreshElement()
        {
            _levelUpEventListList[^1] = new();
        }
    }
}