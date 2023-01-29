using System;

public class Wallet
{
    public Action OnCoinsChanged;
    public Action OnNotEnoughCoins;

    public int CoinsCount { get; private set; }

    public void PutCoins(int count)
    {
        CoinsCount += count;
        OnCoinsChanged?.Invoke();
    }

    public void PickUpCoins(int count)
    {
        if (CoinsCount >= count)
        {
            CoinsCount -= count;
            OnCoinsChanged?.Invoke();
        }
        else
        {
            OnNotEnoughCoins?.Invoke();
        }
    }
}
