using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class AoESkill : AbstractSkill
    {
        private AoEController _aoeController;
        public AoESkill(AbstractSkillData skillData, in PlayerManager playerManager, in Transform playerTransform)
            : base(skillData, in playerManager, in playerTransform)
        {
        }

        private AoESkillData SkillData => (AoESkillData)_skillData;
        private AoESkillParameter RuntimeParameter => (AoESkillParameter)_runtimeParameter;

        public override void StartSkill()
        {
            _aoeController = Object.Instantiate(SkillData.Original);
            _aoeController.ApplyParameter(RuntimeParameter, _playerManagerInstance);
            CriAudioManager.Instance.PlaySE("SE_Holyfield");
        }

        public override void UpdateSkill(float deltaTime)
        {
            //  AoEの座標更新
            //var playerPosition = _playerTransform.position;
            //playerPosition.y = SkillData.GroundHeight;
            _aoeController.transform.position = _playerTransform.position;
        }

        public override void OnLevelUp()
        {
            _aoeController.ApplyParameter(RuntimeParameter, _playerManagerInstance);
        }
    }
}