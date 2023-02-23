using Ekonomika.Utils;
using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketSellController : MonoBehaviour
{
    public event Action<Inventory> OnOpenSellMarket;
    public event Action OnCloseSellMarket;

    public event Action OnOpenSellItem;
    public event Action OnCloseSellItem;

    [SerializeField]
    private MarketChest marketChest;

    private Inventory playerInventory;

    private void Awake()
    {
        marketChest.OnOpenChes += OpenSellMarket;
    }
    
    private void OnDestroy()
    {
        marketChest.OnOpenChes -= OpenSellMarket;
    }

    public void SellItemOnTheMarket(SellItem sellItem)
    {
        int playerId = PhotonNetwork.LocalPlayer.ActorNumber;
        string playerName = PhotonNetwork.LocalPlayer.NickName;
        MarketLot newMarketLot = new MarketLot(playerId, playerName, sellItem);
        playerInventory.PickUpItem(sellItem.item, sellItem.count);
        
        SendSellTrade(newMarketLot);
    }

    public void OpenSellMarket(Inventory playerInventory)
    {
        this.playerInventory = playerInventory;
        OnOpenSellMarket?.Invoke(playerInventory);
    }

    public void CloseSellMarket()
    {
        OnCloseSellMarket?.Invoke();
    }

    public void OpenSellItem()
    {
        OnOpenSellItem?.Invoke();
    }

    public void CloseSellItem()
    {
        OnCloseSellItem?.Invoke();

        if (playerInventory != null)
        {
            OnOpenSellMarket?.Invoke(playerInventory);
        }
    }

    public void SendSellTrade(MarketLot onlineSellItem)
    {
        List<string> shop = PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Shop") ?
            ((string[])PhotonNetwork.CurrentRoom.CustomProperties["Shop"]).ToList<string>() : new List<string>();
        shop.Add($"{onlineSellItem.playerId}|{onlineSellItem.sellItem.item.name}|{onlineSellItem.sellItem.count}|{onlineSellItem.sellItem.price}");
        Hashtable _CP = new Hashtable();
        _CP["Shop"] = shop.ToArray();
        PhotonNetwork.CurrentRoom.SetCustomProperties(_CP);
    }
}
