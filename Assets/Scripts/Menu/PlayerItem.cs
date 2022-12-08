using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    [Header("Элементы объекта")]
    [SerializeField] Image imageAvatar;
    [SerializeField] TMP_Text textPlayerName;
    [SerializeField] Button buttonChange;
    [SerializeField] TMP_InputField inputNick;
    [SerializeField] Button inputNickButton;
    [Space]
    [SerializeField] Button buttonReady;
    [SerializeField] TMP_Text textButtonReady;
    [SerializeField] Button buttonReadyOther;
    [SerializeField] TMP_Text textButtonReadyOther;

    private LobbyMenu lm;

    [HideInInspector] public Player phPlayer;
    [HideInInspector] public int phId = -1;

    [Space]
    [SerializeField] bool boolReady;

    private ExitGames.Client.Photon.Hashtable _CP = new ExitGames.Client.Photon.Hashtable();

    public void SetPlayerInfo(Player _player)
    {
        if (phPlayer == null)
        {
            phPlayer = _player;
        }

        if (phId == -1)
        {
            phId = phPlayer.ActorNumber;
        }

        SetNick();
        SetReady(phId, 0);
        SetCharacter(this);
        if (_player.IsLocal)
        {
            buttonChange.gameObject.SetActive(true);

            inputNick.gameObject.SetActive(true);

            buttonReady.gameObject.SetActive(true);
            buttonReadyOther.gameObject.SetActive(false);
        }
    }

    public void SetNick()
    {
        textPlayerName.text = phPlayer.NickName;
    }

    public void SetCharacter(PlayerItem _player)
    {
        // изменение профессии
    }

    public void SetReady(PlayerItem _player)
    {
        if (photonView.IsMine)
        {
            
        }
        // готовность игрока
    }

    public void StartInputNick()
    {
        inputNickButton.interactable = true;
    }

    public void EndInputNick()
    {
        inputNickButton.interactable = false;
        if (inputNick.text != "")
        {
            PhotonNetwork.LocalPlayer.NickName = inputNick.text;
            inputNick.text = "";
            lm.Send_Data("ReloadNick", phId);
        }
    }

    public void SetReady(int _id, int _status)
    {
        bool _bool = _status == 0 ? false : true;
        if (_id == phId)
        {
            // локальный игрок
            textButtonReady.color = _bool ? Color.green : Color.red;
            textButtonReady.text = _bool ? "Готов" : "Не готов";
            lm.Send_Data("ReloadReady", _id, _bool);
        }
        else
        {
            // остальные игроки
            textButtonReadyOther.color = _bool ? Color.green : Color.red;
            textButtonReadyOther.text = _bool ? "Готов" : "Не готов";
        }
    }

    public void Set(UnityAction eventClick, LobbyMenu _lm)
    {
        buttonChange.onClick.AddListener(eventClick);
        lm = _lm;
    }
}
