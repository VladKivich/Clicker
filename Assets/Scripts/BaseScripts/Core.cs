using Controllers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace BaseScripts
{
    public class Core : MonoBehaviour
    {
        #region ProgressBars

        [SerializeField] private Image gpuImage;
        [SerializeField] private Image cpuImage;
        [SerializeField, Range(1, 5)] private float AutoClickCountDown = 3f;

        #endregion

        #region Buttons

        [SerializeField] private ClickerMainButton mainButton;

        #endregion

        #region Controllers&Models

        private ClickerController clickerController;

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
            mainButton.DelayBeforeAutoClick = AutoClickCountDown;

            #region CreateControllers

            clickerController = new ClickerController(mainButton);

            #endregion
        }

        private void LateUpdate()
        {

        }
    }
}

