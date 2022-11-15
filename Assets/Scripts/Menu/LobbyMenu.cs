using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] List<PlayerItem> playerItemsList = new List<PlayerItem>();
    [SerializeField] PlayerItem playerItemPrefab;
    [SerializeField] Transform playerItemParent;
    List<string> nick = new List<string> { "Jazz", "Alex", "Choon", "Jenorer", "Frin", "Qwano" };

    private void Awake()
    {
        PhotonNetwork.NickName = CreateRandomName(PhotonNetwork.LocalPlayer);
    }

    private void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }

            playerItemsList.Add(newPlayerItem);
        }
    }

    public string CreateRandomName(Player _player)
    {
        string _nick = _player.NickName;
        if (_nick == null || _nick == "")
        {
            _nick = $"{nick[Random.Range(0, nick.Count)]} {Random.Range(0, 100)}";
        }
        return _nick;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerList();
    }
}
