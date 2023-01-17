using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : WindowBehaviour
{
    private Action OnApplyCallback;

    [Header("Message")]
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Apply Button")]
    [SerializeField] private Button applyButton;
    [SerializeField] private TextMeshProUGUI applyButtonText;

    protected override void Start()
    {
        base.Start();

        applyButton.onClick.AddListener(Apply);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        applyButton.onClick.RemoveAllListeners();
    }

    public void ShowInfo(string messageText, string applyButtonText, Action OnApplyCallback)
    {
        this.messageText.text = messageText;
        this.applyButtonText.text = applyButtonText;

        this.OnApplyCallback = OnApplyCallback;

        showCanvasGroup.Show();
    }

    private void Apply()
    {
        showCanvasGroup.Hide();
        OnApplyCallback?.Invoke();
    }
}
