using System;
using System.Collections.Generic;

namespace ERPforAll.Shared.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int MaxRoom { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
