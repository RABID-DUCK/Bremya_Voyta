using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Loading;
    [SerializeField] GameObject Error;
    [SerializeField] TMP_Text ErrorText;

    private void Awake()
    {
        Loading.SetActive(true);
    }

    private void Start()
    {
        Menu.SetActive(true);
        Loading.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 6;

        if (PhotonNetwork.CountOfRooms <= 0)
        {
            PhotonNetwork.CreateRoom("Room", roomOptions);
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            if (PhotonNetwork.CountOfPlayersInRooms < 6)
            {
                if (PhotonNetwork.JoinRoom("Room"))
                {
                    SceneManager.LoadScene("Lobby");
                }
            }
            else
            {
                Loading.SetActive(false);
                Error.SetActive(true);
                ErrorText.text = "Лобби заполнено!";
            }
        }
    }

    public void Play()
    {
        Menu.SetActive(false);
        Loading.SetActive(true);

        if(!PhotonNetwork.ConnectUsingSettings())
        {
            Menu.SetActive(true);
            Error.SetActive(true);
            ErrorText.text = "Не удалось подключиться к серверам!";
        }

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ErrorText.text = $"{returnCode} {message}";
        Error.SetActive(true);
        Loading.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
