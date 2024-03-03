using SoulRunProject.SoulMixScene;

namespace SoulRunProject.InGame
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
