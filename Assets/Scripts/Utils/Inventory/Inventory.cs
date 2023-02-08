using System;
using System.Collections.Generic;

namespace Ekonomika.Utils
{
    public class Inventory
    {
        public event Action OnInventoryChanged;

        public InventoryConteiner[] InventoryConteiners { get => _conteiners.ToArray(); }
        
        private List<InventoryConteiner> _conteiners = new List<InventoryConteiner>();

        InventorySYNC invSYNC = new InventorySYNC();

        public void PutItem(Item type, int count = 1)
        {
            InventoryConteiner foundConteiner = FindConteiner(type);

            if (foundConteiner != null)
            {
                foundConteiner.ItemCount += count;
            }
            else
            {
                CtreateNewConteiner(type, count);
            }

            invSYNC.PutOrPickUpItem(type, count);

            OnInventoryChanged?.Invoke();
        }

        public void PickUpItem(Item type, int count = 1)
        {
            InventoryConteiner foundConteiner = FindConteiner(type);

            if (foundConteiner == null || foundConteiner.ItemCount < count)
                throw new InvalidOperationException();

            foundConteiner.ItemCount -= count;

            invSYNC.PutOrPickUpItem(type, -count);

            OnInventoryChanged?.Invoke();
        }

        private InventoryConteiner FindConteiner(Item type)
        {
            return _conteiners.Find(x => { return x.Item == type; });
        }

        private void CtreateNewConteiner(Item type, int startCount)
        {
            _conteiners.Add(new InventoryConteiner(type, startCount));
        }
    }
}
