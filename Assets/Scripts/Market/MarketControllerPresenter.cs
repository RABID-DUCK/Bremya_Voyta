using System;
using UnityEngine;

public class MarketControllerPresenter : MonoBehaviour
{
    [SerializeField]
    private MarketController marketController;

    public void BuyWood()
    {
        Calculate(marketController.marketPrices.woodPrice, () =>
        {
            marketController.character.inventory.woodCount++;
        });
    }

    public void BuyBerries()
    {
        Calculate(marketController.marketPrices.berriesPrice, () =>
        {
            marketController.character.inventory.berriesCount++;
        });
    }

    public void BuyCarrot()
    {
        Calculate(marketController.marketPrices.carrotPrice, () =>
        {
            marketController.character.inventory.carrotCount++;
        });
    }

    public void BuyMilk()
    {
        Calculate(marketController.marketPrices.milkPrice, () =>
        {
            marketController.character.inventory.milkCount++;
        });
    }

    public void BuyCoal()
    {
        Calculate(marketController.marketPrices.coalPrice, () =>
        {
            marketController.character.inventory.coalCount++;
        });
    }

    public void BuyIron()
    {
        Calculate(marketController.marketPrices.ironPrice, () =>
        {
            marketController.character.inventory.ironCount++;
        });
    }

    public void BuyMeat()
    {
        Calculate(marketController.marketPrices.meatPrice, () =>
        {
            marketController.character.inventory.meatCount++;
        });
    }

    public void BuyFish()
    {
        Calculate(marketController.marketPrices.fishPrice, () =>
        {
            marketController.character.inventory.fishCount++;
        });
    }

    private void Calculate(int price, Action OnSuccess)
    {
        if (price <= marketController.character.inventory.coins)
        {
            marketController.character.inventory.coins -= price;
            OnSuccess?.Invoke();
        }
        else
        {
            //TODO: Pop-up window
        }
    }
}
