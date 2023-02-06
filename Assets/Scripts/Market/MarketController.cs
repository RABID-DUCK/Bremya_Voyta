using Ekonomika.Utils;
using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

public class MarketController : MonoBehaviourPunCallbacks
{
    public event Action OnOpenMarket;
    public event Action OnCloseMarket;

    public event Action OnUpdateMarketItems;

    public SellItem[] ItemsForSale { get => _itemsForSale.ToArray(); }
    public OnlineSellItem[] onlineSellItems
    {
        get
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Shop"))
            {
                List<OnlineSellItem> Items = new List<OnlineSellItem>();

                string[] items = (string[])PhotonNetwork.CurrentRoom.CustomProperties["Shop"];
                // игрок [0] | предмет [1] | количество [2] | цена [3]
                foreach (string item in items)
                {
                    string[] _temp = item.Split("|");

                    Items.Add(new OnlineSellItem(_temp[0], new SellItem(
                        ItemFinder.FindItemByName(_temp[1]),
                        int.Parse(_temp[3]),
                        int.Parse(_temp[2])
                    )));

                    //TODO: дописать поиск по предмета.
                }

                return Items.ToArray();
            }
            return null;
        }
    }

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

    public void BuyOnlineItem(OnlineSellItem onlineSellItem)
    {
        List<string> shop = PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Shop") ?
            ((string[])PhotonNetwork.CurrentRoom.CustomProperties["Shop"]).ToList<string>() : new List<string>();
        string _value = $"{onlineSellItem.playerName}|{onlineSellItem.item.item.name}|{onlineSellItem.item.count}|{onlineSellItem.item.price}";

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Shop"))
        {
            shop.Remove(shop.FirstOrDefault(i => i == _value));
            Hashtable _CP = new Hashtable();
            _CP["Shop"] = shop.ToArray();
            PhotonNetwork.CurrentRoom.SetCustomProperties(_CP);
        }
    }

    public void SellOnlineItem(OnlineSellItem onlineSellItem)
    {
        List<string> shop = PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Shop") ?
            ((string[])PhotonNetwork.CurrentRoom.CustomProperties["Shop"]).ToList<string>() : new List<string>();
        shop.Add($"{onlineSellItem.playerName}|{onlineSellItem.item.item.name}|{onlineSellItem.item.count}|{onlineSellItem.item.price}");
        Hashtable _CP = new Hashtable();
        _CP["Shop"] = shop.ToArray();
        PhotonNetwork.CurrentRoom.SetCustomProperties(_CP);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("Shop"))
        {
            OnUpdateMarketItems?.Invoke();
        }
    }
}
