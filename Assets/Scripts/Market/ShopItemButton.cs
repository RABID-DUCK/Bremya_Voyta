using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    public Action<Item> OnClick;

    [SerializeField] private Image itemIcon;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI priceText;

    private Item buttonItem = null;

    private void Awake()
    {
        buyButton.onClick.AddListener(OnClickByButton);
    }

    public void Initialization(SellItem sellItem)
    {
        buttonItem = sellItem.item;
        Sprite itemSprite = buttonItem.ItemSprite;

        if (itemSprite)
        {
            itemIcon.sprite = itemSprite;
        }
        else
        {
            itemIcon.gameObject.SetActive(false);
        }

        priceText.text = sellItem.price.ToString();
    }

    private void OnClickByButton()
    {
        if (buttonItem)
        {
            OnClick?.Invoke(buttonItem);
        }
    }
}
