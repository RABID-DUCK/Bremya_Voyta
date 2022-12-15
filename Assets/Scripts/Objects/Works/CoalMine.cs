public class CoalMine : Work
{
    protected override void WriteItemsInPlayerInventory(Character player, int itemsCount)
    {
        player.inventory.coalCount += itemsCount;
    }
}
