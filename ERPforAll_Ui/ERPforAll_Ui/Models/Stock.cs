using System;
using System.Collections.Generic;

namespace ERPforAll_Ui.Models
{
    public partial class Stock
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int WarehouseId { get; set; }
        public int ItemId { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual Warehouse Warehouse { get; set; } = null!;
    }
}
