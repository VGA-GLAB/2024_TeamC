using System;
using UnityEngine;
using UnityEngine.UI;
using SoulRunProject.Common;
using Unity.Burst;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// レベルアップ時に獲得可能なアイテムのBase
    /// </summary>
    /// <typeparam name="T"> プレイヤー強化処理で使用する参照の型 </typeparam>
    /// プレイヤーの強化処理で欲しいScriptが違う
    public abstract class LevelUpItemBase<T>
    {
        [SerializeField] protected string _itemName;
        [SerializeField] protected Image _itemImage;
        protected T _reference;

        public string ItemName => _itemName;
        public Image ItemImage => _itemImage;

        /// <summary> プレイヤー強化処理で使用するScript参照を取得する </summary>
        /// <param name="reference"> プレイヤー強化処理で使用する参照 </param>
        public void GetReference(T reference)
        {
            _reference = reference;
        }
        
        /// <summary> アイテムの効果処理 </summary>
        public abstract void ItemEffect();
    }

    /// <summary>
    /// 特定スキルのレベルを上げるアイテム
    /// </summary>
    [Serializable]
    public class SkillLevelUpItem : LevelUpItemBase<SkillManager>
    {
        [SerializeField, Tooltip("レベルアップさせるスキル")] private PlayerSkill _skillToLevelUp;
        
        public override void ItemEffect()
        {
            if (_reference.CurrentSkillTypes.Contains(_skillToLevelUp)) // そのスキルを持っている
            {
                _reference.LevelUpSkill(_skillToLevelUp);
            }
            else // 持っていない
            {
                _reference.AddSkill(_skillToLevelUp);
            }
        }
    }

    /// <summary>
    /// プレイヤーのステータスを複数上げるアイテム
    /// </summary>
    [Serializable]
    public class StatusUpItem : LevelUpItemBase<PlayerManager>
    {
        [SerializeField] private StatusEffect[] ItemEffects;
        
        public override void ItemEffect()
        {
            foreach (var statusEffect in ItemEffects)
            {
                statusEffect.UpStatus(_reference);
            }   
        }

        [Serializable]
        class StatusEffect
        {
            [SerializeField, Tooltip("上げるステータス")] private StatusType _statusToUp;
            [SerializeField, Tooltip("上昇量")] private float _upValue;

            /// <summary> 指定されたステータスを指定された値上昇させる </summary>
            /// <param name="playerManager"> 強化するPlayerManager </param>
            public void UpStatus(PlayerManager playerManager)
            {
                switch (_statusToUp)
                {
                    case StatusType.Hp:
                        playerManager.CurrentStatus.Hp += (int)_upValue;
                        return;
                    case StatusType.Attack:
                        playerManager.CurrentStatus.Attack += (int)_upValue;
                        return;
                    case StatusType.Defence:
                        playerManager.CurrentStatus.Defence += (int)_upValue;
                        return;
                    case StatusType.CoolTime:
                        playerManager.CurrentStatus.CoolTime += _upValue;
                        return;
                    case StatusType.Range:
                        playerManager.CurrentStatus.Range += _upValue;
                        return;
                    case StatusType.BulletSpeed:
                        playerManager.CurrentStatus.BulletSpeed += _upValue;
                        return;
                    case StatusType.EffectTime:
                        playerManager.CurrentStatus.EffectTime += _upValue;
                        return;
                    case StatusType.BulletNum:
                        playerManager.CurrentStatus.BulletNum += (int)_upValue;
                        return;
                    case StatusType.Penetration:
                        playerManager.CurrentStatus.Penetration += _upValue;
                        return;
                    case StatusType.MoveSpeed:
                        playerManager.CurrentStatus.MoveSpeed += _upValue;
                        return;
                    case StatusType.GrowthSpeed:
                        playerManager.CurrentStatus.GrowthSpeed += _upValue;
                        return;
                    case StatusType.Luck:
                        playerManager.CurrentStatus.Luck += _upValue;
                        return;
                    case StatusType.CriticalRate:
                        playerManager.CurrentStatus.CriticalRate += _upValue;
                        return;
                    case StatusType.CriticalDamageRate:
                        playerManager.CurrentStatus.CriticalDamageRate += _upValue;
                        return;
                    case StatusType.SoulAbsorption:
                        playerManager.CurrentStatus.SoulAbsorption += _upValue;
                        return;
                    case StatusType.SoulAcquisition:
                        playerManager.CurrentStatus.SoulAcquisition += _upValue;
                        return;
                }
            }
        }
    }

    public enum StatusType
    {
        Hp,
        [InspectorName("攻撃力")]　Attack,
        [InspectorName("防御力")]　Defence,
        [InspectorName("クールタイム減少率")]　CoolTime,
        [InspectorName("スキル範囲増加率")]　Range,
        [InspectorName("弾速増加率")]　BulletSpeed,
        [InspectorName("追加効果時間")]　EffectTime,
        [InspectorName("追加弾数")]　BulletNum,
        [InspectorName("貫通力")]　Penetration,
        [InspectorName("移動スピード")]　MoveSpeed,
        [InspectorName("成長速度")]　GrowthSpeed,
        [InspectorName("金運")]　Luck,
        [InspectorName("クリティカル率")]　CriticalRate,
        [InspectorName("クリティカルダメージ倍率")]　CriticalDamageRate,
        [InspectorName("ソウル吸収力")]　SoulAbsorption,
        [InspectorName("ソウル獲得率")]　SoulAcquisition
    }
}
