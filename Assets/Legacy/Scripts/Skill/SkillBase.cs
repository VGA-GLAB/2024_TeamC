using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// Skillの基底クラス
    /// スキルはレベルをもち、1～５までの値で上昇のみする
    /// 
    /// </summary>
    public abstract class SkillBase : MonoBehaviour
    {
        /// <summary> 技名 </summary>
        [SerializeField] protected string _skillName = "";
        /// <summary> スキルのレベル </summary>
        [SerializeField] protected SkillLevel _skillLevel = new();
        /// <summary> 攻撃力 </summary>
        [SerializeField] protected float _atk = 0;
        /// <summary> 攻撃範囲倍率 </summary>
        [SerializeField] protected float _attackAreaRate = 0;
        /// <summary> 技が次に発動するまでの時間 </summary>
        [SerializeField] protected float _coolDownTime = 0;
        /// <summary> 各レベルでのこの技のステータスを記したテーブル。データは最大で5LVまでデータは0から</summary>
        [SerializeField] protected SkillLevelData[] skillLevelDatas = new SkillLevelData[5];

        /// <summary>
        /// スキルの発動処理
        /// ここにスキルの内容を書く。レベルアップ時のステータス上昇処理は基底クラスで行うので技は現在の変数をもとに発動する
        /// </summary>
        public abstract void ActivateCurrentStatusSkill();

        /// <summary>
        /// ここにスキルを止める処理を書く。再起動時は再びActivateCurrentStatusSkillが呼ばれる
        /// </summary>
        public abstract void DeactivateCurrentStatusSkill();

        public void LevelUp()
        {
            SetCurrentLevelStatus();
        }

        /// <summary>
        /// ステータスの上昇処理
        /// </summary>
        private void SetCurrentLevelStatus()
        {
            _skillLevel.LevelUP();
            if (skillLevelDatas.Length <= _skillLevel.Level) return;
            _atk = skillLevelDatas[_skillLevel.Level - 1].Atk;  //インデックスとレベルの値がずれるため-1
            _attackAreaRate = skillLevelDatas[_skillLevel.Level - 1].AttackRate;
            _coolDownTime = skillLevelDatas[_skillLevel.Level - 1].CoolDownTime;
        }
    }

    /// <summary>
    /// スキルのレベルを管理するクラス
    /// 最大で5になる
    /// </summary>
    public class SkillLevel
    {
        private int _level = 1;
        const int MaxLevel = 5; //レベルの最大値
        public int Level => _level;

        public void LevelUP()
        {
            _level = Mathf.Max(_level + 1, MaxLevel);
        }
    }

    /// <summary>
    /// 各レベルが持つデータ
    /// </summary>
    [System.Serializable]
    public class SkillLevelData
    {
        //TODO　後にreadOnlyにしてデータを入れるとき以外は操作できないようにすること
        public  int Level = 1;
        public  float Atk = 1;
        public  float AttackRate = 1;
        public  float CoolDownTime = 1;
    }
}
