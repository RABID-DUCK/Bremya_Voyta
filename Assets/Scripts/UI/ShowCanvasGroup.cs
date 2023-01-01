using System;
using System.Collections;
using UnityEngine;

public class ShowCanvasGroup : MonoBehaviour
{
    public event Action OnShowed;
    public event Action OnHided;

    public CanvasGroup canvasGroup;
    public float speed = 2f;

    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(Open());
    }

    public void FastShow()
    {
        canvasGroup.alpha = 1;
        OnShowed?.Invoke();
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(Close());
    }

    public void FastHide()
    {
        canvasGroup.alpha = 0;
        OnHided?.Invoke();
    }

    private IEnumerator Open()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }

        OnShowed?.Invoke();
    }

    private IEnumerator Close()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }

        OnHided?.Invoke();
    }
}
