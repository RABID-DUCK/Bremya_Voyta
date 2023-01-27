using System.Collections.Generic;
using UnityEngine;

public class MarketControllerPresenter : MonoBehaviour
{
    [SerializeField]
    private MarketController marketController;

    [SerializeField]
    private MarketItemButton marketItemButton;

    [SerializeField]
    private Transform buttonSpawnTransform;

    private List<MarketItemButton> spawnedMarketItemButtons = new List<MarketItemButton>();

    private void Start()
    {
        marketItemButton.gameObject.SetActive(false);
        marketController.OnOpenMarket += UpdateItemsForSale;
    }

    private void OnDestroy()
    {
        marketController.OnOpenMarket -= UpdateItemsForSale;
    }

    public void UpdateItemsForSale()
    {
        ResetItemsForSale();

        foreach (SellItem sellItem in marketController.ItemsForSale)
        {
            MarketItemButton createdmarketItemButton = Instantiate(marketItemButton, buttonSpawnTransform);

            createdmarketItemButton.Initialization(sellItem);
            createdmarketItemButton.OnClick += marketController.BuyItem;
            createdmarketItemButton.gameObject.SetActive(true);

            spawnedMarketItemButtons.Add(createdmarketItemButton);
        }
    }

    private void ResetItemsForSale()
    {
        foreach (MarketItemButton button in spawnedMarketItemButtons)
        {
            button.OnClick -= marketController.BuyItem;
            Destroy(button.gameObject);
        }

        spawnedMarketItemButtons.Clear();
    }
}
