using SoulRunProject.InGame;

namespace SoulRunProject.TakechiTest
{
    /// <summary>
    /// デバッグ用エネミークラス
    /// </summary>
    public class TestEnemy : FieldEntityController
    {
        public IEntityMover Mover => _mover;
        void Update()
        {
            _mover.OnUpdateMove(transform);
        }
    }
}