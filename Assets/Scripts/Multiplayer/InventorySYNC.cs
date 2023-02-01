using Ekonomika.Utils;
using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class InventorySYNC : MonoBehaviourPunCallbacks
{
    public void PutOrPickUpItem(Item item, int count)
    {
        Hashtable _CP = new Hashtable();
        List<string> tempInventory = new List<string>();
        string _item = null;
        string[] _temp = (string[])PhotonNetwork.LocalPlayer.CustomProperties["inventory"];
        if (_temp != null)
        {
            tempInventory = _temp.ToList();
            _item = tempInventory.FirstOrDefault(i => i.Split('|')[0] == item.name);
        }

        if (_item == null)
        {
            _item = $"{item.name}|0";
            tempInventory.Add(_item); 
        }

        int _id = tempInventory.IndexOf(_item);
        string[] _arr = _item.Split('|');
        _arr[1] = (Int32.Parse(_arr[1]) + count).ToString();

        tempInventory[_id] = string.Join("|", _arr);
        _CP["inventory"] = tempInventory.ToArray<string>();

        PhotonNetwork.LocalPlayer.SetCustomProperties(_CP);
    }

    public void Debugus()
    {
        string[] mass = (string[])PhotonNetwork.LocalPlayer.CustomProperties["inventory"];
        foreach (var item in mass)
        {
            print(item);
        }
    }
}
