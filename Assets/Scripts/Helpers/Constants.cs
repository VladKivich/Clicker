namespace Helpers
{
    public static class Constants
    {
        public const float MAXIMUM_FILL_AMOUNT = 1.0f;
        public const float PROGRESS_BAR_UNIT = 0.01f;
        public const float COOL_DOWN_STEP_TIME = 1.0f;
        public const float AUTO_CLICK_TIME = 3.0f;
        public const float AUTO_CLICK_STEP_TIME = 0.01f;
        public const float DELAY_BEFOR_AUTO_CLICK = 0.75f;

        #region BaseComponentesValues

        public const float BASE_COMPONENT_CPU_COOLDOWN_PERCENTAGE_PER_STEP = 5f;
        public const float BASE_COMPONENT_CPU_HEAT_PERCENTAGE_PER_CLICK = 7.5f;
        public const float BASE_COMPONENT_GPU_FILLBAR_PERCENTAGE_PER_CLICK = 2.75f;
        public const int BASE_COMPONENT_GPU_AUTOCLICK_REWARD_PER_SECOND  = 50;
        public const uint BASE_COMPONENT_MONEY_FOR_SINGLE_CLICK = 10;
        public const uint BASE_COMPONENT_LEVEL = 1;
        public const uint MAX_COMPONENT_LEVEL = 10;

        #endregion

        #region BaseShopProgressValues

        public const float BASE_SHOP_CPU_COOLDOWN_PROGRESS = 0.5f;
        public const float BASE_SHOP_CPU_HEAT_PROGRESS = 0.25f;
        public const float BASE_SHOP_GPU_FILLBAR_PROGRESS = 10.25f;
        public const int BASE_SHOP_GPU_AUTOCLICK_REWARD_PROGRESS = 5;
        public const int BASE_SHOP_MONEY_FOR_SINGLE_CLICK_PROGRESS = 1;
        public const uint BASE_SHOP_COMPONENT_COST = 10;
        public const uint BASE_SHOP_COMPONENT_COST_PROGRESS = 50;

        #endregion
    }
}
