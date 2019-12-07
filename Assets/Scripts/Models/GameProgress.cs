using Assets.Scripts.Helpers;
using System;
using System.Collections.Generic;

namespace Models
{
    public class GameProgress : IComponentsPC, IModel
    {
        public uint currentAmount { get; private set; }
        public event Action OnModelUpdate;

        private GPU gpu;
        private CPU cpu;
        private Monitor monitor;

        private Dictionary<Type, IComputerComponent> components;

        #region ComponentsSpecifications

        public float CPUCoolDownPercentagePerStep => cpu.CoolDownPercentagePerStep;
        public float CPUHeatPercentagePerClick => cpu.HeatPercentagePerClick;
        public int GPUAutoClickRewardPerSecond => gpu.AutoClickRewardPerSecond;
        public float GPUFillBarPercentagePerClick => gpu.FillBarPercentagePerClick;
        public uint MoneyForSingleClick => monitor.MoneyForSingleClick;

        #endregion

        public GameProgress()
        {
            currentAmount = 0;
            gpu = new GPU();
            cpu = new CPU();
            monitor = new Monitor();

            components = new Dictionary<Type, IComputerComponent>()
            {
                {typeof(GPU), gpu},
                {typeof(CPU), cpu},
                {typeof(Monitor), monitor}
            };
        }

        public void AddMoneyForClick()
        {
            currentAmount += MoneyForSingleClick;
            OnModelUpdate?.Invoke();
        }

        public void AddMoneyForAutoClick(uint money)
        {
            currentAmount += money;
            OnModelUpdate?.Invoke();
        }

        public void AddMoney(uint amount)
        {
            currentAmount += amount;
            OnModelUpdate?.Invoke();
        }

        public bool CanSpendMoney(uint amountToSpend)
        {
            if (currentAmount >= amountToSpend)
            {
                currentAmount -= amountToSpend;
                return true;
            }
            return false;
        }

        public void UpdateComponent(IComputerComponentModel component)
        {
            switch (component)
            {
                case GPUModel gpuComponent:
                    gpu.UpdateComponent(component);
                    break;
                case CPUModel cpuComponent:
                    cpu.UpdateComponent(component);
                    break;
                case MonitorModel monitorComponent:
                    monitor.UpdateComponent(component);
                    break;
                default:
                    return;
            }
            OnModelUpdate?.Invoke();
        }

        public IComputerComponent GetComponentModel<T>() where T : IComputerComponent
        {
            var type = typeof(T);
            if (components.ContainsKey(type))
            {
                return components[type];
            }
            return null;
        }
    }
}

