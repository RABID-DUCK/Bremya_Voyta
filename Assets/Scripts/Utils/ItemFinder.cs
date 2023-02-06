using UnityEngine;

namespace Ekonomika.Utils
{
    public static class ItemFinder
    {
        private const string ItemsFolderInResources = "Items";

        public static Item FindItemByName(string ItemName)
        {
            foreach (Object item in Resources.LoadAll(ItemsFolderInResources, typeof(Item)))
            {
                if (item.name == ItemName)
                {
                    return (Item)item;
                }
            }

            return null;
        }
    }
}
