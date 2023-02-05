using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class LobbyMenu : MonoBehaviourPunCallbacks
{
    [Header("Ёлементы лобби")]
    [SerializeField] public GameObject Room;
    [SerializeField] private GameObject ChangeRoom;
    [SerializeField] private GameObject Loading;
    [SerializeField] private Button buttonStart;
    [Space] public GameObject Error;
    public TMP_Text ErrorText;
    [Space] public GameObject characterInfoPanel;
    public Image characterInfoImage;
    public TMP_Text characterInfoText;

    [Header("Ёлемент игрка в лобби")]
    [SerializeField] private PlayerItem playerItemPrefab;
    [SerializeField] private Transform playerItemParent;

    [Header("Ёлемент класса выбора")]
    [SerializeField] public GameObject charactersMenu;

    [SerializeField] private CharacterItem characterItemPrefab;
    [SerializeField] private Transform characterItemParent;

    public List<CharacterSO> listCharacters;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();

    private ExitGames.Client.Photon.Hashtable _CP = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        UpdateCharectersList(0);

        Room.SetActive(true);
        buttonStart.gameObject.SetActive(PhotonNetwork.IsMasterClient ? true : false);

        foreach (CharacterSO _itemCharacter in listCharacters)
        {
            foreach (var (_nick, j) in _itemCharacter.full.Select((_nick, j) => (_nick, j)))
            {
                _itemCharacter.full[j] = "";
            }
        }

        foreach (KeyValuePair<int, Player> _player in PhotonNetwork.CurrentRoom.Players)
        {
            if (_player.Value.CustomProperties["Profession"] != null && _player.Value != PhotonNetwork.LocalPlayer)
            {
                string _nameCh = (string)_player.Value.CustomProperties["Profession"];
                int _idCh = (int)_player.Value.CustomProperties["Skin"];
                CharacterSO _character = listCharacters.FirstOrDefault(c => c.nameCharacter == _nameCh);
                _character.full[_idCh] = _player.Value.NickName;
            }
        }

        CreateRandomCharacter();
        UpdatePlayerList();

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void CreateRandomCharacter()
    {
        foreach (CharacterSO _itemCharacter in listCharacters)
        {
            foreach (var (_nick, j) in _itemCharacter.full.Select((_nick, j) => (_nick, j)))
            {
                if (_nick == "" || _nick == null)
                {
                    _CP["Profession"] = $"{_itemCharacter.nameCharacter}";
                    _CP["Skin"] = j;
                    PhotonNetwork.LocalPlayer.SetCustomProperties(_CP);
                    return;
                }
            }
        }
    }

    private int startInt = 0;
    private int limit = 3;

    public void UpdateCharectersList(int pos)   // создание/обновление профессий
    {
        if (pos == -1 && startInt <= 0 || pos == 1 && startInt >= listCharacters.Count - limit)
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
            newCharacterItem.lm = this;
            newCharacterItem.name = listCharacters[i].name;
            newCharacterItem.characterSO = listCharacters[i];
            newCharacterItem.avatarCharacter.sprite = listCharacters[i].avatars[0];
            newCharacterItem.prefabCharacter = listCharacters[i].prefabs[0];
            foreach (var (_filled, j) in listCharacters[i].full.Select((_filled, j) => (_filled, j)))
            {
                if (_filled != null)
                {
                    newCharacterItem.avatarCharacter.sprite = listCharacters[i].avatars[j];
                    newCharacterItem.prefabCharacter = listCharacters[i].prefabs[j];
                    break;
                }
            }
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

            PlayerItem _imagePlayer = playerItemsList.FirstOrDefault(p => p.phPlayer == _player.Value);
            foreach (CharacterSO _characterSO in listCharacters)
            {
                foreach (var (_nick, j) in _characterSO.full.Select((_nick, j) => (_nick, j)))
                {
                    if (_characterSO.full[j] == _player.Value.NickName)
                    {
                        _imagePlayer.imageAvatar.sprite = _characterSO.avatars[j];
                    }
                }
            }
        }
    }

    public void Send_Data(string _command, params object[] _params)
    {
        photonView.RPC(_command, RpcTarget.AllBuffered, _params);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("CityScene");
    }

    public void OpenOrCloseCharacters()
    {
        charactersMenu.SetActive(!charactersMenu.activeSelf);
        Room.SetActive(!Room.activeSelf);
    }

    public void LeaveRoom()
    {
        ClearPlayerData(PhotonNetwork.LocalPlayer);

        PhotonNetwork.AutomaticallySyncScene = false;
        SceneManager.LoadScene("Menu");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        PlayerItem _player = playerItemsList.FirstOrDefault(p => p.phPlayer == targetPlayer);

        if (changedProps[(byte)255] != null) // ник
        {
            _player.SetNick();
        }

        if (!string.IsNullOrEmpty((string)changedProps["Profession"]))
        {
            string _nameCh = (string)changedProps["Profession"];
            int _idCh = (int)changedProps["Skin"];

            foreach (CharacterSO _itemCharacter in listCharacters)
            {
                foreach (var (_nick, j) in _itemCharacter.full.Select((_nick, j) => (_nick, j)))
                {
                    if (_nick == targetPlayer.NickName)
                    {
                        _itemCharacter.full[j] = "";
                    }
                }
            }

            CharacterSO _character = listCharacters.FirstOrDefault(c => c.nameCharacter == _nameCh);
            _character.full[_idCh] = targetPlayer.NickName;
            _player.imageAvatar.sprite = _character.avatars[_idCh];
        }

        if (changedProps["Ready"] != null)
        {
            _player.SetReady();

            if (PhotonNetwork.IsMasterClient)
            {
                buttonStart.interactable = true;
                foreach (PlayerItem playItem in playerItemsList)
                {
                    if ((bool)playItem.phPlayer.CustomProperties["Ready"] == false)
                    {
                        buttonStart.interactable = false;
                        break;
                    }
                }
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        buttonStart.gameObject.SetActive(PhotonNetwork.IsMasterClient ? true : false);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ClearPlayerData(otherPlayer);

        UpdatePlayerList();
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerList();
    }

    public void OpenError(string text)
    {
        ErrorText.text = text;
        Error.SetActive(true);
    }

    public void ClearPlayerData(Player _player)
    {
        foreach (CharacterSO _characterSO in listCharacters)
        {
            foreach (var (_nick, j) in _characterSO.full.Select((_nick, j) => (_nick, j)))
            {
                if (_characterSO.full[j] == _player.NickName)
                {
                    _characterSO.full[j] = "";
                }
            }
        }

        _CP["Profession"] = null;
        _player.SetCustomProperties(_CP);
    }
}
