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
        canvasGroup.blocksRaycasts = true;
        
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
        canvasGroup.blocksRaycasts = false;

        canvasGroup.alpha = 0;
        OnHided?.Invoke();
    }

    private IEnumerator Open()
    {
        canvasGroup.blocksRaycasts = true;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }

        OnShowed?.Invoke();
    }

    private IEnumerator Close()
    {
        canvasGroup.blocksRaycasts = false;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }

        OnHided?.Invoke();
    }
}
