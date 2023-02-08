public class MarketLot
{
    public int playerId;
    public string playerName;
    public SellItem sellItem;

    public MarketLot(int playerId, string playerName, SellItem sellItem)
    {
        this.playerId = playerId;
        this.playerName = playerName;
        this.sellItem = sellItem;
    }
}
