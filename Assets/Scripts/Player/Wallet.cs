using ExitGames.Client.Photon;
using Photon.Pun;
using System;

public class Wallet
{
    public event Action OnCoinsChanged;

    public int CoinsCount { get; private set; }

    public void PutCoins(int count)
    {
        CoinsCount += count;
        ChangeMoney();
        OnCoinsChanged?.Invoke();
    }

    public void PickUpCoins(int count)
    {
        if (CoinsCount < count)
            throw new InvalidOperationException();

        CoinsCount -= count;
        ChangeMoney();
        OnCoinsChanged?.Invoke();
    }

    private void ChangeMoney()
    {
        Hashtable _CP = new Hashtable();
        _CP["coins"] = CoinsCount;
        PhotonNetwork.LocalPlayer.SetCustomProperties(_CP);
    }

    public void SetMoney(int count)
    {
        CoinsCount = count;
        OnCoinsChanged?.Invoke();
    }
}
