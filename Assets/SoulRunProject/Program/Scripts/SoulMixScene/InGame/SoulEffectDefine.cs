using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoulRun.InGame;
using UnityEngine;
using UnityEngine.UI;


namespace SoulRunProject.SoulMixScene
{
    public class SoulEffectDefine : MonoBehaviour
    {
        // 合成可能なSoulを格納するリスト
        private readonly List<SoulCard> _soulMixList = new List<SoulCard>();


        /// <summary> 合成可能かどうかを判定するメソッド boolを返す </summary>
        private bool CanMixSoul(SoulCard soul1, SoulCard soul2)
        {
            SoulEffectCompoMode compoMode = GetSoulEffectCompoMode(soul1, soul2);

            switch (compoMode)
            {
                case SoulEffectCompoMode.Possible:
                    return true;
                case SoulEffectCompoMode.Impossible:
                    return false;
                case SoulEffectCompoMode.OnlyNew:
                    return soul1.SoulLevel == 1 && soul2.SoulLevel == 1;
                case SoulEffectCompoMode.OnlyOwn:
                    return soul1.SoulName == soul2.SoulName;
                case SoulEffectCompoMode.OnlyOwnNew:
                    return soul1.SoulName == soul2.SoulName && soul1.SoulLevel == 1 && soul2.SoulLevel == 1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compoMode), compoMode, "Invalid combination mode.");
            }
        }

        /// <summary> 合成可能かどうかを判定するメソッド SoulEffectCompoModeを返す </summary>
        private SoulEffectCompoMode GetSoulEffectCompoMode(SoulCard soul1, SoulCard soul2)
        {
            if (_soulMixList.Contains(soul1) && _soulMixList.Contains(soul2))
            {
                return SoulEffectCompoMode.Possible;
            }
            else if (!_soulMixList.Contains(soul1) && !_soulMixList.Contains(soul2))
            {
                return SoulEffectCompoMode.Impossible;
            }
            else if (_soulMixList.Contains(soul1) && !_soulMixList.Contains(soul2))
            {
                return SoulEffectCompoMode.OnlyNew;
            }
            else if (!_soulMixList.Contains(soul1) && _soulMixList.Contains(soul2))
            {
                return SoulEffectCompoMode.OnlyOwn;
            }
            else if (soul1.SoulName == soul2.SoulName)
            {
                return SoulEffectCompoMode.OnlyOwnNew;
            }
            else
            {
                return SoulEffectCompoMode.Impossible;
            }
        }

        /// <summary> 2つのSoulを合成して新しいSoulを作成するメソッド </summary>
        private async UniTask<SoulCard> CreateSoul(SoulCard soul1, SoulCard soul2)
        {
            // 2つのSoulの特性を合成して新しいSoulを作成する
            HashSet<TraitDefine> mixedTraits = new HashSet<TraitDefine>(soul1.TraitList);
            // UnionWithメソッドは、2つのHashSetの要素を合成する
            mixedTraits.UnionWith(soul2.TraitList);

            //Playerが選択した特性を取得する
            List<TraitDefine> selectedTraits = await WaitForPlayerTraitSelection(mixedTraits);

            return
                new SoulCard(
                    "MixedSoul",
                    (soul1.SoulLevel + soul2.SoulLevel) / 2,
                    soul1.Ability + "&" + soul2.Ability,
                    new List<StatusDefine>(),
                    selectedTraits
                );
        }
        
        /// <summary> Playerが選択した特性を取得するメソッド </summary>
        private async UniTask<List<TraitDefine>> WaitForPlayerTraitSelection(HashSet<TraitDefine> mixedTraits)
        {
            // Playerが選択した特性を格納するリスト
            List<TraitDefine> selectedTraits = new List<TraitDefine>();
            // mixedTraitsの中から5つの特性をPlayerが選択する
            
            
            // 合成ボタンが押されるまで待機する
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));//TODO: ここはボタンが押されるまで待機するように変更する
            return selectedTraits;
        }


        void ShowTraitSelectionUI(SoulCard soul1, SoulCard soul2)
        {
            GameObject.Find("TraitSelectionUI").SetActive(true);
            var soul1TraitList = GameObject.Find("Soul1TraitList").GetComponent<Text>();
            var soul2TraitList = GameObject.Find("Soul2TraitList").GetComponent<Text>();

            soul1TraitList.text = string.Join("\n", soul1.TraitList);
            soul2TraitList.text = string.Join("\n", soul2.TraitList);
        }

        void DisplayNewSoulInfo(SoulCard soulCard)
        {
            var soulNameText = GameObject.Find("SoulNameText").GetComponent<Text>();
            var soulLevelText = GameObject.Find("SoulLevelText").GetComponent<Text>();

            soulNameText.text = soulCard.SoulName;
            soulLevelText.text = "レベル: " + soulCard.SoulLevel.ToString();
        }

        public enum SoulEffectCompoMode
        {
            Possible,
            Impossible,
            OnlyNew,
            OnlyOwn,
            OnlyOwnNew,
        }

        public static readonly Dictionary<SoulEffectCompoMode, string> EffectCompoModeName =
            new Dictionary<SoulEffectCompoMode, string>()
            {
                { SoulEffectCompoMode.Possible, "合成可能" },
                { SoulEffectCompoMode.Impossible, "合成不可能" },
                { SoulEffectCompoMode.OnlyNew, "新規追加のみ" },
                { SoulEffectCompoMode.OnlyOwn, "自分とカードとのみ合成可能" },
                { SoulEffectCompoMode.OnlyOwnNew, "自分とカードとのみ合成可能(新規追加のみ)" },
            };

        public static string GetEffectCompoModeName(SoulEffectCompoMode compoMode)
        {
            return EffectCompoModeName[compoMode];
        }
    }

    /// <summary> ステータスの定義 </summary>
    public enum StatusDefine
    {
        None, // なし
        Level, // レベル
        Hp, // HP
        Attack, // 基礎攻撃力
        Defense, // 防御力
        Speed, // 速度
        
    }

    /// <summary> 特性の定義 </summary>
    public enum TraitDefine
    {
        None, // なし
        Attack, // 攻撃
        Defense, // 防御
        Recovery, // 回復
        Speed, // 速度
        Critical, // クリティカル
        Counter, // 反撃
        Poison, // 毒
        Paralysis, // 麻痺
        Sleep, // 睡眠
        Confusion, // 混乱
        Charm, // 魅了
        Burn, // 火傷
        Freeze, // 凍結
        Stun, // 気絶
        Curse, // 呪い
        Seal, // 封印
        Blind, // 暗闇
        Fear, // 恐怖
        Silence, // 沈黙
        PoisonResist, // 毒耐性
        ParalysisResist, // 麻痺耐性
        SleepResist, // 睡眠耐性
        ConfusionResist, // 混乱耐性
        CharmResist, // 魅了耐性
        BurnResist, // 火傷耐性
        FreezeResist, // 凍結耐性
        StunResist, // 気絶耐性
        CurseResist, // 呪い耐性
        SealResist, // 封印耐性
        BlindResist, // 暗闇耐性
        FearResist, // 恐怖耐性
        SilenceResist, // 沈黙耐性
        PoisonAttack, // 毒攻撃
        ParalysisAttack, // 麻痺攻撃
        SleepAttack, // 睡眠攻撃
        ConfusionAttack, // 混乱攻撃
        CharmAttack, // 魅了攻
    }
}