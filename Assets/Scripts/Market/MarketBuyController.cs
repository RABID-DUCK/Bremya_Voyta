using Ekonomika.Utils;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;

public class MarketBuyController : MonoBehaviourPunCallbacks
{
    public event Action OnOpenBuyMarket;
    public event Action OnCloseBuyMarket;

    public event Action OnUpdateMarketItems;

    public MarketLot[] MarketLots
    {
        get
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Shop"))
            {
                List<MarketLot> Items = new List<MarketLot>();
                
                string[] items = (string[])PhotonNetwork.CurrentRoom.CustomProperties["Shop"];
                // игрок-id [0] | предмет [1] | количество [2] | цена [3]
                foreach (string item in items)
                {
                    string[] _temp = item.Split("|");

                    Items.Add(new MarketLot(int.Parse(_temp[0]), PhotonNetwork.CurrentRoom.GetPlayer(int.Parse(_temp[0])).NickName, new SellItem(
                        ItemFinder.FindItemByName(_temp[1]),
                        int.Parse(_temp[3]),
                        int.Parse(_temp[2])
                    )));
                }

                return Items.ToArray();
            }
            return null;
        }
    }

    private Character player;

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("Shop"))
        {
            OnUpdateMarketItems?.Invoke();
        }
    }

    public void Initialization(Character player)
    {
        this.player = player;
    }

    public void OpenBuyMarket()
    {
        OnOpenBuyMarket?.Invoke();
    }

    public void CloseBuyMarket()
    {
        OnCloseBuyMarket?.Invoke();
    }

    public void BuyItemOnTheMarket(MarketLot marketLot)
    {
        try
        {
            player.PlayerWallet.PickUpCoins(marketLot.sellItem.price);

            SendBuyTrade(marketLot);
            player.PlayerInventory.PutItem(marketLot.sellItem.item, marketLot.sellItem.count);
        }
        catch (InvalidOperationException)
        {
            UIController.ShowOkInfo($"Вам не хватает {marketLot.sellItem.price - player.PlayerWallet.CoinsCount} монет(ы)!");
        }
    }

    private void SendBuyTrade(MarketLot lot)
    {
        List<string> shop = PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Shop") ?
            ((string[])PhotonNetwork.CurrentRoom.CustomProperties["Shop"]).ToList<string>() : new List<string>();
        string _value = $"{lot.playerId}|{lot.sellItem.item.name}|{lot.sellItem.count}|{lot.sellItem.price}";

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Shop"))
        {
            shop.Remove(shop.FirstOrDefault(i => i == _value));
            Hashtable _CP = new Hashtable();
            _CP["Shop"] = shop.ToArray();
            PhotonNetwork.CurrentRoom.SetCustomProperties(_CP);

            Player player = PhotonNetwork.CurrentRoom.GetPlayer(lot.playerId);
            Hashtable money = new Hashtable() { { "coins", (int)player.CustomProperties["coins"] + lot.sellItem.price } };
            player.SetCustomProperties(money);
        }
    }
}
