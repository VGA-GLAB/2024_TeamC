using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Common;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject
{
    /// <summary>
    ///     飛翔物発射スキル
    ///     <br />表現を加える際にはさらにこれを継承する
    /// </summary>
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/ProjectileSkill")]
    public class ProjectileSkillData : AbstractSkillData
    {
        //  メンバー変数
        [SerializeField, CustomLabel("レベルアップ時のイベントテーブル(パラメーター増加など)")] private LevelUpEventTable<ProjectileLevelUpEvent> _levelUpTable;

        [SerializeField] [CustomLabel("発射する弾のプレハブ")]
        private PlayerBullet _bullet;

        [SerializeField] [CustomLabel("複数弾を発射する際に与える回転基準")]
        private float _baseRotateY = 5f;

        [SerializeField] private Vector3 _muzzleOffset;
        //  コンストラクター
        private ProjectileSkillData()
        {
            _skillParameter = new ProjectileSkillParameter();
        }
        //  プロパティ
        public override List<List<ILevelUpEvent>> LevelUpTable =>
            _levelUpTable.List.Select(list => list.List.OfType<ILevelUpEvent>().ToList()).ToList();

        public ProjectileSkillParameter Parameter => (ProjectileSkillParameter)_skillParameter;
        public PlayerBullet Bullet => _bullet;
        public float BaseRotateY => _baseRotateY;
        public Vector3 MuzzleOffset => _muzzleOffset;
    }
}