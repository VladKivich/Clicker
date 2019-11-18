using BaseScripts;
using Helpers;
using Models;
using System;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class ClickerController : BaseController
    {
        private GameProgress progress;
        private MonoBehaviour mb;

        private ClickerMainButton mainButton;
        public ProgressBarCPU cpu { get; private set; }
        public ProgressBarGPU gpu { get; private set; }

        private Coroutine autoClick;
        private Coroutine cpuCoolDown;

        public ClickerStates state { get; private set; }
        private int autoClickMoney = 0;

        public ClickerController(ClickerMainButton mainButton)
        {
            progress = Core.GetProgress;
            mb = Core.GetMB;
            this.mainButton = mainButton;
            cpu = new ProgressBarCPU(progress);
            gpu = new ProgressBarGPU(progress);
            On();
        }

        public override void On()
        {
            base.On();
            mainButton.onButtonClick += OnClick;
            mainButton.onButtonPressedStart += OnPressedStart;
            mainButton.onButtonPressedEnd += OnPressedEnd;
            cpuCoolDown = mb.StartCoroutine(CoolDown());
            state = ClickerStates.ActiveClicks;
        }

        public override void Off()
        {
            base.Off();
            mainButton.onButtonClick -= OnClick;
            mainButton.onButtonPressedStart -= OnPressedStart;
            mainButton.onButtonPressedEnd -= OnPressedEnd;
            mb.StopCoroutine(cpuCoolDown);
            state = ClickerStates.Pause;
        }

        public void OnClick()
        {
            switch (state)
            {
                case ClickerStates.ActiveClicks:
                    if (!cpu.IsOverHeater)
                    {
                        cpu.AddUnit();
                        progress.AddMoneyForClick();
                    }
                    gpu.AddUnit();
                    break;
                case ClickerStates.AutoClicks:
                    progress.AddMoneyAutoClick(autoClickMoney);
                    break;
            }
        }

        public void OnPressedStart()
        {
            if (gpu.CanStartAutoClick)
            {
                autoClick = mb.StartCoroutine(AutoClicks());
            }
        }

        private IEnumerator CoolDown()
        {
            var stepTime = Constants.COOL_DOWN_STEP_TIME;
            while (true)
            {
                yield return new WaitForSeconds(stepTime);
                cpu.RemoveUnit();
            }
        }

        private IEnumerator AutoClicks()
        {
            var stepTime = Constants.AUTO_CLICK_STEP_TIME;
            state = ClickerStates.AutoClicks;
            float temp = 0f;
            while (gpu.CanRemoveUnit)
            {
                yield return new WaitForSeconds(stepTime);
                gpu.RemoveUnit();
                temp += gpu.IncomePerAutoClickStep;
                if(temp >= 1)
                {
                    autoClickMoney = (int)temp;
                    OnClick();
                    temp -= autoClickMoney;
                    autoClickMoney = 0;
                }
            }
            gpu.Reset();
        }

        public void OnPressedEnd()
        {
            if(state == ClickerStates.AutoClicks)
            {
                mb.StopCoroutine(autoClick);
                if (gpu.GetCurrentAmount > 0)
                {
                    gpu.Reset();
                }
                state = ClickerStates.ActiveClicks;
            }
        }
    }
}
