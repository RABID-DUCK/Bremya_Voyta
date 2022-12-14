using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField] public GameObject[] Spawns;
    private PhotonView _photonView;
    private LobbyMenu _lobbyMenu;
    [SerializeField] private List<CharacterSO> listCharacters;

    private string[] data;
    private string _nameCh;
    private int _idCh;

    private void Start()
    {
        Vector3 randomPositions = Spawns[Random.Range(0, Spawns.Length)].transform.localPosition;
        data = PhotonNetwork.LocalPlayer.CustomProperties["Profession"].ToString().Split('|', 2);
        _nameCh = data[0];
        _idCh = Int32.Parse(data[1]);

        CharacterSO _character = listCharacters.FirstOrDefault(c => c.nameCharacter == _nameCh);
        string nameCharacter = _character.prefabs[_idCh].name;
        PhotonNetwork.Instantiate(Path.Combine($"PhotonPrefabs/{_character.name}", $"{nameCharacter}"), randomPositions, Quaternion.identity);
    }
}
