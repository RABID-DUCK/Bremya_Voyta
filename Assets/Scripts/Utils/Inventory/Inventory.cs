using System;
using System.Collections.Generic;

namespace Ekonomika.Utils
{
    public class Inventory
    {
        public event Action OnInventoryChanged;

        public List<InventoryConteiner> Conteiners { get; private set; } = new List<InventoryConteiner>();

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

            OnInventoryChanged?.Invoke();
        }

        public void PickUpItem(Item type, int count = 1)
        {
            InventoryConteiner foundConteiner = FindConteiner(type);

            if (foundConteiner == null || foundConteiner.ItemCount < count)
                throw new InvalidOperationException();

            foundConteiner.ItemCount -= count;
            
            OnInventoryChanged?.Invoke();
        }

        private InventoryConteiner FindConteiner(Item type)
        {
            return Conteiners.Find(x => { return x.Item == type; });
        }

        private void CtreateNewConteiner(Item type, int startCount)
        {
            Conteiners.Add(new InventoryConteiner(type, startCount));
        }
    }
}
