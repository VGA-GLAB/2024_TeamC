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

namespace SoulRunProject.SoulMixScene
{
    [CreateAssetMenu(fileName = "Status", menuName = "SoulRunProject/Status")]
    public class Status : ScriptableObject
    {
        // Hp
        [SerializeField] private int hp;

        public int Hp
        {
            get => hp;
            set => hp = Mathf.Max(value, 0); // HPは0未満にならないように制限
        }

        // 攻撃力
        [SerializeField] private int attack;

        public int Attack
        {
            get => attack;
            set => attack = value;
        }

        // 防御力
        [SerializeField] private int defence;

        public int Defence
        {
            get => defence;
            set => defence = value;
        }

        // クールタイム
        [SerializeField] private float coolTime;

        public float CoolTime
        {
            get => coolTime;
            set => coolTime = Mathf.Max(value, 0); // クールタイムは0未満にならないように制限
        }

        // 範囲
        [SerializeField] private float range;

        public float Range
        {
            get => range;
            set => range = value;
        }

        // 弾速
        [SerializeField] private float bulletSpeed;

        public float BulletSpeed
        {
            get => bulletSpeed;
            set => bulletSpeed = value;
        }

        // 効果時間
        [SerializeField] private float effectTime;

        public float EffectTime
        {
            get => effectTime;
            set => effectTime = value;
        }

        // 弾数
        [SerializeField] private int bulletNum;

        public int BulletNum
        {
            get => bulletNum;
            set => bulletNum = Mathf.Max(value, 0); // 弾数は0未満にならないように制限
        }

        // 貫通力
        [SerializeField] private int penetration;

        public int Penetration
        {
            get => penetration;
            set => penetration = value;
        }

        // 移動スピード
        [SerializeField] private float moveSpeed;

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        // 成長速度
        [SerializeField] private float growthSpeed;

        public float GrowthSpeed
        {
            get => growthSpeed;
            set => growthSpeed = value;
        }

        // 運
        [SerializeField] private int luck;

        public int Luck
        {
            get => luck;
            set => luck = value;
        }
    }
}