using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] WorldTime worldTime;

    [SerializeField] private GameObject finishPanel;
    [SerializeField] private TMP_Text descriptionText;

    public Dictionary<string, int> players = new Dictionary<string, int>();
    private List<int> sortCoins = new List<int>();

    private void Start()
    {
        worldTime = FindObjectOfType<WorldTime>();

        worldTime.OnEndGame += FinishGame;
    }

    private void FinishGame()
    {
        GetAllPlayers();

        SortPlayersByCoins();

        SetTopPlayersSetting();
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
        int counterPlayers = 1;

        for (int i = sortCoins.Count; i >= 0; i--)
        {
            foreach (var player in players)
            {
                if (sortCoins[i] == player.Value)
                {
                    descriptionText.text += $"{counterPlayers} Место - {player.Key}, количество монет - {player.Value}\r\n";

                    counterPlayers++;
                }
            }
        }
    }
}