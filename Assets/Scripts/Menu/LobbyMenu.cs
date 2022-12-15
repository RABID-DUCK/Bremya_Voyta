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
    [Header("Элементы лобби")]
    [SerializeField] private GameObject Room;
    [SerializeField] private GameObject ChangeRoom;
    [SerializeField] private GameObject Loading;
    [SerializeField] private GameObject Error;
    [SerializeField] private TMP_Text ErrorText;
    [SerializeField] private Button buttonStart;

    [Header("Элемент игрка в лобби")]
    [SerializeField] private PlayerItem playerItemPrefab;
    [SerializeField] private Transform playerItemParent;

    [Header("Элемент класса выбора")]
    [SerializeField] private GameObject charactersMenu;

    [SerializeField] private CharacterItem characterItemPrefab;
    [SerializeField] private Transform characterItemParent;

    [Header("Кол-во доступности игроков в класс")]
    [Tooltip("Лесоруб")] [Range(0, 4)] public int countWoodman;
    [Tooltip("Фермер")] [Range(0, 4)] public int countFarmer;
    [Tooltip("Шахтёр")] [Range(0, 4)] public int countMiner;
    [Tooltip("Охотник")] [Range(0, 4)] public int countHunter;

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
            Name = "Лесоруб",
            Desc = "Описание лесоруба",
            Move1 = "Рубка деревьев",
            Move2 = "Сбор ягод и грибов",
            Locate1 = "Лес",
            Locate2 = "Поляна"
        });

        characterItemsList.Add(new CharacterItem
        {
            Name = "Фермер",
            Desc = "Описание фермера",
            Move1 = "Выращивание овощей",
            Move2 = "Добыча молока",
            Locate1 = "Грядка",
            Locate2 = "Коровник"
        });

        characterItemsList.Add(new CharacterItem
        {
            Name = "Шахтёр",
            Desc = "Описание шахтёра",
            Move1 = "Добыча угля",
            Move2 = "Добыча металла",
            Locate1 = "Шахта (с углём)",
            Locate2 = "Шахта (с металлом)"
        });

        characterItemsList.Add(new CharacterItem
        {
            Name = "Охотник",
            Desc = "Описание охотника",
            Move1 = "Охота",
            Move2 = "Рыбалка",
            Locate1 = "Степь",
            Locate2 = "Пруд"
        });

        UpdateCharectersList(0);
    }

    private int startInt = 0;
    private int limit = 3;

    public void UpdateCharectersList(int pos)   // создание/обновление профессий
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

    private void UpdatePlayerList()     // создание/обновление игроков
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
    public void ReloadNick(int _id)     // вызов метода обновления ника
    {
        PlayerItem _player = playerItemsList.FirstOrDefault(p => p.phId == _id);
        if (_player != null)
        {
            _player.SetNick();
        }
    }

    [PunRPC]
    public void ReloadReady(int _id)    // вызов метода обновления готовности
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
        print($"Игрок ({newPlayer}) зашёл.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        print($"Игрок ({otherPlayer}) вышел.");
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerList();
        print($"Вы ({PhotonNetwork.LocalPlayer}) создали/вошли в лобби.");
    }
}
