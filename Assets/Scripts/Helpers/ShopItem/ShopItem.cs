using Helpers;
using Models;
using System;

namespace Assets.Scripts.Helpers.ShopItem
{
    public class ShopItem<T>: IShopItem where T : IComputerComponent
    {
        private T component;
        public string Description { get; private set; }
        public uint Cost { get; private set; }

        public ShopItem(T component, uint level)
        {
            //TODO Описание товара
            Description = String.Empty;

            Cost = Constants.BASE_SHOP_COMPONENT_COST;

            this.component = component;

            if(level > Constants.MAX_COMPONENT_LEVEL)
            {
                level = Constants.MAX_COMPONENT_LEVEL;
            }

            for (int i = 1; i < level; i++)
            {
                UpdateItemLevel();
            }
        }

        public IComputerComponent GetTypeOfShopItem()
        {
            return component;
        }

        public uint GetShopItemLevel()
        {
            return component.Level;
        }

        //TODO: Получение улучшений
        public void UpdateItemLevel()
        {
            if (component.Level < Constants.MAX_COMPONENT_LEVEL)
            {
                switch (component)
                {
                    case GPU gpu:
                        var autoClick = gpu.AutoClickRewardPerSecond +
                            Constants.BASE_COMPONENT_GPU_AUTOCLICK_REWARD_PER_SECOND;
                        var fillBar = gpu.FillBarPercentagePerClick +
                            Constants.BASE_COMPONENT_GPU_FILLBAR_PERCENTAGE_PER_CLICK;
                        var level = gpu.Level + 1;
                        component.UpdateComponent(new GPUModel(autoClick, fillBar, level));
                        break;
                    case CPU cpu:
                        var cooldown = cpu.CoolDownPercentagePerStep +
                            Constants.BASE_COMPONENT_CPU_COOLDOWN_PERCENTAGE_PER_STEP;
                        var heat = cpu.HeatPercentagePerClick +
                            Constants.BASE_COMPONENT_CPU_HEAT_PERCENTAGE_PER_CLICK;
                        level = cpu.Level + 1;
                        component.UpdateComponent(new CPUModel(cooldown, heat, level));
                        break;
                    case Monitor monitor:
                        var money = monitor.MoneyForSingleClick +
                            Constants.BASE_COMPONENT_MONEY_FOR_SINGLE_CLICK;
                        level = monitor.Level + 1;
                        component.UpdateComponent(new MonitorModel(money, level));
                        break;
                }
                Cost += Constants.BASE_SHOP_COMPONENT_COST_PROGRESS;
            }
        }

        public IComputerComponentModel GetShopItemModel()
        {
            return component.GetComponentModel();
        }
    }
}
