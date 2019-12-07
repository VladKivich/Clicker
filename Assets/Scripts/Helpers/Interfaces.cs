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
