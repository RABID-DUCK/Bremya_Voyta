using System.Collections;
using UnityEngine;

public abstract class BaseTutorialObject : MonoBehaviour
{
    private IEnumerator highlightCoroutine = null;

    public void StartHighlightObject()
    {
        highlightCoroutine = HighlightObject();
        StartCoroutine(highlightCoroutine);
    }

    public void StopHighlightObject()
    {
        StopCoroutine(highlightCoroutine);
        highlightCoroutine = null;
        
        OnStopHighlightObject();
    }

    protected abstract IEnumerator HighlightObject();
    protected abstract void OnStopHighlightObject();
}
