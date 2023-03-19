using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class EndGame : MonoBehaviourPunCallbacks
{
    [SerializeField] private WorldTime worldTime;
    [SerializeField] private WorldTimeEventSender worldTimeEventSender;

    [Header("Top player properties")]
    [SerializeField] private TMP_Text nameTopPlayer;
    [SerializeField] private TMP_Text countCoinsTopPlayer;
    [SerializeField] private Image avatarTopPlayer;

    [Header("All players settings")]
    [SerializeField] private TMP_Text descriptionText;

    [SerializeField] private Button exitToMenu;

    [SerializeField] private ShowCanvasGroup showCanvasGroup;

    [SerializeField] private List<CharacterSO> characterSO;

    private Dictionary<string, int> players = new Dictionary<string, int>();
    private List<int> sortCoins = new List<int>();

    private void Start()
    {
        worldTimeEventSender.OnEndGame += FinishGame;

        exitToMenu.onClick.AddListener(LoadMenuScene);
    }

    // [ContextMenu("FinishGame")]
    private void FinishGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "EndGame", "End" } });
        }
    }

    private void GetAllPlayers()
    {
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            players.Add(player.Value.NickName, (int)player.Value.CustomProperties["coins"]);

            sortCoins.Add((int)player.Value.CustomProperties["coins"]);
        }
    }

    private void SortPlayersByCoins()
    {
        sortCoins.Sort();
    }

    private void SetTopPlayersSetting()
    {
        var ordered = players.OrderByDescending(x => x.Value);
        int st = 0;
        foreach (var item in ordered)
        {
            if (st == 0)
            {
                nameTopPlayer.text = $"Новый староста {item.Key}";
                countCoinsTopPlayer.text = $"Количество монет - {item.Value}";

                Player _player = PhotonNetwork.CurrentRoom.Players
                    .FirstOrDefault(p => p.Value.NickName == item.Key).Value;

                string profession = (string)_player.CustomProperties["Profession"];
                int skin = (int)_player.CustomProperties["Skin"];

                print(profession + " " + skin);
                avatarTopPlayer.sprite = characterSO.FirstOrDefault(ch => ch.nameCharacter == profession).avatars[skin];
            }
            else
            {
                descriptionText.text += $"{st + 1} Место - {item.Key}, количество монет - {item.Value}\r\n";
            }
            st++;
        }
    }

    private void LoadMenuScene()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
    }

    private void ShowFinishPanel()
    {
        showCanvasGroup.Show();
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("EndGame"))
        {
            worldTimeEventSender.OnEndGame -= FinishGame;

            GetAllPlayers();

            SortPlayersByCoins();

            SetTopPlayersSetting();

            ShowFinishPanel();

            worldTime.isStartTime = false;
        }
    }
}