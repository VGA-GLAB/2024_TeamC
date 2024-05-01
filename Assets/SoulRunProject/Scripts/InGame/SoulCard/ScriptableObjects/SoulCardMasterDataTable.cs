using System;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    /// <summary>
    /// ソウルスキルタイプ
    /// </summary>
    public enum SoulSkillType
    {
        SoulFrame = 0,
    }
    
    /// <summary>
    /// ソウルカードの特性タイプ
    /// </summary>
    public enum CharacteristicType
    {
        None = 0,
        Attack = 1,
        CoolTimeReduction = 2,
    }
    
    /// <summary>
    /// ソウルカードの経験値テーブルタイプ
    /// </summary>
    public enum SoulCardExperienceTableType
    {
        Basic = 0,
    }
    
    /// <summary>
    /// ソウルカードの特性タイプ
    /// </summary>
    public enum SoulCardStatusTableType
    {
        Basic = 0,
    }
    
    [ExcelAsset]
    public class SoulCardMasterDataTable : ScriptableObject
    {
        public List<SoulCardMasterData> DataTable;
    }

    [Serializable]
    public class SoulCardMasterData
    {
        [SerializeField , CustomLabel("カードID")] private int _cardID;
        [SerializeField, CustomLabel("アイコン画像名")] private string _imageName;
        [SerializeField, CustomLabel("カード名")] private string _soulName;
        [SerializeField, CustomLabel("説明文")] private string _description;
        [SerializeField, CustomLabel("ソウルスキル")] private SoulSkillType _soulSkillType;
        [SerializeField, CustomLabel("固有特性")] private CharacteristicType _defaultCharacteristicType;
        [SerializeField, CustomLabel("経験値テーブルタイプ")] private SoulCardExperienceTableType _experienceTableType;
        [SerializeField, CustomLabel("ステータス上昇テーブルタイプ")] private SoulCardStatusTableType _statusTableType;
        [SerializeField , CustomLabel("HP") ] private float _hp;
        [SerializeField, CustomLabel("攻撃力")] private int _attack;
        [SerializeField, CustomLabel("防御力")] private int _defence;
        [SerializeField, CustomLabel("クールタイム減少率")] private float _coolTime;
        [SerializeField, CustomLabel("スキル範囲増加率")] private float _skillSize;
        [SerializeField, CustomLabel("弾速増加率")] private float _bulletSpeed;
        [SerializeField, CustomLabel("効果時間(秒)")] private float _effectTime;
        [SerializeField, CustomLabel("弾数")] private int _bulletNum;
        [SerializeField, CustomLabel("貫通力")] private float _penetration;
        [SerializeField, CustomLabel("移動スピード")] private float _moveSpeed;
        [SerializeField, CustomLabel("成長速度")] private float _growthSpeed;
        [SerializeField, CustomLabel("運")] private float _luck;
        [SerializeField, CustomLabel("クリティカル率")] private float _criticalRate;
        [SerializeField, CustomLabel("クリティカルダメージ倍率")] private float _criticalDamageRate;
        [SerializeField, CustomLabel("ソウル吸収力")] private float _soulAbsorption;
        [SerializeField, CustomLabel("ソウル獲得率")] private float _soulAcquisition;
        
        public int CardID => _cardID;
        public string ImageName => _imageName;
        public string SoulName => _soulName;
        public string Description => _description;
        public SoulSkillType SoulSkillType => _soulSkillType;

        public SoulCardExperienceTableType ExperienceTableType => _experienceTableType;

        public SoulCardStatusTableType StatusTableType => _statusTableType;

        public Status BaseStatus =>
            new Status(_hp, _attack, _defence, _coolTime, _skillSize, _bulletSpeed, _effectTime, _bulletNum, _penetration, _moveSpeed, _growthSpeed, _luck, _criticalRate, _criticalDamageRate, _soulAbsorption, _soulAcquisition);

        public CharacteristicType DefaultCharacteristicType => _defaultCharacteristicType;
    }
}