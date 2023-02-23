public class InventoryItemDisplayer : BaseItemDisplayer
{
    public void Initialization(Item item, int count)
    {
        SetItemIcon(item.ItemSprite);
        SetItemName(item.ItemName);
        SetItemCount(count);
    }

    public void Initialization(Item item)
    {
        SetItemIcon(item.ItemSprite);
    }
}
