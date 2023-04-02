using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketItemButton : BaseItemDisplayer
{
    public Action<MarketLot> OnClick;

    [Header("Market elements")]
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;

    private MarketLot buttonItem;

    private void Awake()
    {
        buyButton.onClick.AddListener(OnClickByButton);
    }

    public void Initialization(MarketLot onlineSellItem)
    {
        buttonItem = onlineSellItem;

        SetItemIcon(buttonItem.sellItem.item.ItemSprite);
        SetItemName(onlineSellItem.sellItem.item.ItemName);
        SetItemCount(buttonItem.sellItem.count);

        playerName.text = onlineSellItem.playerName;
        priceText.text = onlineSellItem.sellItem.price.ToString();
    }

    private void OnClickByButton()
    {
        OnClick?.Invoke(buttonItem);
    }
}
