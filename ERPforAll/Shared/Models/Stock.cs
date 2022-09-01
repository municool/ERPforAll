namespace ERPforAll.Shared.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public Warehouse Warehouse { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
    }
}
