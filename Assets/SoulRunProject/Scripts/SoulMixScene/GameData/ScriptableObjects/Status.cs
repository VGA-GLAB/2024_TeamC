/*
- HP
- 攻撃力
- 防御力
- クールタイム
- 範囲
- 弾速
- 効果時間
- 弾数
- 貫通力
- 移動スピード(前と横両方)
- 成長速度(経験値ジェム取得時のEXP上昇率増加)
- 運(クリティカル、お金のドロップ数上昇、ソウルドロップ率UP)
 */

using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunProject.SoulMixScene
{
    [CreateAssetMenu(fileName = "Status", menuName = "SoulRunProject/Status")]
    public class Status : ScriptableObject
    {
        // Hp
        [SerializeField] private int _hp;

        public Status Copy()
        {
            return (Status)MemberwiseClone();
        }

        public int Hp
        {
            get => _hp;
            set => _hp = Mathf.Max(value, 0); // HPは0未満にならないように制限
        }

        // 攻撃力
        [SerializeField] private int _attack;

        public int Attack
        {
            get => _attack;
            set => _attack = value;
        }

        // 防御力
        [SerializeField] private int _defence;

        public int Defence
        {
            get => _defence;
            set => _defence = value;
        }

        // クールタイム減少率
        [SerializeField] private float _coolTime;

        public float CoolTime
        {
            get => _coolTime;
            set => _coolTime = Mathf.Max(value, 0); // クールタイムは0未満にならないように制限
        }

        // 範囲
        [SerializeField] private float _range;

        public float Range
        {
            get => _range;
            set => _range = value;
        }

        // 弾速
        [SerializeField] private float _bulletSpeed;

        public float BulletSpeed
        {
            get => _bulletSpeed;
            set => _bulletSpeed = value;
        }

        // 効果時間
        [SerializeField] private float _effectTime;

        public float EffectTime
        {
            get => _effectTime;
            set => _effectTime = value;
        }

        // 弾数
        [SerializeField] private int _bulletNum;

        public int BulletNum
        {
            get => _bulletNum;
            set => _bulletNum = Mathf.Max(value, 0); // 弾数は0未満にならないように制限
        }

        // 貫通力
        [SerializeField] private int _penetration;

        public int Penetration
        {
            get => _penetration;
            set => _penetration = value;
        }

        // 移動スピード
        [SerializeField] private float _moveSpeed;

        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        // 成長速度
        [SerializeField] private float _growthSpeed;

        public float GrowthSpeed
        {
            get => _growthSpeed;
            set => _growthSpeed = value;
        }

        // 運
        [SerializeField] private int _luck;

        public int Luck
        {
            get => _luck;
            set => _luck = value;
        }
    }
}