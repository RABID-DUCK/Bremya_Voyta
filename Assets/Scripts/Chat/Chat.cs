using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField] TMP_InputField InputField;
    [SerializeField] GameObject Message;
    [SerializeField] GameObject Content;
    private string color = "#00A15D";

    public void SendMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, ($"<color={color}>" + PhotonNetwork.NickName + "</color>" + " : "+ InputField.text));
        InputField.text = "";
    }

    [PunRPC]
    public void GetMessage(string ReceiveMessage)
    {
        GameObject M = Instantiate(Message, Vector3.down, Quaternion.identity, Content.transform);
        M.GetComponent<Message>().MyMessage.text = ReceiveMessage;
    }
}
