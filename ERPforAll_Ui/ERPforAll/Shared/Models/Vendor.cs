using System;
using System.Collections.Generic;

namespace ERPforAll.Shared.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
