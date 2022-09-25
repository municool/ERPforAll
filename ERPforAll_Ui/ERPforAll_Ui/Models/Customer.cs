using System;
using System.Collections.Generic;

namespace ERPforAll_Ui.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Sells = new HashSet<Sell>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Sell> Sells { get; set; }
    }
}
