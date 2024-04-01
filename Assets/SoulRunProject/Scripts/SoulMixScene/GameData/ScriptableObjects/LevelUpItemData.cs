using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// レベルアップ時に提示されるアイテムデータ
    /// </summary>
    [CreateAssetMenu(fileName = "LevelUpItemData", menuName = "SoulRunProject/LevelUpItemData")]
    public class LevelUpItemData : ScriptableObject
    {
        [Header("スキルレベルアップ系")]
        [SerializeField] private SkillLevelUpItem[] _skillLevelUpItems = new SkillLevelUpItem[1];
        [Header("ステータスアップ系")]
        [SerializeField] private StatusUpItem[] _statusUpItems = new StatusUpItem[2];

        public SkillLevelUpItem[] SkillLevelUpItems => _skillLevelUpItems;
        public StatusUpItem[] StatusUpItems => _statusUpItems;
    }
}
