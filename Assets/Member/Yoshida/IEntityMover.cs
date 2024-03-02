using SoulRunProject.SoulMixScene;

namespace SoulRun.InGame.Enemy
{
    /// <summary>
    /// Enemyの移動処理のインターフェース
    /// </summary>
    public interface IEntityMover
    {
        public void GetMoveStatus(Status status);
        public void Move();
    }
}
