using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[Serializable]
public struct SellItem
{
    public Item item;
    public int price;
    public int count;

    public SellItem(Item item, int price, int count)
    {
        this.item = item;
        this.price = price;
        this.count = count;
    }
}

public struct OnlineSellItem
{
    public string playerName;
    public SellItem item;

    public OnlineSellItem(string playerName, SellItem sellItem)
    {
        this.playerName = playerName;
        item = sellItem;
    }
}

public class MarketController : MonoBehaviour
{
    public event Action OnOpenMarket;
    public event Action OnCloseMarket;

    public SellItem[] ItemsForSale { get => _itemsForSale.ToArray(); }

    private List<OnlineSellItem> onlineSellItems;

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

    public void UpdateMarket()
    {
        string[] items = (string[])PhotonNetwork.CurrentRoom.CustomProperties["ShopItems"];
        // игрок [0] | предмет [1] | количество [2] | цена [3]
        foreach (string item in items)
        {
            string[] _temp = item.Split("|");

            onlineSellItems.Add(new OnlineSellItem(_temp[0], new SellItem())); //TODO: дописать поиск по предмета.
        }
    }
}
