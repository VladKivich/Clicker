using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Helpers;

public class ClickerMainButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action onButtonClick;
    public event Action onButtonPressedStart;
    public event Action onButtonPressedEnd;

    private Coroutine pressedButtonDelay;
    private ClickerButtonStates state;

    private void Start()
    {
        state = ClickerButtonStates.SingleClick;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressedButtonDelay = StartCoroutine(AutoClickDelay());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (state)
        {
            case ClickerButtonStates.SingleClick:
                StopCoroutine(pressedButtonDelay);
                onButtonClick?.Invoke();
                break;
            case ClickerButtonStates.ButtonIsPressed:
                onButtonPressedEnd?.Invoke();
                state = ClickerButtonStates.SingleClick;
                break;
        }
    }

    private IEnumerator AutoClickDelay()
    {
        yield return new WaitForSeconds(Constants.DELAY_BEFOR_AUTO_CLICK);
        onButtonPressedStart?.Invoke();
        state = ClickerButtonStates.ButtonIsPressed;
    }

}
