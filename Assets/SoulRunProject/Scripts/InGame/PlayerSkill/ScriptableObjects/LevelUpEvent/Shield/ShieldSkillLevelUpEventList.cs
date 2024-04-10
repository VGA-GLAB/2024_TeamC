using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class ShieldSkillLevelUpEventList : ILevelUpEventList
    {
        [SerializeReference, SubclassSelector, Header("投射物レベルアップイベント")] 
        List<ShieldLevelUpEvent> _levelUpEventList;
        public List<ILevelUpEvent> LevelUpEventList => _levelUpEventList.OfType<ILevelUpEvent>().ToList();
    }
}