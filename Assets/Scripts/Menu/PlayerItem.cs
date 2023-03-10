using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    [Header("Элементы объекта")]
    public Image imageAvatar;
    [SerializeField] TMP_Text textPlayerName;
    [SerializeField] Button buttonChange;
    [SerializeField] TMP_InputField inputNick;
    [SerializeField] Button inputNickButton;
    [Space]
    [SerializeField] Button buttonReady;
    [SerializeField] TMP_Text textButtonReady;
    [SerializeField] Button buttonReadyOther;
    [SerializeField] TMP_Text textButtonReadyOther;

    [HideInInspector] public Player phPlayer;
    [HideInInspector] public int phId = -1;

    [SerializeField] public bool boolReady;

    private Regex regex = new Regex("^[a-zA-Zа-яА-Я0-9]*$");

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
        phPlayer.SetCustomProperties(new Hashtable() { { "Ready", false } });

        if (_player.IsLocal)
        {
            buttonChange.gameObject.SetActive(true);

            inputNick.gameObject.SetActive(true);

            buttonReady.gameObject.SetActive(true);
            buttonReadyOther.gameObject.SetActive(false);

            textButtonReady.color = Color.red;
            textButtonReady.text = "Не готов";
        }

        SetNick();
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
            bool _bool = true;
            foreach (KeyValuePair<int, Player> _player in PhotonNetwork.CurrentRoom.Players)
            {
                if (inputNick.text == _player.Value.NickName)
                {
                    _bool = false;
                    break;
                }
            }

            if (_bool)
            {
                if (regex.IsMatch(inputNick.text))
                {
                    string lastNickName = phPlayer.NickName;
                    phPlayer.NickName = inputNick.text;
                    phPlayer.SetCustomProperties(new Hashtable() { { "LastNickName", lastNickName } });
                }
                else
                {
                    MenuManager.instance.Error("Данный ник содержит недоступные символы!");
                }
            }
            else
            {
                MenuManager.instance.Error("Данный ник уже использьзуется!");
            }

            inputNick.text = "";
        }
    }

    public void ChangeCharacter()
    {
        MenuManager.instance.OpenChangeCharacterPanel();
    }

    public void SetReadyButton()
    {
        boolReady = !boolReady;
        textButtonReady.color = boolReady ? Color.green : Color.red;
        textButtonReady.text = boolReady ? "Готов" : "Не готов";

        phPlayer.SetCustomProperties(new Hashtable() { { "Ready", boolReady } });
    }

    public void SetReady()
    {
        boolReady = (bool)phPlayer.CustomProperties["Ready"];
        textButtonReadyOther.color = boolReady ? Color.green : Color.red;
        textButtonReadyOther.text = boolReady ? "Готов" : "Не готов";
    }
}
