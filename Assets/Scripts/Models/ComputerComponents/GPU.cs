using Assets.Scripts.Helpers;
using Helpers;

namespace Models
{
    public struct GPUModel: IComputerComponentModel
    {
        public int AutoClickRewardPerSecond { get; }
        public float FillBarPercentagePerClick { get; }
        public uint Level { get; }

        public ComputerModule moduleType => ComputerModule.Monitor;

        public GPUModel(int autoClickReward, float fillbarPercent, uint level)
        {
            AutoClickRewardPerSecond = autoClickReward;
            FillBarPercentagePerClick = fillbarPercent;
            Level = level;
        }
    }

    public sealed class GPU : IComputerComponent
    {
        public int AutoClickRewardPerSecond { get; private set; }
        public float FillBarPercentagePerClick { get; private set; }
        public uint Level { get; private set; }

        public GPU(int autoReward, float fillBarPerClick, uint componentLevel)
        {
            AutoClickRewardPerSecond = autoReward;
            FillBarPercentagePerClick = fillBarPerClick;
            Level = componentLevel;
        }

        /// <summary>
        /// Creates a model with base parameters(Level 1 Component)
        /// </summary>
        public GPU()
        {
            AutoClickRewardPerSecond = Constants.BASE_COMPONENT_GPU_AUTOCLICK_REWARD_PER_SECOND;
            FillBarPercentagePerClick = Constants.BASE_COMPONENT_GPU_FILLBAR_PERCENTAGE_PER_CLICK;
            Level = Constants.BASE_COMPONENT_LEVEL;
        }

        public void UpdateComponent(IComputerComponentModel component)
        {
            if (component is GPUModel gpu)
            {
                AutoClickRewardPerSecond = gpu.AutoClickRewardPerSecond;
                FillBarPercentagePerClick = gpu.FillBarPercentagePerClick;
                Level = gpu.Level;
            }
        }

        public IComputerComponentModel GetComponentModel()
        {
            return new GPUModel(AutoClickRewardPerSecond, FillBarPercentagePerClick, Level);
        }
    }
}

