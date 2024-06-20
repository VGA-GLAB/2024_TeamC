using System.Collections.Generic;
using System.Linq;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Common
{
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/LaserSkill")]
    public class LaserSkillData : AbstractSkillData
    {
        [SerializeField, CustomLabel("レベルアップ時のイベントテーブル(パラメーター増加など)")] 
        private LevelUpEventTable<LaserLevelUpEvent> _levelUpTable;
        [SerializeField, CustomLabel("生成するプレハブ")] private LaserController _original;
        [SerializeField, CustomLabel("高さのオフセット")] private float _offsetY = 10f;
        [SerializeField, CustomLabel("後方に半円状にレーザーを展開する際の円の半径")] private float _radius = 5f;
        [SerializeField, CustomLabel("複数レーザーがある際に、次のレーザーを指定した秒数遅れて起動させる")] private float _delay = 0.1f;
        
        private LaserSkillData()
        {
            _skillParameter = new LaserSkillParameter();
        }
        //  プロパティ
        public override List<List<ILevelUpEvent>> LevelUpTable  =>
            _levelUpTable.List.Select(list => list.List.OfType<ILevelUpEvent>().ToList()).ToList();
        
        public LaserSkillParameter Parameter => (LaserSkillParameter)_skillParameter;
        public LaserController Original => _original;
        public float Delay => _delay;
        public float OffsetY => _offsetY;
        public float Radius => _radius;
    }
}