using Assets.Scripts.Helpers;
using Helpers;

namespace Models
{
    public struct MonitorModel : IComputerComponentModel
    {
        public uint MoneyForSingleClick { get; }
        public uint Level { get; }

        public MonitorModel(uint moneyForClick, uint level)
        {
            MoneyForSingleClick = moneyForClick;
            Level = level;
        }
    }

    public class Monitor : IComputerComponent
    {
        public uint MoneyForSingleClick { get; private set; }
        public uint Level { get; private set; }

        public Monitor(uint money, uint componentLevel)
        {
            MoneyForSingleClick = money;
            Level = componentLevel;
        }

        /// <summary>
        /// Creates a model with base parameters(Level 1 Component)
        /// </summary>
        public Monitor()
        {
            Level = Constants.BASE_COMPONENT_LEVEL;
            MoneyForSingleClick = Constants.BASE_COMPONENT_MONEY_FOR_SINGLE_CLICK;
        }

        public void UpdateComponent(IComputerComponentModel component)
        {
            if (component is MonitorModel monitor)
            {
                MoneyForSingleClick = monitor.MoneyForSingleClick;
                Level = monitor.Level;
            }
        }

        public IComputerComponentModel GetComponentModel()
        {
            return new MonitorModel(MoneyForSingleClick, Level);
        }
    }
}
