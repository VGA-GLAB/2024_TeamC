using UnityEngine;
using UniRx;
using UnityEngine.Serialization;

namespace SoulRun.Data
{

    [CreateAssetMenu(fileName = "PlayerMoneyData", menuName = "Player/Money Data", order = 1)]
    public class PlayerMoneyData : ScriptableObject
    {
        [SerializeField]IntReactiveProperty money = new(0);

        public IReadOnlyReactiveProperty<int> Money => money;

        public void SetMoney(int amount)
        {
            if (amount >= 0)
            {
                money.Value = amount;
            }
        }

        public int GetMoneyAmount()
        {
            return money.Value;
        }
    }

}