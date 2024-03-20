namespace SoulRunProject.Common
{
    /// <summary>
    /// 最初から持っているスキル。
    /// </summary>
    public class SoulBullet : SkillBase
    {
        public override void Fire()
        {
        }

        public override void Stop()
        {
        }

        public SoulBullet() : base(nameof(SoulBullet)) { }
    }
}
