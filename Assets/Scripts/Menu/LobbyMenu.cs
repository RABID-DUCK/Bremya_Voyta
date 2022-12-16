using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using System.IO;
using ExitGames.Client.Photon.StructWrapping;

public class LobbyMenu : MonoBehaviourPunCallbacks
{
    [Header("�������� �����")]
    [SerializeField] private GameObject Room;
    [SerializeField] private GameObject ChangeRoom;
    [SerializeField] private GameObject Loading;
    [SerializeField] private GameObject Error;
    [SerializeField] private TMP_Text ErrorText;
    [SerializeField] private Button buttonStart;

    [Header("������� ����� � �����")]
    [SerializeField] private PlayerItem playerItemPrefab;
    [SerializeField] private Transform playerItemParent;

    [Header("������� ������ ������")]
    [SerializeField] private GameObject charactersMenu;

    [SerializeField] private CharacterItem characterItemPrefab;
    [SerializeField] private Transform characterItemParent;

    [Header("���-�� ����������� ������� � �����")]
    [Tooltip("�������")] [Range(0, 4)] public int countLumberjack = 2;
    [SerializeField] private Sprite avatarLumberjack1;
    [SerializeField] private GameObject prefabLumberjack1;
    [SerializeField] private Sprite avatarLumberjack2;
    [SerializeField] private GameObject prefabLumberjack2;

    [Space, Tooltip("������")] [Range(0, 4)] public int countFarmer = 2;
    [SerializeField] private Sprite avatarFarmer1;
    [SerializeField] private GameObject prefabFarmer1;
    [SerializeField] private Sprite avatarFarmer2;
    [SerializeField] private GameObject prefabFarmer2;

    [Space, Tooltip("�����")] [Range(0, 4)] public int countMiner = 2;
    [SerializeField] private Sprite avatarMiner1;
    [SerializeField] private GameObject prefabMiner1;
    [SerializeField] private Sprite avatarMiner2;
    [SerializeField] private GameObject prefabMiner2;

    [Space, Tooltip("�������")] [Range(0, 4)] public int countHunter = 2;
    [SerializeField] private Sprite avatarHunter1;
    [SerializeField] private GameObject prefabHunter1;
    [SerializeField] private Sprite avatarHunter2;
    [SerializeField] private GameObject prefabHunter2;

    private List<PlayerItem> playerItemsList = new List<PlayerItem>();
    private List<CharacterItem> characterItemsList = new List<CharacterItem>();

