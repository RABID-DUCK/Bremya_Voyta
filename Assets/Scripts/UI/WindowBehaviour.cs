using UnityEngine;

public class WindowBehaviour : MonoBehaviour
{
    public ShowCanvasGroup showCanvasGroup;

    protected virtual void Start()
    {
        showCanvasGroup.OnHided += DestroyWindow;
    }

    protected virtual void OnDestroy()
    {
        showCanvasGroup.OnHided -= DestroyWindow;
    }

    private void DestroyWindow()
    {
        Destroy(this.gameObject);
    }
}
