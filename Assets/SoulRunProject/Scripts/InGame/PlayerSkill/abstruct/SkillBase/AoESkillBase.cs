using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// 範囲攻撃スキル実行クラス
    /// </summary>
    public class AoESkillBase : SkillBase
    {
        [SerializeField, Tooltip("InstantiateするAoEプレハブ")] AoEController _original;
        [SerializeField, Tooltip("地面の高さ、y座標")] float _groundHeight;
        static Transform _playerTransform;
        AoEController _aoeController;
        /// <summary>
        /// パラメーターを適用する
        /// </summary>
        void ParameterSet()
        {
            if (SkillBaseParam is AoESkillParameter param)
            {
                _aoeController.Initialize(param.AttackDamage, param.Range);
            }
            else
            {
                Debug.LogError($"パラメータが{nameof(AoESkillParameter)}ではありません　");
            }
        }
        public override void StartSkill()
        {
            _playerTransform = Object.FindObjectOfType<PlayerManager>().transform;
            _aoeController = Object.Instantiate(_original);
            ParameterSet();
        }

        public override void UpdateSkill(float deltaTime)
        {
            //  AoEの座標更新
            var playerPosition = _playerTransform.position;
            playerPosition.y = _groundHeight;
            _aoeController.transform.position = playerPosition;
        }

        public override void OnLevelUp()
        {
            ParameterSet();
        }
    }
}