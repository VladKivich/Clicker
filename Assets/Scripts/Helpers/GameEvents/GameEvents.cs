using Helpers;

namespace Assets.Scripts.Helpers.GameEvents
{
    public abstract class GameEvent { }

    public class AddMoneyGameEvent : GameEvent
    {
        public readonly uint money;
        public AddMoneyGameEvent(uint money)
        {
            this.money = money;
        }
    }

    public class ModuleUpgradeGameEvent : GameEvent
    {
        public readonly int computerModule;
        public ModuleUpgradeGameEvent(ComputerModule module)
        {
            computerModule = (int)module;
        }
    }
}
