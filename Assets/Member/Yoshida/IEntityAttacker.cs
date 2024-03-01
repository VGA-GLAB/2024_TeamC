using SoulRunProject.SoulMixScene;

namespace SoulRun.InGame.Enemy
{
    /// <summary>
    /// Enemyの攻撃処理のインターフェース
    /// </summary>
    public interface IEntityAttacker
    {
        public void GetAttackStatus(Status status);
        public void Attack();
    }
}
