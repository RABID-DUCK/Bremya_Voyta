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
                        UIController.ShowOkInfo($"Вам не хватает {sellItem.price - playerWallet.CoinsCount} монет, чтобы купить {item.ItemName}!");
                    }
                }
            }
        }
    }
}
