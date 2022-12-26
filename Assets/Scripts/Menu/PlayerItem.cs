using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    [Header("�������� �������")]
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

    private LobbyMenu lm;

    [HideInInspector] public Player phPlayer;
    [HideInInspector] public int phId = -1;

    [SerializeField] public bool boolReady;

    private Regex r = new Regex("^[a-zA-Z�-��-�0-9]*$");

    private ExitGames.Client.Photon.Hashtable _CP = new ExitGames.Client.Photon.Hashtable();

    public void Set(UnityAction eventClick, LobbyMenu _lm)  // ������ �����
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

        if (_player.IsLocal)
        {
            buttonChange.gameObject.SetActive(true);

            inputNick.gameObject.SetActive(true);

            buttonReady.gameObject.SetActive(true);
            buttonReadyOther.gameObject.SetActive(false);

            textButtonReady.color = Color.red;
            textButtonReady.text = "�� �����";
        }

        SetNick();
    }

    public void SetNick()
    {
        textPlayerName.text = phPlayer.NickName;
    }

    public void StartInputNick()    // ��������� ������ ���������� ����
    {
        inputNickButton.interactable = true;
        if (boolReady)
        {
            SetReadyButton();
        }
    }

    public void EndInputNick()      // ���������� ���� � ���������� ������
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
                if (r.IsMatch(inputNick.text))
                {
                    phPlayer.NickName = inputNick.text;
                }
                else
                {
                    lm.OpenError("������ ��� �������� ����������� �������!");
                }
            }
            else
            {
                lm.OpenError("������ ��� ��� ��������������!");
            }

            inputNick.text = "";
        }
    }

    public void SetReadyButton()
    {
        boolReady = !boolReady;
        textButtonReady.color = boolReady ? Color.green : Color.red;
        textButtonReady.text = boolReady ? "�����" : "�� �����";

        _CP["Ready"] = boolReady;
        phPlayer.SetCustomProperties(_CP);
    }

    public void SetReady()
    {
        boolReady = (bool)phPlayer.CustomProperties["Ready"];
        textButtonReadyOther.color = boolReady ? Color.green : Color.red;
        textButtonReadyOther.text = boolReady ? "�����" : "�� �����";
    }
}
