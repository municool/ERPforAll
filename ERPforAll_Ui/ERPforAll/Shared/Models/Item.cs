using System;
using System.Collections.Generic;

namespace ERPforAll.Shared.Models
{
    public partial class Item
    {
        public Item()
        {
            Purchases = new HashSet<Purchase>();
            Sells = new HashSet<Sell>();
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double Price { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Sell> Sells { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
