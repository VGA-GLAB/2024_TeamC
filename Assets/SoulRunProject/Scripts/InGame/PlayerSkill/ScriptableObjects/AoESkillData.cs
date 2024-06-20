using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    ///     範囲攻撃スキル
    /// </summary>
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/AoESkill")]
    public class AoESkillData : AbstractSkillData
    {
        //  メンバー変数
        [SerializeField, CustomLabel("レベルアップ時のイベントテーブル(パラメーター増加など)")] private LevelUpEventTable<AoELevelUpEvent> _levelUpTable;
        [SerializeField] [CustomLabel("生成するAoEプレハブ")] private AoEController _original;
        [SerializeField] [CustomLabel("展開する地面の高さ、y座標")] private float _groundHeight;
        //  コンストラクター
        private AoESkillData()
        {
            _skillParameter = new AoESkillParameter();
        }
        //  プロパティ
        public override List<List<ILevelUpEvent>> LevelUpTable =>
            _levelUpTable.List.Select(list => list.List.OfType<ILevelUpEvent>().ToList()).ToList();

        public AoESkillParameter Parameter => (AoESkillParameter)_skillParameter;
        public AoEController Original => _original;
        public float GroundHeight => _groundHeight;
    }
}