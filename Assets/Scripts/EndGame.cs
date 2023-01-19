using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] WorldTime worldTime;

    [SerializeField] private GameObject finishPanel;
    [SerializeField] private TMP_Text descriptionText;

    private List<int> sortCoins = new List<int>();
    private List<string> sortPlayers = new List<string>();

    private void Start()
    {
        worldTime.OnEndGame += FinishGame;
    }

    private void FinishGame()
    {
        GetAllPlayers();
    }

    private void GetAllPlayers()
    {
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            //players.Add(player.Value.NickName, (int)player.Value.CustomProperties["coins"]);
        }
    }

    private void SortPlayersByCoins()
    {
        //sortCoinsList.AddRange<playersDict.Values>
    }
}

// —оздать словарь и записывать в лист имена с помощью предварительной сортировки монет от большего к меньшему. ¬ывод в панель говна.