using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRunProject.SoulMixScene
{
    public class SoulView : MonoBehaviour
    {
        // ソウルカードの参照
        [SerializeField] private SoulCard _soulCard;

        // UI要素への参照
        [SerializeField] private Text idText;
        [SerializeField] private Image soulImage;
        [SerializeField] private Text nameText;
        [SerializeField] private Text levelText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Text statusText;
        [SerializeField] private Text traitsText;
        [SerializeField] private Text skillsText;

        void Start()
        {
            // 各UI要素に値を設定
            // idText.text = "ID: " + _soulCard.CardID.ToString();
            // soulImage.sprite = _soulCard.Image; // 画像の設定
            // nameText.text = "Name: " + _soulCard.soulName;
            // levelText.text = "Level: " + _soulCard.soulLevel.ToString();
            // descriptionText.text = "Description: " + _soulCard.ExplanatoryText;
            //
            // // ステータス、特性、技の表示は、それぞれのクラスの実装に応じて変更する必要があります。
            // statusText.text = "Status: " + GetStatusText(_soulCard.Status);
            // traitsText.text = "Traits: " + GetTraitsText(_soulCard.TraitList);
            // skillsText.text = "Skill: " + GetSkillsText(_soulCard.soulAbility);
        }

        private string GetStatusText(Status status)
        {
            // Statusクラスに応じた表示方法を実装
            return status.ToString(); // 仮の実装
        }

        private string GetTraitsText(List<ITraitInterface> traits)
        {
            // 特性の表示方法を実装
            return String.Join(", ", traits.Select(t => t.ToString())); // 仮の実装
        }

        private string GetSkillsText(SoulAbility soulAbility)
        {
            // 技の表示方法を実装
            return soulAbility.ToString(); // 仮の実装
        }
    }
}