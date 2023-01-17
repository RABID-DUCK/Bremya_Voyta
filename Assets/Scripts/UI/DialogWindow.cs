using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    public ShowCanvasGroup showCanvasGroup;

    private Action OnApplyCallback;
    private Action OnCancelCallback;

    [Header("Title")]
    [SerializeField] private TextMeshProUGUI titleText;

    [Header("Message")]
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Apply Button")]
    [SerializeField] private Button applyButton;
    [SerializeField] private TextMeshProUGUI applyButtonText;

    [Header("Cancel Button")]
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI cancelButtonText;

    private void Awake()
    {
        showCanvasGroup.canvasGroup.alpha = 0;
        applyButton.onClick.AddListener(Apply);
        cancelButton.onClick.AddListener(Cancel);

        showCanvasGroup.OnHided += DestroyWindow;
    }

    private void OnDestroy()
    {
        applyButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        showCanvasGroup.OnHided -= DestroyWindow;
    }

    public void ShowDialog(string titleText, string messageText, string applyButtonText, string cancelButtonText, Action OnApplyCallback, Action OnCancelCallback)
    {
        this.titleText.text = titleText;
        this.messageText.text = messageText;
        this.applyButtonText.text = applyButtonText;
        this.cancelButtonText.text = cancelButtonText;

        this.OnApplyCallback = OnApplyCallback;
        this.OnCancelCallback = OnCancelCallback;

        showCanvasGroup.Show();
    }

    private void Apply()
    {
        showCanvasGroup.Hide();
        OnApplyCallback?.Invoke();
    }

    private void Cancel()
    {
        showCanvasGroup.Hide();
        OnCancelCallback?.Invoke();
    }

    private void DestroyWindow()
    {
        Destroy(this.gameObject);
    }
}
