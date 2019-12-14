using Assets.Scripts.Helpers;
using Assets.Scripts.Helpers.GameEvents;
using BaseScripts;
using Helpers;
using Models;
using System;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class ClickerController : BaseController, IGameEventSender
    {
        private GameProgress progress;
        private MonoBehaviour mb;

        private ClickerMainButton mainButton;
        public ProgressBarCPU cpu { get; private set; }
        public ProgressBarGPU gpu { get; private set; }

        private Coroutine autoClick;
        private Coroutine cpuCoolDown;

        public ClickerStates state { get; private set; }

        private uint autoClickMoney = 0;

        #region GameEvents

        private GameEventType questEventTypes;

        private GameEventType currentQuestEventTypes;

        public GameEventType GetGameEventTypes => questEventTypes;

        public event Action<GameEventType, GameEvent> SendComplexGameEventMessage;
        public event Action<GameEventType> SendSimpleGameEventMessage;

        #endregion

        public void StartSendingEventType(GameEventType eventType)
        {
            if (questEventTypes.HasFlag(eventType))
            {
                currentQuestEventTypes |= eventType;
            }
        }

        public void StopSendingEventType(GameEventType eventType)
        {
            if (questEventTypes.HasFlag(eventType) && currentQuestEventTypes.HasFlag(eventType))
            {
                currentQuestEventTypes ^= eventType;
            }
        }

        public ClickerController(ClickerMainButton mainButton)
        {
            Core.Instance.AddController<ClickerController>(this);
            progress = Core.Instance.GetProgress;
            mb = Core.Instance.GetMB;
            this.mainButton = mainButton;
            cpu = new ProgressBarCPU(progress);
            gpu = new ProgressBarGPU(progress);
            questEventTypes = GameEventType.AutoClick | GameEventType.SingleClick | GameEventType.EarnMoney;
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
                    if (!cpu.IsOverHeated)
                    {
                        cpu.AddUnit();
                        TrySendSimpleGameEvent(GameEventType.SingleClick);
                        progress.AddMoneyForClick();
                        TrySendCoplexGameEvent(GameEventType.EarnMoney, progress.MoneyForSingleClick);
                    }
                    gpu.AddUnit();
                    break;
                case ClickerStates.AutoClicks:
                    progress.AddMoneyForAutoClick(autoClickMoney);
                    TrySendCoplexGameEvent(GameEventType.EarnMoney, autoClickMoney);
                    break;
            }
        }

        private void TrySendSimpleGameEvent(GameEventType type)
        {
            if (currentQuestEventTypes.HasFlag(type))
            {
                SendSimpleGameEventMessage?.Invoke(type);
            }
        }

        private void TrySendCoplexGameEvent(GameEventType type, uint currentMoney)
        {
            if (currentQuestEventTypes.HasFlag(type))
            {
                GameEvent gameEvent;

                switch (type)
                {
                    case GameEventType.EarnMoney:
                        gameEvent = new AddMoneyGameEvent(currentMoney);
                        SendComplexGameEventMessage?.Invoke(GameEventType.EarnMoney, gameEvent);
                        break;
                }
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
            TrySendSimpleGameEvent(GameEventType.AutoClick);
            while (gpu.CanRemoveUnit)
            {
                yield return new WaitForSeconds(stepTime);
                gpu.RemoveUnit();
                temp += gpu.IncomePerAutoClickStep;
                if (temp >= 1)
                {
                    autoClickMoney = (uint)temp;
                    OnClick();
                    temp -= autoClickMoney;
                    autoClickMoney = 0;
                }
            }
            gpu.Reset();
        }

        public void OnPressedEnd()
        {
            if (state == ClickerStates.AutoClicks)
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
