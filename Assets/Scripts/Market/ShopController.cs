using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviourPunCallbacks, IObjectWithCharacter
{
    public event Action OnOpenMarket;
    public event Action OnCloseMarket;
    public event Action OnUpdateMarketItems;

    public SellItem[] ItemsForSale { get => _itemsForSale.ToArray(); }

    [SerializeField]
    private List<SellItem> _itemsForSale;

    public bool Init { get; private set; }

    private Character player;

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("Shop"))
        {
            OnUpdateMarketItems?.Invoke();
        }
    }

    public void InitializePlayer(Character player)
    {
        this.player = player;
        Init = this.player;
    }

    public void OpenMarket()
    {
        OnOpenMarket?.Invoke();
    }

    public void CloseMareket()
    {
        OnCloseMarket?.Invoke();
    }

    public void BuyItemInTheStore(Item item)
    {
        if (Init)
        {
            foreach (SellItem sellItem in _itemsForSale)
            {
                if (sellItem.item == item)
                {
                    if (CheckForLackOfMoney(sellItem.price))
                    {
                        player.PlayerInventory.PutItem(item, sellItem.count);
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

        int randomNumber = rnd.Next(0, 1);

        if (randomNumber == 0)
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

    private bool CheckForLackOfMoney(int coinCount)
    {
        try
        {
            player.PlayerWallet.PickUpCoins(coinCount);
            return true;
        }
        catch (InvalidOperationException)
        {
            UIController.ShowOkInfo($"Вам не хватает {coinCount - player.PlayerWallet.CoinsCount} монет(ы)!");
            return false;
        }
    }
}
