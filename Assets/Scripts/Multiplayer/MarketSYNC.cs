using Ekonomika.Utils;
using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;

public static class MarketSYNC
{
    public static OnlineSellItem[] Get()
    {
        List<OnlineSellItem> Items = new List<OnlineSellItem>();

        string[] items = (string[])PhotonNetwork.CurrentRoom.CustomProperties["Shop"];
        // игрок [0] | предмет [1] | количество [2] | цена [3]
        foreach (string item in items)
        {
            string[] _temp = item.Split("|");

            Items.Add(new OnlineSellItem(_temp[0], new SellItem(
                ItemFinder.FindItemByName(_temp[1]),
                int.Parse(_temp[3]),
                int.Parse(_temp[2])
            )));

            //TODO: дописать поиск по предмета.
        }

        return Items.ToArray();
    }

    public static void Sell(OnlineSellItem sellItem)
    {
        List<string> shop = ((string[])PhotonNetwork.CurrentRoom.CustomProperties["Shop"]).ToList<string>();
        shop.Add($"{sellItem.playerName}|{sellItem.item.item.name}|{sellItem.item.count}|{sellItem.item.price}");
        Hashtable _CP = new Hashtable();
        _CP["Shop"] = shop.ToArray();
        PhotonNetwork.CurrentRoom.SetCustomProperties(_CP);
    }

    public static void Buy(OnlineSellItem sellItem)
    {
        List<string> shop = ((string[])PhotonNetwork.CurrentRoom.CustomProperties["Shop"]).ToList<string>();
        string _value = $"{sellItem.playerName}|{sellItem.item.item.name}|{sellItem.item.count}|{sellItem.item.price}";
        shop.Remove(shop.FirstOrDefault(i => i == _value));
        Hashtable _CP = new Hashtable();
        _CP["Shop"] = shop.ToArray();
        PhotonNetwork.CurrentRoom.SetCustomProperties(_CP);
    }
}
