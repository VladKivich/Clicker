using Assets.Scripts.Helpers;
using Assets.Scripts.Helpers.GameEvents;
using BaseScripts;
using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class MessageController : BaseController
    {
        private Dictionary<int, List<IGameEventSender>> sendersList;
        private Dictionary<int, List<IGameEventHandler>> handlersList;
        private List<IGameEventSender> senders;

        private MonoBehaviour mb;

        public MessageController()
        {
            Core.Instance.AddController<MessageController>(this);
            mb = Core.Instance.GetMB;
            Initialize();
        }

        private void Initialize()
        {
            handlersList = new Dictionary<int, List<IGameEventHandler>>();
            sendersList = new Dictionary<int, List<IGameEventSender>>();

            senders = Core.Instance.GetGameEventsSenders();
            foreach (var sender in senders)
            {
                AddToDictionary(sender);
            }
        }

        public override void On()
        {
            base.On();
            foreach (var sender in senders)
            {
                sender.SendSimpleGameEventMessage += GetSimpleGameEventMessage;
                sender.SendComplexGameEventMessage += GetComplexGameEventMessage;
            }
        }

        public override void Off()
        {
            base.Off();
            foreach (var sender in senders)
            {
                sender.SendSimpleGameEventMessage -= GetSimpleGameEventMessage;
                sender.SendComplexGameEventMessage -= GetComplexGameEventMessage;
            }
        }

        #region Async Methods

        private void GetSimpleGameEventMessage(GameEventType type)
        {
            var key = (int)type;
            if (handlersList.ContainsKey(key) && handlersList[key].Count > 0)
            {
                foreach (var handler in handlersList[key])
                {
                    handler.GetMessage(type);
                }
            }
        }

        private void GetComplexGameEventMessage(GameEventType type, GameEvent eventData)
        {
            var key = (int)type;
            if (handlersList.ContainsKey(key) && handlersList[key].Count > 0)
            {
                foreach (var handler in handlersList[key])
                {
                    handler.GetMessage(type, eventData);
                }
            }
        }

        public void AddHandlerWatchingEvent(IGameEventHandler handler, int eventKey)
        {
            if (handlersList.ContainsKey(eventKey))
            {
                var handlers = handlersList[eventKey];
                if (handlers.Count == 0)
                {
                    handlers.Add(handler);
                    UpdateSendersActivity(eventKey, true);
                }
                else if (!handlers.Contains(handler))
                {
                    handlers.Add(handler);
                }
            }
        }

        public void RemoveHandlerWatchingEvent(IGameEventHandler handler, int eventKey)
        {
            if (handlersList.ContainsKey(eventKey))
            {
                var handlers = handlersList[eventKey];
                if (handlers.Count == 1)
                {
                    if (handlers.Remove(handler))
                    {
                        UpdateSendersActivity(eventKey, false);
                    }
                }
                handlers.Remove(handler);
            }
        }

        private void UpdateSendersActivity(int eventKey, bool Add)
        {
            if (sendersList.ContainsKey(eventKey))
            {
                foreach (var sender in sendersList[eventKey])
                {
                    switch (Add)
                    {
                        case true:
                            sender.StartSendingEventType((GameEventType)eventKey);
                            break;
                        case false:
                            sender.StopSendingEventType((GameEventType)eventKey);
                            break;
                    }
                }
            }
        }

        #endregion

        private void AddToDictionary(IGameEventSender sender)
        {
            var senderEventTypes = sender.GetGameEventTypes.GetType();
            var bitNumber = Enum.GetNames(senderEventTypes).Length;
            var number = (int)sender.GetGameEventTypes;
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
                    if (!handlersList.ContainsKey(key))
                    {
                        handlersList.Add(key, new List<IGameEventHandler>());
                    }
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
    }
}
