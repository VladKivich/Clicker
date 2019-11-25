using BaseScripts;

namespace Controllers
{
    public class UIController : BaseController
    {
        private InGameUI inGameUI;

        public UIController(InGameUI inGameUI, ClickerController clickerController)
        {
            this.inGameUI = inGameUI;
            inGameUI.SetModels(clickerController);
            Core.Instance.AddController<UIController>(this);
        }
    }
}
