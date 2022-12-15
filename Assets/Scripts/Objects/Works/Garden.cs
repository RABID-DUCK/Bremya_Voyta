public class Garden : Work
{
    protected override void WriteItemsInPlayerInventory(Character player, int itemsCount)
    {
        player.inventory.carrotCount += itemsCount;
    }
}
