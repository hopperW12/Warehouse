namespace WarehouseApplication.Data
{
    public class Warehouse
    {
        public List<Item> Items { get; set; }

        public Warehouse()
        {
            Items = new List<Item>();
        }
    }
}
