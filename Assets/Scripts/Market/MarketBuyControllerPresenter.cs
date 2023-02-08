using System.Collections.Generic;
using UnityEngine;

public class MarketBuyControllerPresenter : MonoBehaviour
{
    [SerializeField] MarketBuyController marketBuyController;
    
    [Space]

    [SerializeField] public Transform buttonSpawnTransform;
    [SerializeField] public MarketItemButton marketItemButtonPrefab;

    private List<MarketItemButton> spawnedButtons = new List<MarketItemButton>();

    private void Start()
    {
        marketBuyController.OnUpdateMarketItems += UpdateMarketItems;
        marketItemButtonPrefab.gameObject.SetActive(false);
    }

    private void UpdateMarketItems()
    {
        ClearMarketItems();

        foreach (MarketLot marketLot in marketBuyController.MarketLots)
        {
            MarketItemButton spawnedMarketItemButton = Instantiate(marketItemButtonPrefab, buttonSpawnTransform);
            spawnedMarketItemButton.Initialization(marketLot);
            spawnedMarketItemButton.gameObject.SetActive(true);
            spawnedMarketItemButton.OnClick += marketBuyController.BuyItemOnTheMarket;

            spawnedButtons.Add(spawnedMarketItemButton);
        }
    }

    private void ClearMarketItems()
    {
        foreach (MarketItemButton button in spawnedButtons)
        {
            Destroy(button.gameObject);
        }
        
        spawnedButtons.Clear();
    }
}
