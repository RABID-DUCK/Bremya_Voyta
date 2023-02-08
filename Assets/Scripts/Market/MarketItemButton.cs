using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketItemButton : MonoBehaviour
{
    public Action<MarketLot> OnClick;

    [SerializeField] private TextMeshProUGUI playerNmaeText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI priceText;

    private MarketLot buttonItem;

    private void Awake()
    {
        buyButton.onClick.AddListener(OnClickByButton);
    }

    public void Initialization(MarketLot onlineSellItem)
    {
        buttonItem = onlineSellItem;
        Sprite itemSprite = onlineSellItem.sellItem.item.ItemSprite;

        playerNmaeText.text = onlineSellItem.playerName;

        if (itemSprite)
        {
            itemIcon.sprite = itemSprite;
        }
        else
        {
            itemIcon.gameObject.SetActive(false);
        }

        priceText.text = onlineSellItem.sellItem.price.ToString();
    }

    private void OnClickByButton()
    {
        OnClick?.Invoke(buttonItem);
    }
}
