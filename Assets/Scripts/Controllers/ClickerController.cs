using BaseScripts;
using Helpers;
using Models;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class ClickerController : BaseController
    {
        private GameProgress progress;
        private MonoBehaviour mb;

        private ClickerMainButton mainButton;
        private ProgressBarCPU cpu;

        private Coroutine autoClick;
        private float autoClickTime = 5.0f;

        public ClickerStates state { get; private set; }

        public ClickerController(ClickerMainButton mainButton)
        {
            progress = Core.GetProgress;
            mb = Core.GetMB;
            this.mainButton = mainButton;
            On();
        }

        public override void On()
        {
            base.On();
            mainButton.onButtonClick += OnClick;
            mainButton.onButtonPressedStart += OnPressedStart;
            mainButton.onButtonPressedEnd += OnPressedEnd;
            state = ClickerStates.ActiveClicks;
        }

        public override void Off()
        {
            base.Off();
            mainButton.onButtonClick -= OnClick;
            mainButton.onButtonPressedStart -= OnPressedStart;
            mainButton.onButtonPressedEnd -= OnPressedEnd;
            state = ClickerStates.Pause;
        }

        public void OnClick()
        {
            //TODO Модель прогресса
            Debug.Log("Manual click +1");
            

        }

        public void OnPressedStart()
        {
            autoClick = mb.StartCoroutine(AutoClicks());
        }

        private IEnumerator AutoClicks()
        {
            var timeLeft = autoClickTime;
            while(timeLeft > 0)
            {
                yield return new WaitForSeconds(0.1f);
                timeLeft-=0.1f;
                Debug.Log("Auto click +1");
            }
        }

        public void OnPressedEnd()
        {
            mb.StopCoroutine(autoClick);
            autoClick = null;
            Debug.Log("Auto click is stopped");
        }
    }
}
