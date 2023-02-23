using Ekonomika.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryConteinerItemButton : BaseItemDisplayer
{
    public Action<InventoryConteiner> OnClick;
    
    [Header("Button elements")]
    [SerializeField] private Button selectButton;
    
    private InventoryConteiner inventoryConteiner;

    private void Awake()
    {
        selectButton.onClick.AddListener(OnClickButton);
    }

    public void Initialization(InventoryConteiner inventoryConteiner)
    {
        this.inventoryConteiner = inventoryConteiner;

        SetItemIcon(inventoryConteiner.Item.ItemSprite);
        SetItemName(inventoryConteiner.Item.ItemName);
        SetItemCount(inventoryConteiner.ItemCount);
    }

    private void OnClickButton()
    {
        OnClick.Invoke(inventoryConteiner);
    }
}
