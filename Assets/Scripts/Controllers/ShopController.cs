using Assets.Scripts.Helpers;
using Assets.Scripts.Helpers.ShopItem;
using BaseScripts;
using Models;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class ShopController : BaseController
    {
        private GameProgress progress;

        #region ShopItems

        private ShopItem<GPU> gpu;
        private ShopItem<CPU> cpu;
        private ShopItem<Monitor> monitor;

        private List<IShopItem> items;

        #endregion

        public ShopController()
        {
            progress = Core.Instance.GetProgress;
            Core.Instance.AddController<ShopController>(this);
            Initialize();
        }

        public void Initialize()
        {
            gpu = new ShopItem<GPU>(new GPU(), progress.GetComponentModel<GPU>().Level);
            cpu = new ShopItem<CPU>(new CPU(), progress.GetComponentModel<CPU>().Level);
            monitor = new ShopItem<Monitor>(new Monitor(), progress.GetComponentModel<Monitor>().Level);

            items = new List<IShopItem>
            {
                gpu, cpu, monitor
            };
        }

        public void UpgradeComponent<T>()where T : IComputerComponent
        {
            foreach (var item in items)
            {
                if(item.GetTypeOfShopItem() is T)
                {
                    var cost = item.Cost;
                    if (progress.CanSpendMoney(cost))
                    {
                        progress.UpdateComponent(item.GetShopItemModel());
                        item.UpdateItemLevel();
                        Debug.Log($"Component {item.GetTypeOfShopItem()} has been upgraded!");
                    }
                    else
                    {
                        Debug.Log($"No enught money! Need: {item.Cost}");
                    }
                    return;
                }
            }
        }
    }
}
