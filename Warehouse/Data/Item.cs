namespace WarehouseApplication.Data
{
    public struct Item
    {
        public string Name { get; private set; }
        public ItemType Type { get; private set; }
        public int Amount { get; private set; }

        public Item(string name, ItemType type, int amount)
        {
            Name = name;
            Type = type;
            Amount = amount;
        }
    }

    public enum ItemType
    {
        Jidlo,
        Nabytek,
        Elektronika,
        Ostatni
    }
}
