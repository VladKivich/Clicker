using System;

namespace Models
{
    public class GameProgress
    {
        private int currentAmount;
        public event Action OnModelUpdate;

        public GameProgress()
        {
            currentAmount = 0;
        }

        public void AddMoney(int amount)
        {
            currentAmount += amount;
        }

        public bool CanSpendMoney(int amountToSpend)
        {
            if (currentAmount >= amountToSpend)
            {
                currentAmount -= amountToSpend;
                return true;
            }
            return false;
        }
    }
}

