using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;
using UnityEngine.Video;
using Unity.VisualScripting;
using UnityEditor.VersionControl;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public static MainMenu instance;

    [Header("Свойства комнаты")]
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Loading;
    [SerializeField] private GameObject Error;
    [SerializeField] private TMP_Text ErrorText;
    [SerializeField] private string RoomName = "Room";
    [SerializeField] private VideoPlayer ScreenSaver;
    [Range(0, 6), Tooltip("Кол-во человек в комнате")] public int countPlayerInRoom;

    [Header("Список рандомных ников")]
    [SerializeField, Tooltip("Список ников для игроков")] private List<string> nick = new List<string> { "Jazz", "Alex", "Choon", "Jenorer", "Frin", "Qwano" };

    [Header("Панельки")]
    [SerializeField] GameObject createRoomPanel;
    [SerializeField] GameObject listLobbyPanel;
    [SerializeField] GameObject joinGameLobby;

    [SerializeField] TMP_InputField roomInputField;
    [SerializeField] TMP_Text roomName;
     
    [SerializeField] Transform contentObject;
    [SerializeField] RoomItem roomItemPrefab;


    public void Awake()
    {
        Loading.SetActive(true);
    }

    public void Start()
    {
        instance = this;
        PhotonNetwork.ConnectUsingSettings();
    }
    public void CreateRandomName(Player _player)
    {
        string _nick = _player.NickName;
        if (_nick == null || _nick == "")
        {
            _nick = $"{nick[Random.Range(0, nick.Count)] + Random.Range(0, 100)}";
            _player.NickName = _nick;
        };
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.GameVersion = Application.version;
        Menu.SetActive(true);
        Loading.SetActive(false);
<<<<<<< Updated upstream
    }

    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Loading.SetActive(false);
=======
        PhotonNetwork.JoinLobby();
>>>>>>> Stashed changes
    }

    public void Tutorial()
    {
        Loading.SetActive(true);
        SceneManager.LoadScene("Tutorial");
    }

    public void OnClickCreate()
    {
        createRoomPanel.SetActive(false);
        joinGameLobby.SetActive(false);

        RoomOptions roomOptions = new RoomOptions();
<<<<<<< Updated upstream
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = (byte)countPlayerInRoom;

        CreateRandomName(PhotonNetwork.LocalPlayer);
        if (!PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default))
        {
            Menu.SetActive(true);
            Loading.SetActive(false);
            ErrorText.text = "Ошибка при подключении или создании лобби";
            Error.SetActive(true);
        }
=======
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = (byte)countPlayerInRoom;
        if (roomInputField.text.Length >= 1)
        {
            CreateRandomName(PhotonNetwork.LocalPlayer);
            PhotonNetwork.CreateRoom(roomInputField.text, roomOptions);
        }
    }

    public void FinRooms()
    {
        createRoomPanel.SetActive(false);
        joinGameLobby.SetActive(false);
        listLobbyPanel.SetActive(true);
>>>>>>> Stashed changes
    }

    public override void OnJoinedRoom()
    {
        createRoomPanel.SetActive(false);
        joinGameLobby.SetActive(false);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        SceneManager.LoadScene("Lobby");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Menu.SetActive(true);
        Loading.SetActive(false);
        ErrorText.text = $"{returnCode} {message}";
        Error.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < contentObject.childCount; i++)
        {
            Destroy(contentObject.GetChild(i).gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomItemPrefab, contentObject).GetComponent<RoomItem>().SetUp(roomList[i]);
        }
    }
}
