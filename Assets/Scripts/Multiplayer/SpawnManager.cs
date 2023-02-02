using Cinemachine;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField] public GameObject[] Spawns;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Coordinator sceneCoordinator;
    private Animator animator;
    private PhotonView _photonView;
    private LobbyMenu _lobbyMenu;
    [SerializeField] private List<CharacterSO> listCharacters;
    Quaternion synchRot = Quaternion.identity;

    private string[] data;
    private string _nameCh;
    private int _idCh;

    private void Start()
    {
        Vector3 randomPositions = Spawns[Random.Range(0, Spawns.Length)].transform.position;
        data = PhotonNetwork.LocalPlayer.CustomProperties["Profession"].ToString().Split('|', 2);
        _nameCh = data[0];
        _idCh = Int32.Parse(data[1]);
        CharacterSO _character = listCharacters.FirstOrDefault(c => c.nameCharacter == _nameCh);
        string nameCharacter = _character.prefabs[_idCh].name;
        PhotonView newPlayer = PhotonNetwork.Instantiate(Path.Combine($"PhotonPrefabs/{_character.name}", $"{nameCharacter}"), randomPositions, Quaternion.identity).GetPhotonView();
        //newPlayer.transform.parent = transform;
        if (newPlayer.IsMine)
        {
            _camera.Follow = newPlayer.transform;
            sceneCoordinator.InitializationPlayer(newPlayer.GetComponent<Character>());
            newPlayer.GetComponent<Character>().PlayerWallet.PutCoins(50);
        }

        PhotonNetwork.AutomaticallySyncScene = false;
    }
}
