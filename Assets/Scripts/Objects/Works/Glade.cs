public class Glade : Work
{
    protected override void WriteItemsInPlayerInventory(Character player, int itemsCount)
    {
        player.inventory.berriesCount += player.inventory.berriesCount += itemsCount;
    }
}
