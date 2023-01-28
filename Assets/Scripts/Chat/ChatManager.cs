using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    ChatClient chatClient;
    [SerializeField] string userID;
    private string nickName = PhotonNetwork.LocalPlayer.NickName;
    [SerializeField] TextMeshProUGUI chatText;
    [SerializeField] TMP_InputField textMessage;
    [SerializeField] GameObject chatPanel;

    public void DebugReturn(DebugLevel level, string message)
    {
        // Debug.Log($"{level}, {message}");
    }

    public void OnChatStateChange(ChatState state)
    {
        // Debug.Log(state);
    }

    public void OnConnected()
    {
        //chatText.text += "\n Вы подключились к чату!";
        chatClient.Subscribe("G");
    }

    public void OnDisconnected()
    {
        chatClient.Unsubscribe(new string[] { "G" });
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            chatText.text += $"\n<b>[{channelName}] <color=#FFD700>{senders[i]}</color>:</b> {messages[i]}";
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            chatText.text += $"\n<align=center><b>Вы <color=green>подклюлись</color> к [{channels[i]}] чату.</b></align>";
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            chatText.text += $"\n<align=center><b>Вы <color=red>отключились</color> от [{channels[i]}] чата.</b></align>";
        }
    }

    public void OnUserSubscribed(string channel, string user)
    {
        chatText.text += $"\n<align=center><b>Пользователь {user} <color=green>подключился</color> к [{channel}] чату</b></align>";
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        chatText.text += $"\n<align=center><b>Пользователь {user} <color=red>отключлися</color> от [{channel}] чата</b></align>";
    }

    void Start()
    {
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(nickName));
    }

    void Update()
    {
        this.chatClient.Service();
    }

    public void SendButton()
    {
        if (textMessage.text != "")
        {
            chatClient.PublishMessage("G", textMessage.text);
            textMessage.text = "";
        }
    }

    public void OpenChat()
    {
        chatPanel.SetActive(chatPanel.activeSelf == true ? false : true);
    }
}
