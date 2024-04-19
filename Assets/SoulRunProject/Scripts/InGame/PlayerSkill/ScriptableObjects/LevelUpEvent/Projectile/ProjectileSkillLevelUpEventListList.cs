using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("発射スキルイベント")]
    public class ProjectileSkillLevelUpEventListList : ILevelUpEventListList
    {
        [SerializeField, EnumDrawer(typeof(SkillLevelLabel))] 
        List<ProjectileSkillLevelUpEventList> _levelUpEventListList;
        public List<ILevelUpEventList> LevelUpEventListList => 
            _levelUpEventListList.OfType<ILevelUpEventList>().ToList();

        public void RefreshElement()
        {
            _levelUpEventListList[^1] = new();
        }
    }
}