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
    [System.Serializable]
    public class Status
    {
        [SerializeField] private int hp;
        [SerializeField] private int attack;
        [SerializeField] private int defence;
        [SerializeField] private float coolTime;
        [SerializeField] private float range;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float effectTime;
        [SerializeField] private int bulletNum;
        [SerializeField] private int penetration;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float growthSpeed;
        [SerializeField] private int luck;
    }
}