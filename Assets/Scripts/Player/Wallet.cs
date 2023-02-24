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
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable() { { "coins", CoinsCount } });
        OnCoinsChanged?.Invoke();
    }

    public void PickUpCoins(int count)
    {
        if (CoinsCount < count)
            throw new InvalidOperationException();

        CoinsCount -= count;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable() { { "coins", CoinsCount } });
        OnCoinsChanged?.Invoke();
    }

    public void Set(int count)
    {
        if (count != CoinsCount)
        {
            CoinsCount = count;
            OnCoinsChanged?.Invoke();
        }
    }
}
