using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// レベルアップ時に提示されるアイテムデータ
    /// </summary>
    [CreateAssetMenu(fileName = "LevelUpItemData", menuName = "SoulRunProject/LevelUpItemData")]
    public class LevelUpItemData : ScriptableObject
    {
        [Header("ステータスアップ系")]
        [SerializeField] private StatusUpItem[] _statusUpItems = new StatusUpItem[2];

        public StatusUpItem[] StatusUpItems => _statusUpItems;
    }
}
