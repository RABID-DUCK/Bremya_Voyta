using System.Collections.Generic;
using UnityEngine;

public class ShopControllerPresenter : MonoBehaviour
{
    [SerializeField]
    private MarketController marketController;

    [SerializeField]
    private ShopItemButton marketItemButton;

    [SerializeField]
    private Transform buttonSpawnTransform;

    private List<ShopItemButton> spawnedMarketItemButtons = new List<ShopItemButton>();

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
            ShopItemButton createdmarketItemButton = Instantiate(marketItemButton, buttonSpawnTransform);

            createdmarketItemButton.Initialization(sellItem);
            createdmarketItemButton.OnClick += marketController.BuyItemInTheStore;
            createdmarketItemButton.gameObject.SetActive(true);

            spawnedMarketItemButtons.Add(createdmarketItemButton);
        }
    }

    private void ResetItemsForSale()
    {
        foreach (ShopItemButton button in spawnedMarketItemButtons)
        {
            button.OnClick -= marketController.BuyItemInTheStore;
            Destroy(button.gameObject);
        }

        spawnedMarketItemButtons.Clear();
    }
}
