using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.Skill
{
    /// <summary>
    ///     ダメージ無効化スキル
    /// </summary>
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/ShieldSkill")]
    public class ShieldSkillData : AbstractSkillData
    {
        //  メンバー変数
        [SerializeField, CustomLabel("レベルアップ時のイベントテーブル(パラメーター増加など)")] 
        private LevelUpEventTable<ShieldLevelUpEvent> _levelUpTable;
        [SerializeField] [CustomLabel("シールドの見た目、プレハブ")] private GameObject _original;
        //  コンストラクター
        private ShieldSkillData()
        {
            _skillParameter = new ShieldSkillParameter();
        }
        //  プロパティ
        public override List<List<ILevelUpEvent>> LevelUpTable =>
            _levelUpTable.List.Select(list => list.List.OfType<ILevelUpEvent>().ToList()).ToList();

        public ShieldSkillParameter Parameter => (ShieldSkillParameter)_skillParameter;
        public GameObject Original => _original;
    }
}