using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    [CreateAssetMenu(fileName = "SoulCard", menuName = "SoulRunProject/SoulCard")]
    [Serializable]
    public class SoulCard : ScriptableObject
    {
        // 固有番号
        [SerializeField] private int cardID;

        // カードの画像
        [SerializeField] private Sprite image;

        // 名前
        [SerializeField] private string soulName;

        // レベル
        [SerializeField] private int soulLevel;

        // 能力
        [SerializeField] private SoulAbility soulAbility;

        // 説明文
        [SerializeField] private string explanatoryText;

        // ステータス
        [SerializeField] private Status status;

        // 特性
        [SerializeField] private List<TraitWrapper> traitList;
    }
}