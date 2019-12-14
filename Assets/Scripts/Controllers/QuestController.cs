using Assets.Scripts.Helpers;
using Assets.Scripts.Helpers.GameEvents;
using Assets.Scripts.Helpers.GameQuests;
using BaseScripts;
using Helpers;
using Models;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class QuestController : BaseController, IGameEventHandler
    {
        private Dictionary<int, List<GameQuests>> quests;
        private GameProgress progress;
        private MessageController messageController;

        public QuestController()
        {
            Core.Instance.AddController<QuestController>(this);
            progress = Core.Instance.GetProgress;
        }

        public override void On()
        {
            base.On();
            messageController = Core.Instance.GetController<MessageController>();

            //TODO Remove Quests
            #region Temporary Quests

            quests = new Dictionary<int, List<GameQuests>>();
            {
                quests.Add((int)GameEventType.SingleClick, new List<GameQuests>());
                quests.Add((int)GameEventType.AutoClick, new List<GameQuests>());
                quests.Add((int)GameEventType.EarnMoney, new List<GameQuests>());
            };

            AddQuest(new ClicksQuest(15, GameEventType.SingleClick, "Make 15 clicks", 100));
            AddQuest(new ClicksQuest(15, GameEventType.AutoClick, "Make 3 autoclicks", 300));
            AddQuest(new MoneyQuest(599, "Earn 599 money", 500));

            #endregion
        }

        public void AddQuest(GameQuests quest)
        {
            var key = (int)quest.QuestEvent;
            if (quests.ContainsKey(key))
            {
                var questList = quests[key];
                if(questList.Count == 0)
                {
                    quests[key].Add(quest);
                    messageController.AddHandlerWatchingEvent(this, key);
                }
                else
                {
                    quests[key].Add(quest);
                }
            }
            else
            {
                quests.Add(key, new List<GameQuests>());
                quests[key].Add(quest);
                messageController.AddHandlerWatchingEvent(this, key);
            }
            quest.AddQuestCompletedAction(QuestCompleteNotification);
        }

        private void RemoveQuest(GameQuests quest)
        {
            quest.RemoveQuestCompletedAction();
            var key = (int)quest.QuestEvent;
            if (quests.ContainsKey(key))
            {
                var questList = quests[key];
                if (questList.Count == 1)
                {
                    questList.Remove(quest);
                    messageController.RemoveHandlerWatchingEvent(this, key);
                }
                else
                {
                    questList.Remove(quest);
                }
            }
        }

        private void QuestCompleteNotification(GameQuests quest)
        {
            Debug.Log($"Quest is complete. Quest task was {quest.QuestDescription} " +
                $"Your reward = {quest.RewardMoney}");

            progress.AddMoney(quest.RewardMoney);
            GetMessage(GameEventType.EarnMoney, new AddMoneyGameEvent(quest.RewardMoney));
            RemoveQuest(quest);
        }

        public void GetMessage(GameEventType eventType)
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

        public void GetMessage(GameEventType eventType, GameEvent eventData)
        {
            if (eventData == null)
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
                            if (eventData is AddMoneyGameEvent e)
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
