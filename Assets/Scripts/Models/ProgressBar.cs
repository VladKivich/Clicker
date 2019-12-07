using Assets.Scripts.Helpers;
using Helpers;
using System;

namespace Models
{
    public abstract class ProgressBar: IModel
    {
        private protected float currentFillAmount;
        private protected float unit = Constants.PROGRESS_BAR_UNIT;

        public event Action OnModelUpdate;

        public float GetCurrentAmount
        {
            get
            {
                return currentFillAmount;
            }
        }

        public void Reset()
        {
            currentFillAmount = 0;
            Update();
        }

        protected void Update()
        {
            OnModelUpdate?.Invoke();
        }

        public abstract void UpdateComponentsInfo();

        public abstract void AddUnit();

        public abstract void RemoveUnit();

        public bool CanRemoveUnit
        {
            get
            {
                return currentFillAmount > 0;
            }
        }
    }

    public class ProgressBarCPU : ProgressBar
    {
        private IComponentsPC components;
        private float CoolDownValue;
        private float HeatValue;
        private bool overHeat;

        public bool IsOverHeated
        {
            get => overHeat;
        }

        public ProgressBarCPU(IComponentsPC components)
        {
            currentFillAmount = 0;
            this.components = components;
            if(components is IModel model)
            {
                model.OnModelUpdate += UpdateComponentsInfo;
            }
            UpdateComponentsInfo();
        }

        public override void UpdateComponentsInfo()
        {
            CoolDownValue = components.CPUCoolDownPercentagePerStep * Constants.PROGRESS_BAR_UNIT;
            HeatValue = components.CPUHeatPercentagePerClick * Constants.PROGRESS_BAR_UNIT;
        }

        public override void AddUnit()
        {
            if(currentFillAmount < Constants.MAXIMUM_FILL_AMOUNT)
            {
                currentFillAmount += HeatValue;
                Update();
            }
            else
            {
                overHeat = true;
            }
        }

        public override void RemoveUnit()
        {
            if(currentFillAmount > 0)
            {
                currentFillAmount -= CoolDownValue;
                Update();
            }
            else
            {
                overHeat = false;
                Reset();
            }
        }
    }

    public class ProgressBarGPU : ProgressBar
    {
        private IComponentsPC components;
        public float IncomePerAutoClickStep { get; private set; }

        private float FillBarPerClick;
        private float ReduceBarPerAutoClickStep;

        public bool CanStartAutoClick
        {
            get
            {
                return currentFillAmount >= Constants.MAXIMUM_FILL_AMOUNT;
            }
        }

        public ProgressBarGPU(IComponentsPC components)
        {
            currentFillAmount = 0;
            this.components = components;
            ReduceBarPerAutoClickStep = (Constants.MAXIMUM_FILL_AMOUNT / Constants.AUTO_CLICK_TIME) 
                * Constants.AUTO_CLICK_STEP_TIME;
            if (components is IModel model)
            {
                model.OnModelUpdate += UpdateComponentsInfo;
            }
            UpdateComponentsInfo();
        }

        public override void UpdateComponentsInfo()
        {
            IncomePerAutoClickStep = (components.GPUAutoClickRewardPerSecond * Constants.AUTO_CLICK_TIME) / 
                Constants.AUTO_CLICK_TIME * Constants.AUTO_CLICK_STEP_TIME;
            FillBarPerClick = components.GPUFillBarPercentagePerClick * Constants.PROGRESS_BAR_UNIT;
        }

        public override void AddUnit()
        {
            if (currentFillAmount < Constants.MAXIMUM_FILL_AMOUNT)
            {
                currentFillAmount += FillBarPerClick;
                Update();
            }
        }

        public override void RemoveUnit()
        {
            if (currentFillAmount > 0)
            {
                currentFillAmount -= ReduceBarPerAutoClickStep;
                Update();
            }
        }
    }
}
