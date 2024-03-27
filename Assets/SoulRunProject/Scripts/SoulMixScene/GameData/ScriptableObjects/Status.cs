/*
HP	値	100
攻撃力	値	1	各スキルにこの値が加算
防御力	値	0	ダメージからこの値が引かれる
クールタイム減少率	割合	1	クールタイムにこの値が乗算。値が小さくなるほどクールタイムが短くなる
スキル範囲増加率	割合	1	この値が乗算
弾速増加率	割合	1	この値が乗算
追加効果時間(秒)	値	0	スキルの効果時間にこの値が追加される
追加弾数	値	0	1増えるごとに弾が1増える
貫通力	値	0	1増えるごとに敵を1体貫通できる。元から貫通するものもある。
移動スピード	割合	1	基本スピードに乗算される(縦横共通)
成長速度	割合	1	経験値を獲得したときに獲得した値にこの割合が乗算される
金運	割合	1	ドロップオブジェクト数が増加
クリティカル率	割合	0.1
クリティカルダメージ倍率	割合	2	基本クリティカルはダメージ2倍
ソウル吸収力	割合	1	上昇で吸収範囲が上昇
ソウル獲得率	割合	1	敵のドロップ率と掛け合わせて100％を超えるとドロップ率が上昇
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
            return Instantiate(this);
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
            set => _coolTime = Mathf.Clamp(value, 0.00f, 1.00f); // 0.00から1.00までの範囲に制限
        }

        // スキル範囲増加率
        [SerializeField] private float _range;

        public float Range
        {
            get => _range;
            set => _range = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }

        // 弾速増加率
        [SerializeField] private float _bulletSpeed;

        public float BulletSpeed
        {
            get => _bulletSpeed;
            set => _bulletSpeed = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }

        // 効果時間(秒)
        [SerializeField] private float _effectTime;

        public float EffectTime
        {
            get => _effectTime;
            set => _effectTime = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }

        // 弾数
        [SerializeField] private int _bulletNum;

        public int BulletNum
        {
            get => _bulletNum;
            set => _bulletNum = Mathf.Max(value, 0); // 弾数は0未満にならないように制限
        }

        // 貫通力
        [SerializeField] private float _penetration;

        public float Penetration
        {
            get => _penetration;
            set => _penetration = Mathf.Max(value, 0.00f); // 貫通力は0未満にならないように制限
        }

        // 移動スピード
        [SerializeField] private float _moveSpeed;

        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }

        // 成長速度
        [SerializeField] private float _growthSpeed;

        public float GrowthSpeed
        {
            get => _growthSpeed;
            set => _growthSpeed = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }

        // 運
        [SerializeField] private float _luck;

        public float Luck
        {
            get => _luck;
            set => _luck = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }


        // クリティカル率
        [SerializeField] private float _criticalRate;

        public float CriticalRate
        {
            get => _criticalRate;
            set => _criticalRate = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }

        // クリティカルダメージ倍率
        [SerializeField] private float _criticalDamageRate;

        public float CriticalDamageRate
        {
            get => _criticalDamageRate;
            set => _criticalDamageRate = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }

        // ソウル吸収力
        [SerializeField] private float _soulAbsorption;

        public float SoulAbsorption
        {
            get => _soulAbsorption;
            set => _soulAbsorption = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }

        // ソウル獲得率
        [SerializeField] private float _soulAcquisition;

        public float SoulAcquisition
        {
            get => _soulAcquisition;
            set => _soulAcquisition = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
    }
}