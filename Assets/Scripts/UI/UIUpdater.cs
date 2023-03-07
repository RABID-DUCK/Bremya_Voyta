using Ekonomika.Utils;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpdater : MonoBehaviour, IObjectWithCharacter
{
    public bool Init { get; private set; } = false;

    [Header("Coins Update")]
    [SerializeField] private TextMeshProUGUI coinsCountText;

    [Header("Inventory Update")]
    [SerializeField] private InventoryItemDisplayer itemDisplayer;
    [SerializeField] private Transform inventoryContentTransform;

    private List<InventoryItemDisplayer> spawnedItemDisplayer = new List<InventoryItemDisplayer>();

    private Inventory _playerInventory;
    private Wallet _playerWallet;

    private void Start()
    {
        itemDisplayer.gameObject.SetActive(false);
    }

    public void InitializePlayer(Character player)
    {
        if (player)
        {
            _playerInventory = player.PlayerInventory;
            _playerWallet = player.PlayerWallet;
        }

        if (_playerInventory != null && _playerWallet != null)
        {
            _playerInventory.OnInventoryChanged += OnChangeInventory;
            _playerWallet.OnCoinsChanged += OnChangeCoins;

            OnChangeInventory();
            OnChangeCoins();

            Init = true;
        }
    }

    private void OnDestroy()
    {
        if (Init)
        {
            _playerInventory.OnInventoryChanged -= OnChangeInventory;
            _playerWallet.OnCoinsChanged -= OnChangeCoins;
        }
    }

    private void OnChangeCoins()
    {
        coinsCountText.text = _playerWallet.CoinsCount.ToString();
    }

    private void OnChangeInventory()
    {
        ResetSpawnedItemDisplayer();

        foreach (InventoryConteiner conteiner in _playerInventory.InventoryConteiners)
        {
            InventoryItemDisplayer createdItemDisplayer = Instantiate(itemDisplayer, inventoryContentTransform);
            createdItemDisplayer.Initialization(conteiner.Item, conteiner.ItemCount);
            createdItemDisplayer.gameObject.SetActive(true);

            spawnedItemDisplayer.Add(createdItemDisplayer);
        }
    }

    private void ResetSpawnedItemDisplayer()
    {
        foreach (InventoryItemDisplayer item in spawnedItemDisplayer)
        {
            Destroy(item.gameObject);
        }

        spawnedItemDisplayer.Clear();
    }
}
