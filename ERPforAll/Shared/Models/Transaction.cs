namespace ERPforAll.Shared.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
        public Associate Customer { get; set; }
        public Guid TradeId { get; set; }
    }
}
