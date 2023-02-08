using Ekonomika.Utils;
using Ekonomika.Work;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Character : MonoBehaviourPunCallbacks
{
    public Wallet PlayerWallet { get; private set; } = new Wallet();
    public Inventory PlayerInventory { get; private set; } = new Inventory();

    public bool Init { get; private set; }

    private ClickEventer clickEventer;

    [SerializeField]
    private List<Item> allowedEarnedItems;

    private CharacterController controller;

    public SpawnManager spawnManager;

    private void OnDestroy()
    {
        if (Init)
        {
            clickEventer.OnClickWork -= GetObject;
        }
    }

    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this);
        }

        controller = gameObject.GetComponent<CharacterController>();
    }

    public void Initialization(ClickEventer clickEventer)
    {
        OnDestroy();
        InitPlayer(clickEventer);
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

    public void Teleport(Vector3 cords, Quaternion rotation = new Quaternion())
    {
        controller.enabled = false;
        transform.position = cords;
        transform.GetChild(0).rotation = rotation;
        controller.enabled = true;
    }

    public void ReturnHome()
    {
        Transform posHouse = spawnManager.houses.FirstOrDefault(h => photonView.Owner.NickName == h.playerNick).doorway;
        Teleport(posHouse.position, posHouse.rotation);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == PhotonNetwork.LocalPlayer && changedProps.ContainsKey("coins"))
        {
            if ((int)changedProps["coins"] != PlayerWallet.CoinsCount)
            {
                PlayerWallet.SetMoney((int)changedProps["coins"]);
            }
        }
    }
}
