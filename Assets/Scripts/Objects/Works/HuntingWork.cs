public class HuntingWork : Work
{
    protected override void WriteItemsInPlayerInventory(Character player, int itemsCount)
    {
        player.inventory.meatCount += itemsCount;
    }
}
