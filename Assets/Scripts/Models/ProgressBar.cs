using Helpers;
using System;

namespace Models
{
    public abstract class ProgressBar
    {
        private protected float currentFillAmount;
        private protected float unit = 0.01f;

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
    }
}
