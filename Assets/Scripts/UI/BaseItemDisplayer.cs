using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItemDisplayer : MonoBehaviour
{
    [SerializeField] bool hideEmptyElements = true;
    [Header("Basic elements")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCount;

    private bool isIconFilled = false;
    private bool isItemNameFilled = false;
    private bool isitemCountFilled = false;

    protected void SetItemIcon(Sprite icon)
    {
        if (itemIcon)
        {
            itemIcon.sprite = icon;
        }

        isIconFilled = itemIcon.sprite && itemIcon;
        Fill—heck();
    }

    protected void SetItemName(string name)
    {
        if (itemName)
        {
            itemName.text = name;
        }

        isItemNameFilled = itemName;
        Fill—heck();
    }

    protected void SetItemCount(int count)
    {
        if (itemCount)
        {
            itemCount.text = count.ToString();
        }

        isitemCountFilled = itemCount;
        Fill—heck();
    }

    protected void Fill—heck()
    {
        if (hideEmptyElements)
        {
            itemIcon?.gameObject.SetActive(isIconFilled);
            itemName?.gameObject.SetActive(isItemNameFilled);
            itemCount?.gameObject.SetActive(isitemCountFilled);

            OnFill—heck();
        }
    }

    protected virtual void OnFill—heck() { }
}
