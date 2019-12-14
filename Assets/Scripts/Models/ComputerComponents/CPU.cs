using Assets.Scripts.Helpers;
using Helpers;

namespace Models
{
    public struct CPUModel : IComputerComponentModel
    {
        public float CoolDownPercentagePerStep { get; }
        public float HeatPercentagePerClick { get; }
        public uint Level { get; }

        public ComputerModule moduleType => ComputerModule.CPU;

        public CPUModel(float cooldownPercent, float heatPercentage, uint level)
        {
            CoolDownPercentagePerStep = cooldownPercent;
            HeatPercentagePerClick = heatPercentage;
            Level = level;
        }
    }

    public class CPU : IComputerComponent
    {
        public float CoolDownPercentagePerStep { get; private set; }
        public float HeatPercentagePerClick { get; private set; }
        public uint Level { get; private set; }

        public CPU(float cooldownPercent, float heatPercentage, uint componentLevel)
        {
            CoolDownPercentagePerStep = cooldownPercent;
            HeatPercentagePerClick = heatPercentage;
            Level = componentLevel;
        }

        /// <summary>
        /// Creates a model with base parameters(Level 1 Component)
        /// </summary>
        public CPU()
        {
            CoolDownPercentagePerStep = Constants.BASE_COMPONENT_CPU_COOLDOWN_PERCENTAGE_PER_STEP;
            HeatPercentagePerClick = Constants.BASE_COMPONENT_CPU_HEAT_PERCENTAGE_PER_CLICK;
            Level = Constants.BASE_COMPONENT_LEVEL;
        }

        public void UpdateComponent(IComputerComponentModel component)
        {
            if (component is CPUModel cpu)
            {
                CoolDownPercentagePerStep = cpu.CoolDownPercentagePerStep;
                HeatPercentagePerClick = cpu.HeatPercentagePerClick;
                Level = cpu.Level;
            }
        }

        public IComputerComponentModel GetComponentModel()
        {
            return new CPUModel(CoolDownPercentagePerStep, HeatPercentagePerClick, Level);
        }
    }
}

