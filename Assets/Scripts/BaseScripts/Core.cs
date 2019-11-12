using Controllers;
using Helpers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace BaseScripts
{
    public class Core : MonoBehaviour
    {
        #region Buttons&UI

        [SerializeField] private ClickerMainButton mainButton;
        [SerializeField] private InGameUI inGameUI;

        #endregion

        #region Controllers&Models

        private ClickerController clickerController;
        private UIController uiController;

        #endregion

        [SerializeField] private Camera mainCamera;

        public static Core GetCore { get; private set; }
        public static MonoBehaviour GetMB { get; private set; }
        public static GameProgress GetProgress { get; private set; }

        private void Awake()
        {
            GetCore = this;
            GetMB = this;
            GetProgress = new GameProgress();
            mainButton.DelayBeforeAutoClick = Constants.DELAY_BEFOR_AUTO_CLICK;

            #region CreateControllers

            clickerController = new ClickerController(mainButton);
            uiController = new UIController(inGameUI, clickerController);

            #endregion
        }

        private void LateUpdate()
        {

        }
    }
}

