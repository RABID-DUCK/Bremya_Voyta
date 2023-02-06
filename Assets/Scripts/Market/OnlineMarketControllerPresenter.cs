using System.Collections.Generic;
using UnityEngine;

public class OnlineMarketControllerPresenter : MonoBehaviour
{
    [SerializeField]
    private MarketController marketController;

    [SerializeField]
    private MarketItemButton marketItemButtonPrefab;

    [SerializeField]
    private Transform buttonSpawnTransform;

    private List<MarketItemButton> spawnedMarketItemButtons = new List<MarketItemButton>();

    private void Awake()
    {
        marketController.OnOpenMarket += WriteMarketItems;
        marketController.OnUpdateMarketItems += WriteMarketItems;

        marketItemButtonPrefab.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        marketController.OnOpenMarket -= WriteMarketItems;
        marketController.OnUpdateMarketItems -= WriteMarketItems;
    }

    public void WriteMarketItems()
    {
        ClearMarketItems();

        if (marketController.onlineSellItems != null)
        {
            foreach (OnlineSellItem sellItem in marketController.onlineSellItems)
            {
                MarketItemButton createdmarketItemButton = Instantiate(marketItemButtonPrefab, buttonSpawnTransform);
                createdmarketItemButton.Initialization(sellItem);
                createdmarketItemButton.gameObject.SetActive(true);
                createdmarketItemButton.OnClick += OnBuyOnlineItem;

                spawnedMarketItemButtons.Add(createdmarketItemButton);
            }
        }
    }

    public void ClearMarketItems()
    {
        foreach (MarketItemButton button in spawnedMarketItemButtons)
        {
            Destroy(button.gameObject);
        }

        spawnedMarketItemButtons.Clear();
    }

    private void OnBuyOnlineItem(OnlineSellItem onlineSellItem)
    {
        marketController.BuyOnlineItem(onlineSellItem);
    }
}
