using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CharacterItem : MonoBehaviour
{
    public Button btnChange;
    public Button btnInfo;
    public Image avatarCharacter;
    public string Name { get; set; }
    public string Desc { get; set; }
    public string Move1 { get; set; }
    public string Move2 { get; set; }
    public string Locate1 { get; set; }
    public string Locate2 { get; set; }
    public int countPlayers { get; set; }
    public List<Sprite> avatars = new List<Sprite>();
    public List<GameObject> prefabs = new List<GameObject>();
    public List<bool> full = new List<bool>();
    public LobbyMenu lm;

    private ExitGames.Client.Photon.Hashtable _CP = new ExitGames.Client.Photon.Hashtable();

    public void ChangeCharacter()
    {
        _CP["Profession"] = Name;
        PhotonNetwork.LocalPlayer.SetCustomProperties(_CP);

        lm.Send_Data("ReloadCharacter", PhotonNetwork.LocalPlayer.ActorNumber);
    }
}
