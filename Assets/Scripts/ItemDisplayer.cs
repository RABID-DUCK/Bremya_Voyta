using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplayer : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemNameText;

    public void Initialization(Item item, int count)
    {
        SetItemName($"({count})");
        SetItemSprite(item.ItemSprite);
    }

    protected void SetItemSprite(Sprite sprite)
    {
        if (itemIcon && sprite)
        {
            itemIcon.sprite = sprite;
        }
        else
        {
            itemIcon.gameObject.SetActive(false);
        }
    }

    protected void SetItemName(string itemName)
    {
        if (itemNameText)
        {
            itemNameText.text = itemName;
        }
    }
}
