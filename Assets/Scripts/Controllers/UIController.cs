using BaseScripts;
using View;

namespace Controllers
{
    public class UIController : BaseController
    {
        private InGameUIView inGameUI;

        public UIController(InGameUIView inGameUI, ClickerController clickerController)
        {
            this.inGameUI = inGameUI;
            inGameUI.SetModels(clickerController);
            Core.Instance.AddController<UIController>(this);
        }
    }
}
