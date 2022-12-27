public class FarmerWork : Work
{
    protected override void WriteItemsInPlayerInventory(Character player, int itemsCount)
    {
        player.inventory.carrotCount += itemsCount;
    }
}
