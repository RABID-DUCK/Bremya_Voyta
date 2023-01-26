namespace Ekonomika.Utils
{
    public class InventoryConteiner
    {
        public Item Item { get; private set; }
        public int ItemCount
        {
            get => _itemCount;

            set
            {
                _itemCount = value;
            }
        }

        private int _itemCount;

        public InventoryConteiner(Item type, int startCount = 0)
        {
            Item = type;
            _itemCount = startCount;
        }
    }
}
