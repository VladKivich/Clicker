using Assets.Scripts.Helpers.GameEvents;
using Helpers;
using System;

namespace Assets.Scripts.Helpers
{
    public interface IGameEventSender
    {
        GEventType GetSendingGEventTypes { get; }

        void StartSendingEventType(GEventType eventType);

        void StopSendingEventType(GEventType eventType);

        event Action<GEventType, BaseGEvent> SendComplexGEvent;

        event Action<GEventType> SendSimpleGEvent;
    }
}
