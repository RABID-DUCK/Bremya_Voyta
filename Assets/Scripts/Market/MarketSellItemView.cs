using Ekonomika.Utils;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketSellItemView : MonoBehaviour
{
    public Action<SellItem> OnApply;
    public Action OnClose;

    [SerializeField] private Image itemImage;
    
    [Header("Set value")]
    [SerializeField] private Slider countSlider;
    [SerializeField] private TextMeshProUGUI countText;
    [Space]
    [SerializeField] private Slider priceSlider;
    [SerializeField] private TextMeshProUGUI priceText;
    
    [Header("Control")]
    [SerializeField] private Button applyButton;
    [SerializeField] private Button closeButton;

    private InventoryConteiner currentInventoryConteiner;

    private void Start()
    {
        applyButton.onClick.AddListener(Apply);
        closeButton.onClick.AddListener(Close);

        countSlider.onValueChanged.AddListener(OnCountValueChanged);
        priceSlider.onValueChanged.AddListener(OnPriceValueChanged);
    }

    public void SetItem(InventoryConteiner inventoryConteiner)
    {
        priceSlider.value = 10f;
        countSlider.maxValue = inventoryConteiner.ItemCount;
        countSlider.value = (int)inventoryConteiner.ItemCount / 2;

        itemImage.sprite = inventoryConteiner.Item.ItemSprite;

        countText.text = countSlider.value.ToString();
        priceText.text = priceSlider.value.ToString();

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

    private void OnCountValueChanged(float value)
    {
        int newValue = (int)value;
        countText.text = newValue.ToString();
    }

    private void OnPriceValueChanged(float value)
    {
        int newValue = (int)value;
        priceText.text = newValue.ToString();
    }
}
