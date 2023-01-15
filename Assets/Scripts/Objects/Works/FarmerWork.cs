public class FarmerWork : Work, IWork
{
    protected override void WriteItemsInPlayerInventory(Character player, int itemsCount)
    {
        player.inventory.carrotCount += itemsCount;
    }
}
