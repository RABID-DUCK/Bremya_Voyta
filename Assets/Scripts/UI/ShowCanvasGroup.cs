using System;
using System.Collections;
using UnityEngine;

public class ShowCanvasGroup : MonoBehaviour
{
    public event Action OnShowed;
    public event Action OnHided;

    public CanvasGroup canvasGroup;
    public float speed = 2f;

    public bool IsHided
    {
        get
        {
            return _isHided;
        }

        private set
        {
            _isHided = value;
            canvasGroup.blocksRaycasts = !_isHided;
        }
    }

    [SerializeField]
    private bool _isHided;

    private void Awake()
    {
        IsHided = _isHided;

        if (!IsHided)
        {
            canvasGroup.alpha = 1f;
        }
        else
        {
            canvasGroup.alpha = 0f;
        }
    }

    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(Open());
    }

    public void FastShow()
    {
        canvasGroup.alpha = 1;
        IsHided = false;

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
        IsHided = true;

        OnHided?.Invoke();
    }

    private IEnumerator Open()
    {
        IsHided = false;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }

        OnShowed?.Invoke();
    }

    private IEnumerator Close()
    {
        IsHided = true;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }

        OnHided?.Invoke();
    }
}
