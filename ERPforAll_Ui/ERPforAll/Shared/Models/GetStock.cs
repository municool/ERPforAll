using System;
using System.Collections.Generic;

namespace ERPforAll.Shared.Models
{
    public partial class GetStock
    {
        public int StockId { get; set; }
        public int StockAmount { get; set; }
        public string Item { get; set; } = null!;
        public string Warehouse { get; set; } = null!;
        public int WarehouseId { get; set; }
        public int ItemId { get; set; }
    }
}
