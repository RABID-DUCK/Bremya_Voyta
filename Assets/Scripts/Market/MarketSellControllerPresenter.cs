using Ekonomika.Utils;
using System.Collections.Generic;
using UnityEngine;

public class MarketSellControllerPresenter : MonoBehaviour
{
    [SerializeField]
    private MarketSellController marketSellController;

    [SerializeField] private InventoryConteinerItemButton inventoryConteinerItemButtonPrefab;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private MarketSellItemView marketSellItemView;

    private List<InventoryConteinerItemButton> spawnedShopItemButtons = new List<InventoryConteinerItemButton>();

    private void Start()
    {
        marketSellController.OnOpenSellMarket += ShowInventory;

        marketSellItemView.OnApply += Select;
        marketSellItemView.OnClose += marketSellController.CloseSellItem;
        
        inventoryConteinerItemButtonPrefab.gameObject.SetActive(false);
    }

    private void ShowInventory(Inventory inventory)
    {
        HideInventory();

        foreach (InventoryConteiner conteiner in inventory.InventoryConteiners)
        {
            InventoryConteinerItemButton spawnedShopItemButton = Instantiate(inventoryConteinerItemButtonPrefab, spawnTransform);
            spawnedShopItemButton.Initialization(conteiner);
            spawnedShopItemButton.OnClick += SetCurrentItem;
            spawnedShopItemButton.gameObject.SetActive(true);

            spawnedShopItemButtons.Add(spawnedShopItemButton);
        }
    }

    private void HideInventory()
    {
        foreach (InventoryConteinerItemButton button in spawnedShopItemButtons)
        {
            Destroy(button.gameObject);
        }

        spawnedShopItemButtons.Clear();
    }

    private void SetCurrentItem(InventoryConteiner inventoryConteiner)
    {
        marketSellItemView.SetItem(inventoryConteiner);
        marketSellController.OpenSellItem();
    }

    private void Select(SellItem sellItem)
    {
        marketSellController.SellItemOnTheMarket(sellItem);
        marketSellController.CloseSellItem();
    }
}
