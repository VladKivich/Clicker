using System;

namespace Helpers
{
    [Flags]
    public enum GameEventType:int
    {
        SingleClick = 1,
        AutoClick = 2,
        EarnMoney = 4,
        QuestComplete = 8,
        ComponentUpgrade = 16
    }

    public enum ComputerModule
    {
        CPU = 1,
        GPU = 2,
        Monitor = 3,
        CoolingSystem = 4,
    }

    public enum ClickerButtonStates
    {
        SingleClick,
        ButtonIsPressed
    }

    public enum ClickerStates
    {
        ActiveClicks,
        AutoClicks,
        Pause
    }
}
