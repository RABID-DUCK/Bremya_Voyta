using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : BaseItemDisplayer
{
    public Action<Item> OnClick;

    [Header("Shop elements")]
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;

    private Item buttonItem = null;

    private void Awake()
    {
        buyButton.onClick.AddListener(OnClickByButton);
    }

    public void Initialization(SellItem sellItem)
    {
        SetItemName(sellItem.item.ItemName);
        SetItemIcon(sellItem.item.ItemSprite);
        SetItemCount(sellItem.count);

        buttonItem = sellItem.item;
        priceText.text = sellItem.price.ToString();

        Fill—heck();
    }

    private void OnClickByButton()
    {
        if (buttonItem)
        {
            OnClick?.Invoke(buttonItem);
        }
    }
}
