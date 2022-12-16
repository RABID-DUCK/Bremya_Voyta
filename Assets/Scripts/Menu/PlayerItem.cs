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

    [SerializeField] public bool boolReady;

    private ExitGames.Client.Photon.Hashtable _CP = new ExitGames.Client.Photon.Hashtable();

    public void Set(UnityAction eventClick, LobbyMenu _lm)  // создаёт связь
    {
        buttonChange.onClick.AddListener(eventClick);
        lm = _lm;
    }

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

        boolReady = false;
        _CP["Ready"] = false;
        phPlayer.SetCustomProperties(_CP);

        SetNick();
        SetCharacter();

        if (_player.IsLocal)
        {
            buttonChange.gameObject.SetActive(true);

            inputNick.gameObject.SetActive(true);

            buttonReady.gameObject.SetActive(true);
            buttonReadyOther.gameObject.SetActive(false);

            textButtonReady.color = Color.red;
            textButtonReady.text = "Не готов";
        }
    }

    public void SetNick()
    {
        textPlayerName.text = phPlayer.NickName;
    }

    public void StartInputNick()    // включение кнопки применения ника
    {
        inputNickButton.interactable = true;
        if (boolReady)
        {
            SetReadyButton();
        }
    }

    public void EndInputNick()      // применение ника и выключение кнопки
    {
        inputNickButton.interactable = false;
        if (inputNick.text != "")
        {
            phPlayer.NickName = inputNick.text;
            inputNick.text = "";
            lm.Send_Data("ReloadNick", phId);
        }
    }

    public void SetReadyButton()
    {
        boolReady = !boolReady;
        textButtonReady.color = boolReady ? Color.green : Color.red;
        textButtonReady.text = boolReady ? "Готов" : "Не готов";

        _CP["Ready"] = boolReady;
        phPlayer.SetCustomProperties(_CP);

        lm.Send_Data("ReloadReady", phId);
    }

    public void SetReady()
    {
        if (!phPlayer.IsLocal)
        {
            if (phPlayer.CustomProperties.ContainsKey("Ready"))
            {
                boolReady = (bool)phPlayer.CustomProperties["Ready"];
                textButtonReadyOther.color = boolReady ? Color.green : Color.red;
                textButtonReadyOther.text = boolReady ? "Готов" : "Не готов";
            }
        }
    }

    public void SetCharacter()
    {
        // изменение профессии
    }
}
