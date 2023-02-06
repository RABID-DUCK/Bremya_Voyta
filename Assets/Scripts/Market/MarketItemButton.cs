using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketItemButton : MonoBehaviour
{
    public Action<OnlineSellItem> OnClick;

    [SerializeField] private TextMeshProUGUI playerNmaeText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI priceText;

    private OnlineSellItem buttonItem;

    private void Awake()
    {
        buyButton.onClick.AddListener(OnClickByButton);
    }

    public void Initialization(OnlineSellItem onlineSellItem)
    {
        buttonItem = onlineSellItem;
        Sprite itemSprite = onlineSellItem.item.item.ItemSprite;

        playerNmaeText.text = onlineSellItem.playerName;

        if (itemSprite)
        {
            itemIcon.sprite = itemSprite;
        }
        else
        {
            itemIcon.gameObject.SetActive(false);
        }

        priceText.text = onlineSellItem.item.price.ToString();
    }

    private void OnClickByButton()
    {
        OnClick?.Invoke(buttonItem);
    }
}
