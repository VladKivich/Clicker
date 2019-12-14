using Assets.Scripts.Helpers.GameEvents;
using Helpers;
using System;

namespace Assets.Scripts.Helpers
{
    #region GameEvent Interfaces

    //TODO Add to Core
    public interface IMessageSystem
    {
        void AddHandlerWatchingEvent(IGameEventHandler handler, int eventKey);
        void RemoveHandlerWatchingEvent(IGameEventHandler handler, int eventKey);
    }

    public interface IGameEventSender
    {
        GameEventType GetGameEventTypes { get; }

        void StartSendingEventType(GameEventType eventType);

        void StopSendingEventType(GameEventType eventType);

        event Action<GameEventType, GameEvent> SendComplexGameEventMessage;
        event Action<GameEventType> SendSimpleGameEventMessage;
    }

    public interface IGameEventHandler
    {
        void GetMessage(GameEventType eventType);

        void GetMessage(GameEventType eventType, GameEvent eventData);
    }

    #endregion

    public interface IComponentsPC
    {
        float CPUCoolDownPercentagePerStep { get; }
        float CPUHeatPercentagePerClick { get; }
        int GPUAutoClickRewardPerSecond { get; }
        float GPUFillBarPercentagePerClick { get; }
        uint MoneyForSingleClick { get; }
    }

    public interface IComputerComponent
    {
        uint Level { get; }
        void UpdateComponent(IComputerComponentModel component);
        IComputerComponentModel GetComponentModel();
    }

    public interface IComputerComponentModel
    {
        uint Level { get; }
        ComputerModule moduleType { get; }
    }

    public interface IShopItem
    {
        string Description { get; }
        uint Cost { get; }
        IComputerComponent GetTypeOfShopItem();
        uint GetShopItemLevel();
        void UpdateItemLevel();
        IComputerComponentModel GetShopItemModel();
    }

    public interface IModel
    {
        event Action OnModelUpdate;
    }
}
