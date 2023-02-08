using Ekonomika.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MarketSellItemView : MonoBehaviour
{
    public Action<SellItem> OnApply;
    public Action OnClose;

    [SerializeField] private Image itemImage;
    [SerializeField] private Slider priceSlider;
    [SerializeField] private Slider countSlider;
    [SerializeField] private Button applyButton;
    [SerializeField] private Button closeButton;

    private InventoryConteiner currentInventoryConteiner;

    private void Start()
    {
        applyButton.onClick.AddListener(Apply);
        closeButton.onClick.AddListener(Close);
    }

    public void SetItem(InventoryConteiner inventoryConteiner)
    {

        itemImage.sprite = inventoryConteiner.Item.ItemSprite;
        countSlider.maxValue = inventoryConteiner.ItemCount;

        currentInventoryConteiner = inventoryConteiner;
    }

    public void Apply()
    {
        SellItem newSellItem = new SellItem(currentInventoryConteiner.Item, (int)priceSlider.value, (int)countSlider.value);

        OnApply?.Invoke(newSellItem);
    }

    private void Close()
    {
        OnClose?.Invoke();
    }
}
