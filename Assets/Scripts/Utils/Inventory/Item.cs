using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Create Item")]
public class Item : ScriptableObject
{
    public string ItemName { get => _itemName; }
    public Sprite ItemSprite { get => _itemSprite; }

    [SerializeField]
    private string _itemName;

    [SerializeField]
    private Sprite _itemSprite;
}
