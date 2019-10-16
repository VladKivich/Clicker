using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ProgressBar
{
    public class ProgressBarScript : MonoBehaviour
    {
        [SerializeField] private Text clickCount;
        [SerializeField] private Text reduceTime;
        [SerializeField] private Button increaseTime;
        [SerializeField] private Button decreaseTime;
        [SerializeField] private Image Fill;

        private Coroutine reduceTimeCoroutine;
        private float clickCountReduseTime;
        private const float MAX_REDUCE_TIME = 5f;
        private const float MIN_REDUCE_TIME = 0.25f;
        private int countOfClicks;
        private bool isBlocked;

        private void Awake()
        {
            reduceTimeCoroutine = StartCoroutine("ReduceBrogressBar");
            clickCountReduseTime = 1f;
            increaseTime.onClick.AddListener(IncreaseTime);
            decreaseTime.onClick.AddListener(DecreaseTime);
        }

        private IEnumerator ReduceBrogressBar()
        {
            while (true)
            {
                yield return new WaitForSeconds(clickCountReduseTime);
                var amount = Fill.fillAmount;
                if (amount > 0)
                {
                    amount -= 0.01f;
                    Fill.fillAmount = amount;
                }
                else if(amount == 0)
                {
                    isBlocked = false;
                }
            }
        }

        private void IncreaseTime()
        {
            if (clickCountReduseTime < MAX_REDUCE_TIME)
            {
                clickCountReduseTime += 0.25f;
                ChangeTime();
            }
        }

        private void DecreaseTime()
        {
            if (clickCountReduseTime > MIN_REDUCE_TIME)
            {
                clickCountReduseTime -= 0.25f;
                ChangeTime();
            }
        }

        private void ChangeTime()
        {
            reduceTime.text = String.Format($"{clickCountReduseTime.ToString()} sec");
        }

        public void IncreaseClickCount()
        {
            if (Fill.fillAmount < 1f && !isBlocked)
            {
                Fill.fillAmount += 0.05f;
                countOfClicks++;
                ChangeClickCount();
            }
            else
            {
                isBlocked = true;
            }
        }

        private void ChangeClickCount()
        {
            clickCount.text = countOfClicks.ToString();
        }

        public void ResetClickCount()
        {
            countOfClicks = 0;
            ChangeClickCount();
        }
    }
}
