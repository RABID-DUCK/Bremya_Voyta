using Ekonomika.Dialog;
using Ekonomika.Utils;
using System;
using UnityEngine;

public class CitySceneUICoordinator : MonoBehaviour
{
    [Header("Market")]
    [SerializeField] private ShopController marketController;
    [SerializeField] private ShowCanvasGroup marketShowCanvasGroup;

    [SerializeField] private MarketBuyController marketBuyController;
    [SerializeField] private MarketSellController marketSellController;
    [SerializeField] private ShowCanvasGroup marketSellShowCanvasGroup;
    [SerializeField] private ShowCanvasGroup marketSellItemShowCanvasGroup;

    public void Subscribe()
    {
        marketController.OnOpenMarket += OpenMarketPanel;
        marketController.OnCloseMarket += CloseMarketPanel;
        
        marketSellController.OnOpenSellMarket += OpenSellMarket;
        marketSellController.OnCloseSellMarket += CloseSellMarket;
        marketSellController.OnOpenSellItem += OpenSellItem;
        marketSellController.OnCloseSellItem += CloseSellItem;
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
