namespace SoulRun.InGame
{
    public interface IEntityAttacker
    {
        public void GetAttackStatus(int attack, float coolTime, float range);
        public void Attack();
    }
}
