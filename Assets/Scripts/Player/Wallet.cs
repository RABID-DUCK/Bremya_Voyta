using System;

public class Wallet
{
    public event Action OnCoinsChanged;

    public int CoinsCount { get; private set; }

    public void PutCoins(int count)
    {
        CoinsCount += count;
        OnCoinsChanged?.Invoke();
    }

    public void PickUpCoins(int count)
    {
        if (CoinsCount < count)
            throw new InvalidOperationException();

        CoinsCount -= count;
        OnCoinsChanged?.Invoke();
    }
}
