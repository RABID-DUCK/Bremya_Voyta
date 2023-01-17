using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : WindowBehaviour
{
    private Action OnApplyCallback;
    private Action OnCancelCallback;

    [Header("Message")]
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Apply Button")]
    [SerializeField] private Button applyButton;
    [SerializeField] private TextMeshProUGUI applyButtonText;

    [Header("Cancel Button")]
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI cancelButtonText;

    protected override void Start()
    {
        base.Start();

        applyButton.onClick.AddListener(Apply);
        cancelButton.onClick.AddListener(Cancel);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        applyButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
    }

    public void ShowDialog(string messageText, string applyButtonText, string cancelButtonText, Action OnApplyCallback, Action OnCancelCallback)
    {
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
}
