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
    [Tooltip("�������")] [Range(0, 4)] public int countWoodman;
    [Tooltip("������")] [Range(0, 4)] public int countFarmer;
    [Tooltip("�����")] [Range(0, 4)] public int countMiner;
    [Tooltip("�������")] [Range(0, 4)] public int countHunter;

    private List<PlayerItem> playerItemsList = new List<PlayerItem>();
    private List<CharacterItem> characterItemsList = new List<CharacterItem>();

    private void Awake()
    {
        FillCharacters();
        UpdatePlayerList();
    }

    private void FillCharacters() {

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

    public void StartGame()
    {
        SceneManager.LoadScene("Menu");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
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
