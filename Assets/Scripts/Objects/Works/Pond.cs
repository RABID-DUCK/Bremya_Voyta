public class Pond : Work
{
    protected override void WriteItemsInPlayerInventory(Character player, int itemsCount)
    {
        player.inventory.fishCount += itemsCount;
    }
}
