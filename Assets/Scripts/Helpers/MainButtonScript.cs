using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action onButtonClick;
    public event Action onButtonPressedStart;
    public event Action onButtonPressedEnd;

    private Coroutine buttonPressedCoroutine;
    private bool isAutoClickStarted;
    [HideInInspector] public float AutoCLickCountDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onButtonPressedStart == null || isAutoClickStarted)
            return;
        if (buttonPressedCoroutine == null)
        {
            buttonPressedCoroutine = StartCoroutine(CountDownToStartAutoClick());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onButtonClick != null && !isAutoClickStarted)
        {
            onButtonClick.Invoke();
            StopCoroutine(buttonPressedCoroutine);
            buttonPressedCoroutine = null;
        }
        else if (onButtonPressedEnd != null && isAutoClickStarted)
        {
            buttonPressedCoroutine = null;
            onButtonPressedEnd.Invoke();
            isAutoClickStarted = false;
        }
    }

    private IEnumerator CountDownToStartAutoClick()
    {
        yield return new WaitForSeconds(AutoCLickCountDown);
        isAutoClickStarted = true;
        onButtonPressedStart.Invoke();
    }

}
