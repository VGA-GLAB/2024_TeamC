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
        public int Hp { get; private set; }
        public int Attack { get; private set; }
        public int Defence { get; private set; }
        public float CoolTime { get; private set; }
        public float Range { get; private set; }
        public float BulletSpeed { get; private set; }
        public float EffectTime { get; private set; }
        public int BulletNum { get; private set; }
        public int Penetration { get; private set; }
        public float MoveSpeed { get; private set; }
        public float GrowthSpeed { get; private set; }
        public int Luck { get; private set; }
    }
}