    private void Awake()
    {
        FillCharacters();
        UpdatePlayerList();
        Room.SetActive(true);
        buttonStart.gameObject.SetActive(PhotonNetwork.IsMasterClient ? true : false);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void FillCharacters() {
        characterItemsList.Add(new CharacterItem
        {
            Name = "�������",
            Desc = "�������� ��������",
            Move1 = "����� ��������",
            Move2 = "���� ���� � ������",
            Locate1 = "���",
            Locate2 = "������",
            avatars = { avatarLumberjack1, avatarLumberjack2 },
            prefabs = { prefabLumberjack1, prefabLumberjack2 },
            full = { false, false }
        });

        characterItemsList.Add(new CharacterItem
        {
            Name = "������",
            Desc = "�������� �������",
            Move1 = "����������� ������",
            Move2 = "������ ������",
            Locate1 = "������",
            Locate2 = "��������",
            avatars = { avatarFarmer1, avatarFarmer2 },
            prefabs = { prefabFarmer1, prefabFarmer2 },
            full = { false, false }
        });

        characterItemsList.Add(new CharacterItem
        {
            Name = "�����",
            Desc = "�������� ������",
            Move1 = "������ ����",
            Move2 = "������ �������",
            Locate1 = "����� (� ����)",
            Locate2 = "����� (� ��������)",
            avatars = { avatarMiner1, avatarMiner2 },
            prefabs = { prefabMiner1, prefabMiner2 },
            full = { false, false }
        });

        characterItemsList.Add(new CharacterItem
        {
            Name = "�������",
            Desc = "�������� ��������",
            Move1 = "�����",
            Move2 = "�������",
            Locate1 = "�����",
            Locate2 = "����",
            avatars = { avatarHunter1, avatarHunter2 },
            prefabs = { prefabHunter1, prefabHunter2 },
            full = { false, false }
        });

        UpdateCharectersList(0);
    }

    private int startInt = 0;
    private int limit = 3;

    public void UpdateCharectersList(int pos)   // ��������/���������� ���������
    {
        if (pos == -1 && startInt <= 0 || pos == 1 && startInt >= characterItemsList.Count - limit)
        {
            return;
        }

        startInt += pos;

        for (int i = 0; i < characterItemParent.childCount; i++)
        {
            Destroy(characterItemParent.GetChild(i).gameObject);
        }

        for (int i = startInt; i < startInt + limit; i++)
        {
            CharacterItem newCharacterItem = Instantiate(characterItemPrefab, characterItemParent);
            newCharacterItem.name = characterItemsList[i].Name;
            newCharacterItem.Name = characterItemsList[i].Name;
            newCharacterItem.Desc = characterItemsList[i].Desc;
            newCharacterItem.Move1 = characterItemsList[i].Move1;
            newCharacterItem.Move2 = characterItemsList[i].Move2;
            newCharacterItem.Locate1 = characterItemsList[i].Locate1;
            newCharacterItem.Locate2 = characterItemsList[i].Locate2;
            newCharacterItem.countPlayers = characterItemsList[i].countPlayers;
            newCharacterItem.avatarCharacter.sprite = characterItemsList[i].avatars[0];
            newCharacterItem.lm = this;
        }
    }

    private void UpdatePlayerList()     // ��������/���������� �������
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

        foreach (KeyValuePair<int, Player> _player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.Set(OpenOrCloseCharacters, this);
            newPlayerItem.SetPlayerInfo(_player.Value);

            playerItemsList.Add(newPlayerItem);
        }
    }

    public void Send_Data(string _command, params object[] _params)
    {
        photonView.RPC(_command, RpcTarget.AllBuffered, _params);
    }

    [PunRPC]
    public void ReloadNick(int _id)     // ����� ������ ���������� ����
    {
        PlayerItem _player = playerItemsList.FirstOrDefault(p => p.phId == _id);
        if (_player != null)
        {
            _player.SetNick();
        }
    }

    [PunRPC]
    public void ReloadReady(int _id)    // ����� ������ ���������� ����������
    {
        PlayerItem _player = playerItemsList.FirstOrDefault(p => p.phId == _id);
        if (_player != null)
        {
            _player.SetReady();
        }

        if (PhotonNetwork.IsMasterClient)
        {
            foreach (PlayerItem playItem in playerItemsList)
            {
                print(playItem.boolReady);
                if ((bool)playItem.phPlayer.CustomProperties["Ready"] == false)
                {
                    buttonStart.interactable = false;
                    return;
                }
            }
            buttonStart.interactable = PhotonNetwork.IsMasterClient ? true : false;
        }
    }

    [PunRPC]
    public void ReloadCharacter(int _id)     // ����� ������ ���������� ����
    {
        PlayerItem _player = playerItemsList.FirstOrDefault(p => p.phId == _id);
        if (_player != null)
        {
            _player.SetCharacter();
        }
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("CityScene");
    }

    public void OpenOrCloseCharacters()
    {
        charactersMenu.SetActive(!charactersMenu.activeSelf);
        Room.SetActive(!Room.activeSelf);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        SceneManager.LoadScene("Menu");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        buttonStart.gameObject.SetActive(PhotonNetwork.IsMasterClient ? true : false);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
        print($"����� ({newPlayer}) �����.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        print($"����� ({otherPlayer}) �����.");
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerList();
        print($"�� ({PhotonNetwork.LocalPlayer}) �������/����� � �����.");
    }
}
