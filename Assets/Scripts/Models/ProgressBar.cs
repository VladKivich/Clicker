using Helpers;
using System;

namespace Models
{
    public abstract class ProgressBar
    {
        private protected float currentFillAmount;
        private protected float unit = Constants.PROGRESS_BAR_UNIT;

        public event Action onModelUpdate;

        public float GetCurrentAmount
        {
            get
            {
                return currentFillAmount;
            }
        }

        public void AddUnit()
        {
            if(currentFillAmount < Constants.MAXIMUM_FILL_AMOUNT)
            {
                currentFillAmount += unit;
            }
        }

        public void RemoveUnit()
        {
            if (currentFillAmount > 0)
            {
                currentFillAmount -= unit;
            }
        }

        public void Reset()
        {
            currentFillAmount = 0;
        }

        public abstract void UpdateComponentsInfo();
    }

    public class ProgressBarCPU : ProgressBar
    {
        private IComputerComponents progress;

        private float CPUCoolDownValue;

        public ProgressBarCPU(IComputerComponents progress)
        {
            currentFillAmount = 0;
            this.progress = progress;
            UpdateComponentsInfo();
        }

        public override void UpdateComponentsInfo()
        {
            CPUCoolDownValue = progress.CPUCoolDownValue;
        }
    }
}
