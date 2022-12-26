using System.Collections;
using UnityEngine;

public class ShowCanvasGroup : MonoBehaviour
{
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
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(Close());
    }

    public void FastHide()
    {
        canvasGroup.alpha = 0;
    }

    private IEnumerator Open()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Close()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
    }
}
