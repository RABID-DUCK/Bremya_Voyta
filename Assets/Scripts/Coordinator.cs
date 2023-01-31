using Ekonomika.Dialog;
using System;
using UnityEngine;

public class Coordinator : MonoBehaviour
{
    public static event Action OnEndEducation;

    [SerializeField] private ClickEventer clickEventer;
    [SerializeField] private UIUpdater uiUpdater;

    [Header("Start")]
    [SerializeField] private DialogData startDialogData;
    [SerializeField] private DialogPresenter dialogPresenter;

    [Header("Market")]
    [SerializeField] private MarketController marketController;
    [SerializeField] private ShowCanvasGroup marketShowCanvasGroup;

    [Header("Tax")]
    [SerializeField] private TaxBoxPresenter taxBoxPresenter;
    
    private Character player;

    private void Start()
    {
        if (dialogPresenter)
        {
            dialogPresenter.OnDialogEnd += EndEducation;
            dialogPresenter.StartDialog(startDialogData);
        }

        clickEventer.OnClickObject += OnClickObject;
    }

    private void OnDestroy()
    {
        clickEventer.OnClickObject -= OnClickObject;
    }

    public void InitializationPlayer(Character player)
    {
        player.Initialization(clickEventer);
        marketController.Initialization(player);
        uiUpdater.Initialization(player);
        taxBoxPresenter.Initialization(player);

        this.player = player;
    }

    private void OnClickObject(IClickableObject clickableObject)
    {
        if (player)
        {
            clickableObject.Execute(player);
        }
    }

    private void EndEducation()
    {
        dialogPresenter.OnDialogEnd -= EndEducation;

        marketController.OnOpenMarket += OpenMarketPanel;
        marketController.OnCloseMarket += CloseMarketPanel;

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
