using Ekonomika.Utils;
using System;

public class MarketChest : OutlineClickableObject
{
    public Action<Inventory> OnOpenChes;

    public override void Execute()
    {
        OnOpenChes?.Invoke(Player.PlayerInventory);
    }
}
