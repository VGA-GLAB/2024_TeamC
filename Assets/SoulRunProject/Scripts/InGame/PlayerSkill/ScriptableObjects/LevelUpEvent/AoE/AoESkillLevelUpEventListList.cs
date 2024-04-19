using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("範囲スキルイベント")]
    public class AoESkillLevelUpEventListList : ILevelUpEventListList
    {
        [SerializeField , EnumDrawer(typeof(SkillLevelLabel))] 
        List<AoESkillLevelUpEventList> _levelUpEventListList;
        public List<ILevelUpEventList> LevelUpEventListList => 
            _levelUpEventListList.OfType<ILevelUpEventList>().ToList();
        
        public void RefreshElement()
        {
            _levelUpEventListList[^1] = new();
        }
    }
    
}