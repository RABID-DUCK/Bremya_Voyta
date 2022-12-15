public class Cowshed : Work
{
    protected override void WriteItemsInPlayerInventory(Character player, int itemsCount)
    {
        player.inventory.milkCount += itemsCount;
    }
}
