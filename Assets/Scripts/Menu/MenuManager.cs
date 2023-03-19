using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;
using Hastable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public static MenuManager instance;
    [Header("Data")]
    [SerializeField] private GameData data;

    [Header("Menu Elements")]
    [SerializeField, Space] private GameObject menuPanel;
    [SerializeField, Space] private GameObject settingsPanel;
    [SerializeField, Space] private GameObject createRoomPanel;
    [SerializeField] private TMP_InputField roomInputField;
    [SerializeField, Space] private GameObject searchRoomPanel;
    [SerializeField] private Transform roomsListTransform;
    [SerializeField] private RoomItem roomItemPrefab;

    [Header("Lobby Elements")]
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private Button startButton;
    [SerializeField, Space] private GameObject charactersPanel;
    [SerializeField] Transform charactersListTransform;
    [SerializeField] CharacterItem characterItemPrefab;
    [SerializeField, Space] private GameObject characterInfoPanel;
    [SerializeField] private Image characterInfoImage;
    [SerializeField] private TMP_Text characterInfoText;
    [SerializeField, Space] private Transform playersListTransform;
    private List<PlayerItem> playerItemsList = new List<PlayerItem>();
    [SerializeField] private PlayerItem playerItemPrefab;

    [Header("Other Elements")]
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingBar;
    [SerializeField, Space] private GameObject errorPanel;
    [SerializeField] private TMP_Text errorText;

    // General
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        menuPanel.SetActive(true);
        ClearPlayerData();
        UpdateCharectersList(0);
        CreateRandomName(PhotonNetwork.LocalPlayer);
    }

    private void OnApplicationQuit()
    {
        ClearPlayerData();
    }

    public void Error(string message, short code = -1)
    {
        errorText.text = $"{(code != -1 ? code + " " : "")}{message}";
        errorPanel.SetActive(true);
    }

    public void OpenOrClosePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void Send_Data(string _command, params object[] _params)
    {
        photonView.RPC(_command, RpcTarget.AllBuffered, _params);
    }

    // Menu Func's
    public void OpenCreateRoomPanel()
    {
        createRoomPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void OpenSearchRoomPanel()
    {
        searchRoomPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void CloseSettingsPanel()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void CloseCreatePanel()
    {
        menuPanel.SetActive(true);
        createRoomPanel.SetActive(false);
    }

    /*public void Tutorial()
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("Tutorial");
    }*/

    public void QuitGame()
    {
        Application.Quit();
    }

    // Create Methods
    private readonly Regex regex = new Regex("^[a-zA-Zа-яА-Я0-9]*$");
    public void CreateRoom()
    {
        loadingPanel.SetActive(true);
        createRoomPanel.SetActive(false);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = (byte)data.countPlayersRoom;
        if (!string.IsNullOrEmpty(roomInputField.text))
        {
            if (regex.IsMatch(roomInputField.text))
            {
                PhotonNetwork.CreateRoom(roomInputField.text, roomOptions);
            }
            else
            {
                Error("Данное название содержит недоступные символы!\nНазвание дожно состоять из букв и/или чисел");
                createRoomPanel.SetActive(true);
            }
        }
        else
        {
            Error("Пожалуйста введите название комнаты!");
            createRoomPanel.SetActive(true);
        }
        loadingPanel.SetActive(false);
    }

    public void CheckRoomName()
    {
        if (roomInputField.text.Length > 20)
        {
            roomInputField.text = roomInputField.text.Remove(20);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CloseCreatePanel();
        Error(message, returnCode);
    }

    // Search Room Methods
    public override void OnRoomListUpdate(List<RoomInfo> roomsList)
    {
        foreach (Transform roomItem in roomsListTransform)
        {
            Destroy(roomItem.gameObject);
        }

        foreach (RoomInfo room in roomsList)
        {
            if (!room.RemovedFromList)
            {
                RoomItem roomItem = Instantiate(roomItemPrefab, roomsListTransform);
                roomItem.SetRoomInfo(room);
            }
        };
    }

    public void CloseSearchRoomPanel()
    {
        menuPanel.SetActive(true);
        searchRoomPanel.SetActive(false);
    }

    // Join Room Methods
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public override void OnJoinedRoom()
    {
        FillCharactersPlayers();

        GetCharacter();
        ToggleMasterButton();
        UpdatePlayerList();

        roomPanel.SetActive(true);
        createRoomPanel.SetActive(false);
        searchRoomPanel.SetActive(false);

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        menuPanel.SetActive(true);
        loadingPanel.SetActive(false);
        Error(message, returnCode);
    }

    // Lobby Methods
    // -> Main Methods

    public void LeaveRoom()
    {
        ClearPlayerData(PhotonNetwork.LocalPlayer);
        ClearPlayerData();

        PhotonNetwork.AutomaticallySyncScene = false;
        menuPanel.SetActive(true);
        roomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }

    public void StartGame()
    {
        PhotonNetwork.CurrentRoom.IsVisible = false;
        Send_Data("StartLoadingGame");
        PhotonNetwork.LoadLevel("CityScene");
    }

    [PunRPC]
    public void StartLoadingGame()
    {
        loadingPanel.SetActive(true);
        roomPanel.SetActive(false);
        charactersPanel.SetActive(false);
        characterInfoPanel.SetActive(false);

        loadingBar.value = 0;
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        while (PhotonNetwork.LevelLoadingProgress < 1)
        {
            loadingBar.value = PhotonNetwork.LevelLoadingProgress;
            yield return new WaitForEndOfFrame();
        }
    }

    // -> Character (Profession Change and Clear)

    public void OpenChangeCharacterPanel()
    {
        charactersPanel.SetActive(true);
        roomPanel.SetActive(false);
    }

    public void CloseChangeCharacterPanel()
    {
        roomPanel.SetActive(true);
        charactersPanel.SetActive(false);
    }

    private void ClearPlayerData(Player _player = null)
    {
        if (_player == null)
        {
            foreach (CharacterSO _itemCharacter in data.listCharacters)
            {
                foreach (var (_nick, j) in _itemCharacter.full.Select((_nick, j) => (_nick, j)))
                {
                    _itemCharacter.full[j] = "";
                }
            }
        }
        else
        {
            data.listCharacters.ForEach((characterItem) =>
            {
                foreach (var (_nick, j) in characterItem.full.Select((_nick, j) => (_nick, j)))
                {
                    if (characterItem.full[j] == _player.NickName)
                    {
                        characterItem.full[j] = "";
                    }
                }
            });
            _player.SetCustomProperties(new Hastable() { { "Profession", null } });
        }
    }

    public void GetCharacter(CharacterItem characterItem = null)
    {
        if (characterItem == null)
        {
            foreach (CharacterSO characterSO in data.listCharacters)
            {
                foreach (var (_nick, j) in characterSO.full.Select((_nick, j) => (_nick, j)))
                {
                    if (_nick == "" || _nick == null)
                    {
                        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hastable()
                        {
                            { "Profession", characterSO.nameCharacter },
                            { "Skin", j }
                        });
                        return;
                    }
                }
            };
        }
        else
        {
            CharacterSO characterSO = characterItem.characterSO;
            foreach (var (_nick, j) in characterSO.full.Select((_nick, j) => (_nick, j)))
            {
                if (characterSO.full[j] == PhotonNetwork.LocalPlayer.NickName)
                {
                    Error($"Вы уже выбрали данную профессию");
                    return;
                }
                if (characterSO.full[j] == "" || _nick == null)
                {
                    PhotonNetwork.LocalPlayer.SetCustomProperties(new Hastable()
                    {
                        { "Profession", characterSO.nameCharacter },
                        { "Skin", j }
                    });
                    CloseChangeCharacterPanel();
                    return;
                }
            }
            Error($"Данная профессия уже занята");
        }
    }

    public void OpenCharacterInfo(CharacterItem ch)
    {
        characterInfoImage.sprite = ch.characterSO.avatars[0];
        characterInfoText.text = ch.characterSO.description;
        characterInfoPanel.SetActive(true);
    }

    private int startInt = 0, limit = 3;
    public void UpdateCharectersList(int pos)   // создание/обновление профессий
    {
        if (pos == -1 && startInt <= 0 || pos == 1 && startInt >= data.listCharacters.Count - limit)
        {
            return;
        }

        startInt += pos;

        for (int i = 0; i < charactersListTransform.childCount; i++)
        {
            Destroy(charactersListTransform.GetChild(i).gameObject);
        }

        for (int i = startInt; i < startInt + limit; i++)
        {
            CharacterItem newCharacterItem = Instantiate(characterItemPrefab, charactersListTransform);
            newCharacterItem.name = data.listCharacters[i].name;
            newCharacterItem.characterSO = data.listCharacters[i];
            newCharacterItem.avatarCharacter.sprite = data.listCharacters[i].avatars[0];
            newCharacterItem.prefabCharacter = data.listCharacters[i].prefabs[0];
            foreach (var (_filled, j) in data.listCharacters[i].full.Select((_filled, j) => (_filled, j)))
            {
                if (_filled != null)
                {
                    newCharacterItem.avatarCharacter.sprite = data.listCharacters[i].avatars[j];
                    newCharacterItem.prefabCharacter = data.listCharacters[i].prefabs[j];
                    break;
                }
            }
        }
    }

    private void FillCharactersPlayers()
    {
        foreach (KeyValuePair<int, Player> _player in PhotonNetwork.CurrentRoom.Players)
        {
            if (_player.Value.CustomProperties["Profession"] != null && _player.Value != PhotonNetwork.LocalPlayer)
            {
                string _nameCh = (string)_player.Value.CustomProperties["Profession"];
                int _idCh = (int)_player.Value.CustomProperties["Skin"];
                CharacterSO _character = data.listCharacters.FirstOrDefault(c => c.nameCharacter == _nameCh);
                _character.full[_idCh] = _player.Value.NickName;
            }
        }
    }

    // -> Player
    private void UpdatePlayerList()     // создание/обновление игроков
    {
        playerItemsList?.ForEach((playerItem) =>
        {
            Destroy(playerItem.gameObject);
        });
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> _player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playersListTransform);
            newPlayerItem.SetPlayerInfo(_player.Value);

            playerItemsList.Add(newPlayerItem);

            PlayerItem _imagePlayer = playerItemsList.FirstOrDefault(p => p.phPlayer == _player.Value);
            data.listCharacters?.ForEach((_characterSO) =>
            {
                foreach (var (_nick, j) in _characterSO.full.Select((_nick, j) => (_nick, j)))
                {
                    if (_characterSO.full[j] == _player.Value.NickName)
                    {
                        _imagePlayer.imageAvatar.sprite = _characterSO.avatars[j];
                    }
                }
            });
        }
    }

    // Photon Methods
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.JoinLobby();
    }

    public void CreateRandomName(Player _player)
    {
        string _nick = _player.NickName;
        if (_nick == null || _nick == "")
        {
            _nick = $"{data.nick[Random.Range(0, data.nick.Count)] + Random.Range(0, 100)}";
            _player.NickName = _nick;
        };
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        ToggleMasterButton();
    }

    private void ToggleMasterButton()
    {
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient ? true : false);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ClearPlayerData(otherPlayer);
        UpdatePlayerList();
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hastable changedProps)
    {
        PlayerItem _player = playerItemsList.FirstOrDefault(p => p.phPlayer == targetPlayer);

        if (_player == null)
        {
            return;
        }

        if (changedProps.ContainsKey("LastNickName"))
        {
            string lastNickName = (string)changedProps["LastNickName"];
            data.listCharacters.ForEach((chatacter) =>
            {
                foreach (var (_nick, j) in chatacter.full.Select((_nick, j) => (_nick, j)))
                {
                    if (_nick == lastNickName)
                    {
                        chatacter.full[j] = targetPlayer.NickName;
                        return;
                    }
                }
            });

            _player.SetNick();
        }

        if (changedProps.ContainsKey("Profession") && changedProps.ContainsKey("Skin"))
        {
            string _nameCh = (string)changedProps["Profession"];
            int _idCh = (int)changedProps["Skin"];

            data.listCharacters.ForEach((chatacter) =>
            {
                foreach (var (_nick, j) in chatacter.full.Select((_nick, j) => (_nick, j)))
                {
                    if (_nick == targetPlayer.NickName)
                    {
                        chatacter.full[j] = "";
                    }
                }
            });

            CharacterSO _character = data.listCharacters.FirstOrDefault(c => c.nameCharacter == _nameCh);
            _character.full[_idCh] = targetPlayer.NickName;
            _player.imageAvatar.sprite = _character.avatars[_idCh];
        }

        if (changedProps.ContainsKey("Ready"))
        {
            _player.SetReady();

            if (PhotonNetwork.IsMasterClient)
            {
                startButton.interactable = true;
                foreach (PlayerItem playItem in playerItemsList)
                {
                    if ((bool)playItem.phPlayer.CustomProperties["Ready"] == false)
                    {
                        startButton.interactable = false;
                        break;
                    }
                }
            }
        }
    }
}
