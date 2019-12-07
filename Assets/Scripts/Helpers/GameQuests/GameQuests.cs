using Helpers;
using System;

namespace Assets.Scripts.Helpers.GameQuests
{
    public abstract class GameQuests
    {
        public readonly GEventType QuestEvent;

        public readonly string QuestDescription;

        public readonly uint RewardMoney;

        public readonly DateTime ExpirationDate;

        private event Action<GameQuests> questCompletedEvent;

        public GameQuests(GEventType eventType, string descript, uint reward, DateTime time)
        {
            QuestEvent = eventType;
            QuestDescription = descript;
            RewardMoney = reward;
            ExpirationDate = time;
        }

        public void AddQuestCompletedAction(Action<GameQuests> action)
        {
            questCompletedEvent += action;
        }

        public void RemoveQuestCompletedAction(Action<GameQuests> action)
        {
            questCompletedEvent -= action;
        }

        protected void StartQuestCompletedActions()
        {
            questCompletedEvent?.Invoke(this);
        }
    }

    public class ClicksQuest : GameQuests
    {
        public readonly int numberOfClicks;

        public int RemainClicks { get; private set; }

        public ClicksQuest(int clicks, GEventType click, string description, uint reward)
            : base(click, description, reward, DateTime.Now)
        {
            numberOfClicks = clicks;
            RemainClicks = (int)clicks;
        }

        public void CheckNewClick()
        {
            RemainClicks--;
            if (RemainClicks <= 0)
            {
                StartQuestCompletedActions();
            }
        }
    }

    public class MoneyQuest : GameQuests
    {
        public readonly uint AmountToEarn;

        public uint currentAmountToEarn;

        public MoneyQuest(uint amount, string description, uint reward)
            : base(GEventType.EarnMoney, description, reward, DateTime.Now)
        {
            AmountToEarn = amount;
            currentAmountToEarn = amount;
        }

        public void AddMoney(uint money)
        {
            currentAmountToEarn -= money;
            if (currentAmountToEarn <= 0)
            {
                StartQuestCompletedActions();
            }
        }
    }
}
