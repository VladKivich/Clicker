using BaseScripts;
using Models;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class ClickerController : BaseController
    {
        private GameProgress progress;
        private MonoBehaviour mb;

        private MainButtonScript mainButton;

        private Coroutine autoClick;
        private float autoClickTime = 5.0f;

        public ClickerController(MainButtonScript mainButton)
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

        }

        public override void Off()
        {
            base.Off();
            mainButton.onButtonClick -= OnClick;
            mainButton.onButtonPressedStart -= OnPressedStart;
            mainButton.onButtonPressedEnd -= OnPressedEnd;
        }

        public void OnClick()
        {
            Debug.Log("Manual click +1");
        }

        public void OnPressedStart()
        {
            if(autoClick == null)
            {
                autoClick = mb.StartCoroutine(StartAutoClick());
            }
        }

        private IEnumerator StartAutoClick()
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
