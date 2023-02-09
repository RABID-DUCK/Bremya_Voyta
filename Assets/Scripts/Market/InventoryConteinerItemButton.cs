using Ekonomika.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryConteinerItemButton : MonoBehaviour
{
    public Action<InventoryConteiner> OnClick;

    [SerializeField]
    private Image itemIcon;
    
    [SerializeField]
    private Button button;

    private InventoryConteiner inventoryConteiner;

    private void Awake()
    {
        button.onClick.AddListener(OnClickButton);
    }

    public void Initialization(InventoryConteiner inventoryConteiner)
    {
        this.inventoryConteiner = inventoryConteiner;
        
        itemIcon.sprite = inventoryConteiner.Item.ItemSprite;
    }

    private void OnClickButton()
    {
        OnClick.Invoke(inventoryConteiner);
    }
}
