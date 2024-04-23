using System;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    /// <summary>
    /// ソウルカードのマスタデータ
    /// </summary>
    [CreateAssetMenu(fileName = "SoulCard", menuName = "SoulRunProject/SoulCard")]
    [Serializable]
    public class SoulCardMasterData : ScriptableObject
    {
        [SerializeField , CustomLabel("カードID")] private int _cardID;
        [SerializeField, CustomLabel("アイコン画像")] private Sprite _image;
        [SerializeField, CustomLabel("カード名")] private string _soulName;
        [SerializeField, CustomLabel("ソウルスキル")] private SoulSkillBase _soulSkill;
        [SerializeField, CustomLabel("基礎ステータス")] private Status _baseStatus;
        [SerializeField, CustomLabel("最大レベル")] private int _soulMaxLevel;

        public int CardID => _cardID;
        public Sprite Image => _image;

        public string SoulName => _soulName;

        public SoulSkillBase SoulSkill => _soulSkill;

        public Status BaseStatus => _baseStatus;

        public int SoulMaxLevel => _soulMaxLevel;
    }
}