using UniRx;
namespace SoulRun
{
    public class Wallet
    {
        private readonly IntReactiveProperty _money = new (0);

        public IReadOnlyReactiveProperty<int> Money => _money;

        public void AddMoney(int amount)
        {
            if (amount > 0)
            {
                _money.Value += amount;
            }
        }

        public void DeductMoney(int amount)
        {
            if (amount > 0 && _money.Value >= amount)
            {
                _money.Value -= amount;
            }
        }

        public int GetMoneyAmount()
        {
            return _money.Value;
        }
    }

}