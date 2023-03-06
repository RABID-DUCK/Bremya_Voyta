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

    public void Awake()
    {
        Loading.SetActive(true);
    }

    public void CreateRandomName(Player _player)
    {
        string _nick = _player.NickName;
        if (_nick == null || _nick == "")
        {
            _nick = $"{nick[Random.Range(0, nick.Count)]+Random.Range(0, 100)}";
            _player.NickName = _nick;
        };
    }

    public override void OnConnectedToMaster()
    {
        Menu.SetActive(true);
        Loading.SetActive(false);
    }

    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Loading.SetActive(false);
    }

    public void Tutorial()
    {
        Loading.SetActive(true);
        SceneManager.LoadScene("Tutorial");
    }

    public void Play()
    {
        if (!PhotonNetwork.IsConnected) return;

        Menu.SetActive(false);
        Loading.SetActive(true);

        RoomOptions roomOptions = new RoomOptions();
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
    }

    public override void OnJoinedRoom()
    {
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
}
