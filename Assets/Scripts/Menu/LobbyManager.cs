using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject createRoomPanel;
    [SerializeField] GameObject joinGameLobby;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject findLobbyPanel;

    public void OnJoinedGameLobby()
    {
        joinGameLobby.SetActive(true);
        buttons.SetActive(false);
    }

    public void OnCreateRoom()
    {
        createRoomPanel.SetActive(true);
        joinGameLobby.SetActive(false);
    }

    public void CloseFindLobbyPanel()
    {
        findLobbyPanel.SetActive(false);
        buttons.SetActive(true);
    }
}
