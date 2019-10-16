using ProgressBar;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BaseScripts
{
    public class UIController: MonoBehaviour
    {
        [SerializeField] ProgressBarScript[] bars;
        [SerializeField] Button Click;
        [SerializeField] Button Exit;
        [SerializeField] Button ClearAllClicks;
        private Action AddClick;

        private void Awake()
        {
            if(bars != null)
            {
                for (int i = 0; i < bars.Length; i++)
                {
                    AddClick += bars[i].IncreaseClickCount;
                }
            }

            Click.onClick.AddListener(OnClick);
            Exit.onClick.AddListener(ApplicationClose);
            ClearAllClicks.onClick.AddListener(ResetAllBars);

        }

        private void OnClick()
        {
           if(AddClick != null)
            {
                AddClick.Invoke();
            }
        }

        private void ApplicationClose()
        {
            Application.Quit();
        }

        private void ResetAllBars()
        {
            if (bars != null)
            {
                for (int i = 0; i < bars.Length; i++)
                {
                    bars[i].ResetClickCount();
                }
            }
        }

    }
}
