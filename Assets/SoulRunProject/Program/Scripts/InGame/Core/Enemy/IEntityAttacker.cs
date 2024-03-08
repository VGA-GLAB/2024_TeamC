using SoulRunProject.SoulMixScene;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Enemyの攻撃処理のインターフェース
    /// </summary>
    public interface IEntityAttacker
    {
        public void GetAttackStatus(Status status);
        /// <summary>
        /// 起動時に一度のみ呼ばれる
        /// </summary>
        public void OnStart();
        /// <summary>
        /// Updateで呼ばれる攻撃処理
        /// </summary>
        public void OnUpdateAttack();
        public void Stop();
    }
}
