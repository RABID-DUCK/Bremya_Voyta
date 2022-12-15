public class Forest : Work
{
    protected override void WriteItemsInPlayerInventory(Character player, int itemsCount)
    {
        player.inventory.woodCount += itemsCount;
    }
}
