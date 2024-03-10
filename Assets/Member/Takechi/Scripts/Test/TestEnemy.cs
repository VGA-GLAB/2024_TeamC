using SoulRunProject.Common;
using SoulRunProject.InGame;

namespace SoulRunProject.TakechiTest
{
    public class TestEnemy : FieldEntityController
    {
        public IPlayerReference PlayerReference { get; set; }
        void Update()
        {
            _mover?.OnUpdateMove(transform, PlayerReference.Player);
        }
    }
}
