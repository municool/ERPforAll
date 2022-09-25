using System;
using System.Collections.Generic;

namespace ERPforAll.Shared.Models
{
    public partial class Sell
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public DateTime? FullfilledDate { get; set; }
        public Guid TradeId { get; set; }
        public int ItemId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
    }
}
