using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SellItem
{
    public Item item;
    public int price;

    public SellItem(Item item, int price)
    {
        this.item = item;
        this.price = price;
    }
}

public class MarketController : MonoBehaviour
{
    public event Action OnOpenMarket;
    public event Action OnCloseMarket;

    public SellItem[] ItemsForSale { get => _itemsForSale.ToArray(); }

    [SerializeField]
    private List<SellItem> _itemsForSale;

    public bool Init { get; private set; }

    private Character player;

    public void OpenMarket()
    {
        OnOpenMarket?.Invoke();
    }

    public void CloseMareket()
    {
        OnCloseMarket?.Invoke();
    }

    public void Initialization(Character player)
    {
        this.player = player;
        Init = this.player;
    }

    public void BuyItem(Item item)
    {
        if (Init)
        {
            foreach (SellItem sellItem in _itemsForSale)
            {
                if (sellItem.item == item)
                {
                    Wallet playerWallet = player.PlayerWallet;

                    try
                    {
                        playerWallet.PickUpCoins(sellItem.price);
                        player.PlayerInventory.PutItem(item);
                    }
                    catch (InvalidOperationException)
                    {
                        UIController.ShowOkInfo($"Вам не хватает {sellItem.price - playerWallet.CoinsCount} монет(ы), чтобы купить {item.ItemName}!");
                    }
                }
            }
        }
    }

    public void GetMoney(int countMoney)
    {
        if (Init)
        {
            try
            {
                player.PlayerWallet.PickUpCoins(countMoney);
            }
            catch (InvalidOperationException)
            {
                UIController.ShowInfo($"Вам не хватает {countMoney - player.PlayerWallet.CoinsCount} монет(ы), чтобы внести депозит!", "Ок");
            }
        }
    }

    public bool CalculateProbabilityWinning()
    {
        System.Random rnd = new System.Random();

        int randomNumber = rnd.Next(0,1);

        if(randomNumber == 0f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void CalculateWinningAmount(int countMoney, out int winningAmount)
    {
        winningAmount = countMoney / 100 * 40;
    }

    public void SetWinMoney(int coins)
    {
        player.PlayerWallet.PutCoins(coins);
    }
}
