using Ekonomika.Dialog;
using Ekonomika.Utils;
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
    
    [SerializeField] private ShopController marketController;
    [SerializeField] private ShowCanvasGroup marketShowCanvasGroup;

    [SerializeField] private MarketBuyController marketBuyController;
    [SerializeField] private MarketSellController marketSellController;
    [SerializeField] private ShowCanvasGroup marketSellShowCanvasGroup;
    [SerializeField] private ShowCanvasGroup marketSellItemShowCanvasGroup;

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
        taxBoxPresenter.PlayerInitialization(player);
        marketBuyController.Initialization(player);

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
        
        marketSellController.OnOpenSellMarket += OpenSellMarket;
        marketSellController.OnCloseSellMarket += CloseSellMarket;
        marketSellController.OnOpenSellItem += OpenSellItem;
        marketSellController.OnCloseSellItem += CloseSellItem;

        OnEndEducation?.Invoke();
    }

    private void OpenMarketPanel()
    {
        marketShowCanvasGroup.Show();
    }

    private void CloseMarketPanel()
    {
        marketShowCanvasGroup.Hide();
    }

    private void OpenSellMarket(Inventory inventory)
    {
        marketSellShowCanvasGroup.Show();
    }

    private void CloseSellMarket()
    {
        marketSellShowCanvasGroup.Hide();
    }

    private void OpenSellItem()
    {
        marketSellItemShowCanvasGroup.Show();
    }

    private void CloseSellItem()
    {
        marketSellItemShowCanvasGroup.Hide();
    }
}
