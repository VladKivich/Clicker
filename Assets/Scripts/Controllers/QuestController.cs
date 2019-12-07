using Assets.Scripts.Helpers;
using Assets.Scripts.Helpers.GameEvents;
using Assets.Scripts.Helpers.GameQuests;
using BaseScripts;
using Helpers;
using Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class QuestController : BaseController
    {
        private Dictionary<int, List<IGameEventSender>> sendersList;
        private Dictionary<int, List<GameQuests>> quests;
        private GEventType requiredGEventTypes;
        private GameProgress progress;

        public QuestController()
        {
            progress = Core.Instance.GetProgress;
            Core.Instance.AddController<QuestController>(this);
            InitializeSendersList();
            quests = new Dictionary<int, List<GameQuests>>();
            {
                quests.Add((int)GEventType.SingleClick, new List<GameQuests>());
                quests[(int)GEventType.SingleClick].Add(new ClicksQuest(15, GEventType.SingleClick,
                    "Make 15 clicks", 100));
                quests.Add((int)GEventType.AutoClick, new List<GameQuests>());
                quests[(int)GEventType.AutoClick].Add(new ClicksQuest(3, GEventType.AutoClick,
                    "Make 3 autoclicks", 300));
                quests.Add((int)GEventType.EarnMoney, new List<GameQuests>());
                quests[(int)GEventType.EarnMoney].Add(new MoneyQuest(599, "Earn 599 money", 500));
            };

            foreach (var key in quests.Keys)
            {
                SetRequiredGameEventTypes((GEventType)key);
                foreach (var quest in quests[key])
                {
                    quest.AddQuestCompletedAction(QuestCompleteNotification);
                }
            }
        }

        private void SetRequiredGameEventTypes(GEventType type)
        {
            if (!requiredGEventTypes.HasFlag(type))
            {
                requiredGEventTypes |= type;
                var key = (int)type;
                if (sendersList.ContainsKey(key) && sendersList[key].Count > 0)
                {
                    foreach (var sender in sendersList[(int)type])
                    {
                        sender.StartSendingEventType(type);
                    }
                }
            }
        }

        private void RemoveGameEventTypeNotification(GEventType type)
        {
            if (requiredGEventTypes.HasFlag(type))
            {
                requiredGEventTypes ^= type;
                foreach (var sender in sendersList[(int)type])
                {
                    sender.StopSendingEventType(type);
                }
            }
        }

        private void InitializeSendersList()
        {
            var sendersCollection = Core.Instance.GetQuestsEventsSenders();
            sendersList = new Dictionary<int, List<IGameEventSender>>();
            foreach (var sender in sendersCollection)
            {
                sender.SendSimpleGEvent += GetGEventSimpleNotification;
                sender.SendComplexGEvent += GetGEventComplexNotification;
                AddSenderToDictionary(sender);
            }
        }

        private void AddSenderToDictionary(IGameEventSender sender)
        {
            var senderEventTypes = sender.GetSendingGEventTypes.GetType();
            var bitNumber = Enum.GetNames(senderEventTypes).Length;
            var number = (int)sender.GetSendingGEventTypes;
            var bitsInByte = 8;
            var rightShift = (sizeof(int) * bitsInByte) - 1;
            var leftShift = rightShift;
            for (int i = 0; i < bitNumber; i++)
            {
                var currentBit = number << leftShift;
                currentBit >>= rightShift;
                if (currentBit != 0)
                {
                    var key = (int)Math.Pow(2, i);
                    SetKey(key, sender);
                }
                leftShift--;
            }
        }

        private void SetKey(int key, IGameEventSender sender)
        {
            if (!sendersList.ContainsKey(key))
            {
                sendersList.Add(key, new List<IGameEventSender>());
                sendersList[key].Add(sender);
            }
            else
            {
                if (!sendersList[key].Contains(sender))
                {
                    sendersList[key].Add(sender);
                }
            }
        }

        private void QuestCompleteNotification(GameQuests quest)
        {
            Debug.Log($"Quest is complete. Quest task was {quest.QuestDescription} " +
                $"Your reward = {quest.RewardMoney}");
            quest.RemoveQuestCompletedAction(QuestCompleteNotification);
            progress.AddMoney(quest.RewardMoney);
            GetGEventComplexNotification(GEventType.EarnMoney, new AddMoneyGEvent(quest.RewardMoney));
            var questType = quest.QuestEvent;
            quests[(int)questType].Remove(quest);
            if (quests[(int)questType].Count == 0)
            {
                RemoveGameEventTypeNotification(questType);
                requiredGEventTypes ^= questType;
            }
        }

        private void GetGEventSimpleNotification(GEventType eventType)
        {
            var key = (int)eventType;
            if (quests[key].Count != 0)
            {
                var currentQuests = quests[key];
                for (int i = 0; i < currentQuests.Count; i++)
                {
                    if (currentQuests[i] is ClicksQuest q)
                    {
                        q.CheckNewClick();
                        Debug.Log($"Clicks {eventType}");
                    }
                }
            }
        }

        private void GetGEventComplexNotification(GEventType eventType, BaseGEvent eventArg)
        {
            if (eventArg == null)
            {
                return;
            }
            var key = (int)eventType;
            if (quests[key].Count != 0)
            {
                var currentQuests = quests[key];
                for (int i = 0; i < currentQuests.Count; i++)
                {
                    switch (currentQuests[i])
                    {
                        case MoneyQuest q:
                            if (eventArg is AddMoneyGEvent e)
                            {
                                q.AddMoney(e.money);
                            }
                            break;
                    }
                }
            }
        }
    }
}
