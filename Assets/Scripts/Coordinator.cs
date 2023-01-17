using Ekonomika.Dialog;
using System;
using UnityEngine;

public class Coordinator : MonoBehaviour
{
    public static event Action OnEndEducation;

    [Header("Start")]
    [SerializeField] private DialogData startDialogData;
    [SerializeField] private DialogPresenter dialogPresenter;

    [Header("Market")]
    [SerializeField] private MarketController marketController;
    [SerializeField] private ShowCanvasGroup marketShowCanvasGroup;

    private void Start()
    {
        if (dialogPresenter)
        {
            dialogPresenter.OnDialogEnd += EndEducation;
            dialogPresenter.StartDialog(startDialogData);
        }
    }

    private void EndEducation()
    {
        dialogPresenter.OnDialogEnd -= EndEducation;

        marketController.OnOpenMarket += OpenMarketPanel;
        marketController.OnCloseMarket += CloseMarketPanel;

        if (marketShowCanvasGroup != null)
        {
            marketShowCanvasGroup.FastHide();
        }

        OnEndEducation?.Invoke();
    }

    private void OpenMarketPanel()
    {
        if (marketShowCanvasGroup != null)
        {
            marketShowCanvasGroup.Show();
        }
    }

    private void CloseMarketPanel()
    {
        if (marketShowCanvasGroup != null)
        {
            marketShowCanvasGroup.Hide();
        }
    }
}
