using SoulRunProject.SoulMixScene;

namespace SoulRunProject.InGame
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
