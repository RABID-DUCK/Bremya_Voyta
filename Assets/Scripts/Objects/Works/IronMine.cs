public class IronMine : Work
{
    protected override void WriteItemsInPlayerInventory(Character player, int itemsCount)
    {
        player.inventory.ironCount += itemsCount;
    }
}
