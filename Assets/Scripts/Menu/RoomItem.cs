using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMP_Text roomName;
    private RoomInfo roomInfo;

    public void SetRoomInfo(RoomInfo _roomInfo)
    {
        roomInfo = _roomInfo;
        roomName.text = roomInfo.Name;
    }

    public void OnClick()
    {
        MenuManager.instance.JoinRoom(roomInfo);
    }
}
