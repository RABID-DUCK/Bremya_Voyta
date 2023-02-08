using Ekonomika.Utils;
using System;

public class MarketChest : ClickableObject
{
    public Action<Inventory> OnOpenChes;

    public override void Execute(Character player)
    {
        OnOpenChes?.Invoke(player.PlayerInventory);
    }
}
