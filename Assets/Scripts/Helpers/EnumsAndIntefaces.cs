namespace Helpers
{
    public interface IQuestEventSender
    {
        void StartSendingEventType(QuestEventType eventType);

        void StopSendingEventType(QuestEventType eventType);

        void Send(QuestEventType eventType);
    }

    public enum QuestEventType
    {
        SingleClick = 1,
        AutoClick = 2,
        EarnMoney = 3,
        QuestComplete = 4,
        ComponentUpgrade = 5
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
