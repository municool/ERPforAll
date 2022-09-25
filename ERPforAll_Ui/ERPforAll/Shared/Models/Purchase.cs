using System;
using System.Collections.Generic;

namespace ERPforAll.Shared.Models
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public DateTime? FullfilledDate { get; set; }
        public Guid TradeId { get; set; }
        public int ItemId { get; set; }
        public int VendorId { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual Vendor Vendor { get; set; } = null!;
    }
}
