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
using ExitGames.Client.Photon.StructWrapping;
using OpenCover.Framework.Model;

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

    [Header("������ ��������� �����")]
    [SerializeField] private List<string> nick = new List<string> { "Jazz", "Alex", "Choon", "Jenorer", "Frin", "Qwano" };

    [Header("������� ������ ������")]
    [SerializeField] private GameObject charactersMenu;

    [SerializeField] private CharacterItem characterItemPrefab;
    [SerializeField] private Transform characterItemParent;

    [Header("���-�� ����������� ������� � �����")]
    [Tooltip("�������")] [Range(0, 4)] public int countWoodman;
    [Tooltip("������")] [Range(0, 4)] public int countFarmer;
    [Tooltip("�����")] [Range(0, 4)] public int countMiner;
    [Tooltip("�������")] [Range(0, 4)] public int countHunter;

    private List<PlayerItem> playerItemsList = new List<PlayerItem>();
    private List<CharacterItem> characterItemsList = new List<CharacterItem>();

    private void Awake()
    {
        PhotonNetwork.LocalPlayer.NickName = CreateRandomName(PhotonNetwork.LocalPlayer);
        FillCharacters();
        if (PhotonNetwork.IsMasterClient)
        {
            buttonStart.gameObject.SetActive(true);
            buttonStart.interactable = false;
        }
    }

    private void FillCharacters() {
        // |   ��������   |   ��������   |   �������� 1   |   �������� 2   |   ������� 1   |   ������� 2   | 

        characterItemsList.Add(new CharacterItem
        {
            Name = "�������",
            Desc = "�������� ��������",
            Move1 = "����� ��������",
            Move2 = "���� ���� � ������",
            Locate1 = "���",
            Locate2 = "������"
        });

        characterItemsList.Add(new CharacterItem
        {
            Name = "������",
            Desc = "�������� �������",
            Move1 = "����������� ������",
            Move2 = "������ ������",
            Locate1 = "������",
            Locate2 = "��������"
        });

        characterItemsList.Add(new CharacterItem
        {
            Name = "�����",
            Desc = "�������� ������",
            Move1 = "������ ����",
            Move2 = "������ �������",
            Locate1 = "����� (� ����)",
            Locate2 = "����� (� ��������)"
        });

        characterItemsList.Add(new CharacterItem
        {
            Name = "�������",
            Desc = "�������� ��������",
            Move1 = "�����",
            Move2 = "�������",
            Locate1 = "�����",
            Locate2 = "����"
        });

        UpdateCharectersList(0);
    }

    private int startInt = 0;
    private int limit = 3;

    public void UpdateCharectersList(int pos)
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
        }
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

        foreach (KeyValuePair<int, Player> _player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(_player.Value);
            newPlayerItem.Set(OpenOrCloseCharacters, this);

            Debug.Log(_player.Key);

            playerItemsList.Add(newPlayerItem);
        }
    }

    public void Send_Data(string _command, params object[] _params)
    {
        photonView.RPC(_command, RpcTarget.AllBuffered, _params);
    }

    [PunRPC]
    public void ReloadNick(int _id)
    {
        PlayerItem _player = playerItemsList.FirstOrDefault(p => p.phId == _id);
        if (_player != null)
        {
            _player.SetNick();
        }
    }

    [PunRPC]
    public void ReloadReady(int _id, int _bool)
    {
        PlayerItem _player = playerItemsList.FirstOrDefault(p => p.phId == _id);
        if (_player != null)
        {
            _player.SetReady(_id, _bool);
        }
    }

    public void OpenOrCloseCharacters()
    {
        charactersMenu.SetActive(!charactersMenu.activeSelf);
    }

    public void LeaveRoom()
    {
        SceneManager.LoadScene("Menu");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
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
        base.OnPlayerEnteredRoom(newPlayer);
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        UpdatePlayerList();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        UpdatePlayerList();
    }
}
