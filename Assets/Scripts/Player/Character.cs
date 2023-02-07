using Ekonomika.Utils;
using Ekonomika.Work;
using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviourPunCallbacks
{
    public Wallet PlayerWallet { get; private set; } = new Wallet();
    public Inventory PlayerInventory { get; private set; } = new Inventory();

    private Hashtable _CP = new Hashtable();

    public bool Init { get; private set; }

    private ClickEventer clickEventer;

    [SerializeField]
    private List<Item> allowedEarnedItems;

    private void OnDestroy()
    {
        if (Init)
        {
            clickEventer.OnClickWork -= GetObject;
            PlayerWallet.OnCoinsChanged -= ChangeMoney;
        }
    }

    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this);
        }
    }

    public void Initialization(ClickEventer clickEventer)
    {
        OnDestroy();
        InitPlayer(clickEventer);
        PlayerWallet.OnCoinsChanged += ChangeMoney;
    }

    private void InitPlayer(ClickEventer clickEventer)
    {
        clickEventer.OnClickWork += GetObject;
        this.clickEventer = clickEventer;

        Init = true;
    }

    private void GetObject(IWork workObject)
    {
        bool checkWork = allowedEarnedItems.Find(x => { return x == workObject.ReceivedItem; });

        if (checkWork)
        {
            workObject.Execute(this);
        }
        else
        {
            UIController.ShowOkInfo($"Вы не можете работать на данной работе! \nТребуется: <b>{workObject.WorkerName}</b>.");
        }
    }

    private void ChangeMoney()
    {
        _CP["coins"] = PlayerWallet.CoinsCount;
        PhotonNetwork.LocalPlayer.SetCustomProperties(_CP);
    }
}
