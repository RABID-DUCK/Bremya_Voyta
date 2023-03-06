using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMP_Text roomName;
    private RoomInfo _info;

    public void SetUp(RoomInfo roominfo)
    {
        _info = roominfo;
        roomName.text = _info.Name;
    }

    public void OnClick()
    {
        MainMenu.instance.JoinRoom(_info);
    }
}
