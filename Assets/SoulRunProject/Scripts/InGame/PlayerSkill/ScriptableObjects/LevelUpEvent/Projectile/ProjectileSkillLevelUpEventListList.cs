using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("発射スキルイベント")]
    public class ProjectileSkillLevelUpEventListList : ILevelUpEventListList
    {
        [SerializeField, Header("レベルアップイベントテーブル")] 
        List<ProjectileSkillLevelUpEventList> _levelUpEventListList;
        public List<ILevelUpEventList> LevelUpEventListList => 
            _levelUpEventListList.OfType<ILevelUpEventList>().ToList();
    }
}