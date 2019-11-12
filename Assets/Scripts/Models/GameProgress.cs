using System;

namespace Models
{
    public interface IComputerComponents
    {
        float CPUCoolDownValue { get; }
        int GPUAutoClickReward { get; }
    }

    public class GameProgress : IComputerComponents
    {
        public int currentAmount { get; private set; }
        public event Action OnModelUpdate;

        //TO DO: Система улучшений
        public float CPUCoolDownValue { get; private set; } = 0.05f;
        public int GPUAutoClickReward { get; private set; } = 1;

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

