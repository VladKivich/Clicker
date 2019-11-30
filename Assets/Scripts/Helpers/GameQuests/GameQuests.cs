using Helpers;
using System;

namespace Assets.Scripts.Helpers.GameQuests
{
    public abstract class GameQuests
    {
        public readonly GEventType QuestEvent;

        public readonly string QuestDescription;

        public readonly int RewardMoney;

        public readonly DateTime ExpirationDate;

        private event Action<GameQuests> questCompletedEvent;

        public GameQuests(GEventType eventType, string descript, int reward, DateTime time)
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

        public ClicksQuest(int clicks, GEventType click, string description, int reward)
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
        public readonly int AmountToEarn;

        public int currentAmountToEarn;

        public MoneyQuest(int amount, string description, int reward)
            : base(GEventType.EarnMoney, description, reward, DateTime.Now)
        {
            AmountToEarn = amount;
            currentAmountToEarn = (int)amount;
        }

        public void AddMoney(int money)
        {
            currentAmountToEarn -= money;
            if (currentAmountToEarn <= 0)
            {
                StartQuestCompletedActions();
            }
        }
    }
}
