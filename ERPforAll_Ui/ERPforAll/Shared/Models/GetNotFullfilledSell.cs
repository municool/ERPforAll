using System;
using System.Collections.Generic;

namespace ERPforAll.Shared.Models
{
    public partial class GetNotFullfilledSell
    {
        public int SellId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Item { get; set; } = null!;
        public DateTime? FullfilmentDate { get; set; }
    }
}
