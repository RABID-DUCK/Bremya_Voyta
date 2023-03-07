using Ekonomika.Utils;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public enum CharacterType
{
    Farmer,
    Hunter,
    Lumberjack,
    Miner
}

public class Character : MonoBehaviourPunCallbacks
{
    public bool Init { get; private set; }

    public Wallet PlayerWallet { get; private set; } = new Wallet();
    public Inventory PlayerInventory { get; private set; } = new Inventory();
    public CharacterType Type { get => characterType; }

    [SerializeField]
    private CharacterType characterType;

    private CharacterController controller;

    public SpawnManager spawnManager;

    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this);
        }

        controller = gameObject.GetComponent<CharacterController>();
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

    private void SetMoney(int money)
    {
        PlayerWallet.Set(money);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (PhotonNetwork.LocalPlayer == targetPlayer && changedProps.ContainsKey("coins"))
        {
            SetMoney((int)changedProps["coins"]);
        }
    }
}
