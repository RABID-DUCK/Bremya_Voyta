using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text playerName;
    [SerializeField] GameObject Avatar;
    [SerializeField] GameObject Change;
    [SerializeField] GameObject Nick;
    [SerializeField] GameObject Ready;
    [SerializeField] GameObject NotReady;

    List<string> nick = new List<string> { "Jazz", "Alex", "Choon", "Jenorer", "Frin", "Qwano" };

    public void SetPlayerInfo(Player _player)
    {
        _player.NickName = CreateRandomName(_player);
        playerName.text = _player.NickName;
            Change.SetActive(true);
            Nick.SetActive(true);
            Ready.SetActive(true);
            NotReady.SetActive(false);
    }

    public string CreateRandomName(Player _player)
    {
        string _nick = _player.NickName;
        if (_nick == null || _nick == "")
        {
            _nick = $"{nick[Random.Range(0, nick.Count)]} {Random.Range(0, 100)}";
        }
        return _nick;
    }


    public void ApplyLocalChanges()
    {

    }
}
