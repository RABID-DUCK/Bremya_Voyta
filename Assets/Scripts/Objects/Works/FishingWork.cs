public class FishingWork : Work
{
    protected override void WriteItemsInPlayerInventory(Character player, int itemsCount)
    {
        player.inventory.fishCount += itemsCount;
    }
}
