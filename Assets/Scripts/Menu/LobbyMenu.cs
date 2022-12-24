using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviourPunCallbacks
{
    [Header("Ёлементы лобби")]
    [SerializeField] private GameObject Room;
    [SerializeField] private GameObject ChangeRoom;
    [SerializeField] private GameObject Loading;
    [SerializeField] private GameObject Error;
    [SerializeField] private TMP_Text ErrorText;
    [SerializeField] private Button buttonStart;

    [Header("Ёлемент игрка в лобби")]
    [SerializeField] private PlayerItem playerItemPrefab;
    [SerializeField] private Transform playerItemParent;

    [Header("Ёлемент класса выбора")]
    [SerializeField] private GameObject charactersMenu;

    [SerializeField] private CharacterItem characterItemPrefab;
    [SerializeField] private Transform characterItemParent;

    [SerializeField] private List<CharacterSO> listCharacters;

    private List<PlayerItem> playerItemsList = new List<PlayerItem>();

    private void Awake()
    {
        FillCharacters();
        UpdatePlayerList();
        Room.SetActive(true);
        buttonStart.gameObject.SetActive(PhotonNetwork.IsMasterClient ? true : false);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void FillCharacters()
    {


        UpdateCharectersList(0);
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
            newCharacterItem.characterSO = listCharacters[i];
            newCharacterItem.avatarCharacter.sprite = listCharacters[i].avatars[0];
            foreach (var (_filled, j) in listCharacters[i].full.Select((_filled, j) => (_filled, j)))
            {
                if (_filled)
                {
                    continue;
                }
                else
                {
                    newCharacterItem.avatarCharacter.sprite = listCharacters[i].avatars[j];
                    break;
                }
            }
            newCharacterItem.lm = this;
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
        }
    }

    public void Send_Data(string _command, params object[] _params)
    {
        photonView.RPC(_command, RpcTarget.AllBuffered, _params);
    }

    [PunRPC]
    public void ReloadNick(int _id)     // вызов метода обновлени€ ника
    {
        PlayerItem _player = playerItemsList.FirstOrDefault(p => p.phId == _id);
        if (_player != null)
        {
            _player.SetNick();
        }
    }

    [PunRPC]
    public void ReloadReady(int _id)    // вызов метода обновлени€ готовности
    {
        PlayerItem _player = playerItemsList.FirstOrDefault(p => p.phId == _id);
        if (_player != null)
        {
            _player.SetReady();
        }

        if (PhotonNetwork.IsMasterClient)
        {
            foreach (PlayerItem playItem in playerItemsList)
            {
                print(playItem.boolReady);
                if ((bool)playItem.phPlayer.CustomProperties["Ready"] == false)
                {
                    buttonStart.interactable = false;
                    return;
                }
            }
            buttonStart.interactable = PhotonNetwork.IsMasterClient ? true : false;
        }
    }

    [PunRPC]
    public void ReloadCharacter(int _id)     // вызов метода обновлени€ ника
    {
        PlayerItem _player = playerItemsList.FirstOrDefault(p => p.phId == _id);
        if (_player != null)
        {
            _player.SetCharacter();
        }
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
        PhotonNetwork.AutomaticallySyncScene = false;
        SceneManager.LoadScene("Menu");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        buttonStart.gameObject.SetActive(PhotonNetwork.IsMasterClient ? true : false);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
        print($"»грок ({newPlayer}) зашЄл.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        print($"»грок ({otherPlayer}) вышел.");
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerList();
        print($"¬ы ({PhotonNetwork.LocalPlayer}) создали/вошли в лобби.");
    }
}
