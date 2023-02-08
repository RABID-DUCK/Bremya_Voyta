using System;

[Serializable]
public class SellItem 
{
    public Item item;
    public int price;
    public int count;

    public SellItem(Item item, int price, int count)
    {
        this.item = item;
        this.price = price;
        this.count = count;
    }
}
