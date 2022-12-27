using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] public GameObject[] Spawns;
    private PhotonView _photonView;
    private LobbyMenu _lobbyMenu;
    [SerializeField] private PlayerItem playerItemPrefab;
    [SerializeField] private Transform playerItemParent;

    private void Start()
    {
        //foreach (KeyValuePair<int, Player> _player in PhotonNetwork.CurrentRoom.Players)
        //{
        //    Vector3 randomPositions = Spawns[Random.Range(0, Spawns.Length)].transform.localPosition;
        //    PhotonNetwork.Instantiate(player.name, randomPositions, Quaternion.identity);
        //    print(_player.Value.NickName);
        //}
        Vector3 randomPositions = Spawns[Random.Range(0, Spawns.Length)].transform.localPosition;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Hunter_1"), randomPositions, Quaternion.identity);
        //foreach (KeyValuePair<int, Player> _player in PhotonNetwork.CurrentRoom.Players)
        //{
            

        //    //PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);

        //    //_lobbyMenu.playerItemsList.Add(newPlayerItem);
        //    print("PREFAB: " + _player);
        //}
    }
}
