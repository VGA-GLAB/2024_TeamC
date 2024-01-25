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

namespace SoulRunProject.SoulMixScene
{
    public class Status
    {
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int CoolTime { get; set; }
        public int Range { get; set; }
        public int BulletSpeed { get; set; }
        public int EffectTime { get; set; }
        public int BulletNum { get; set; }
        public int Penetration { get; set; }
        public int MoveSpeed { get; set; }
        public int GrowthSpeed { get; set; }
        public int Luck { get; set; }
    }
}