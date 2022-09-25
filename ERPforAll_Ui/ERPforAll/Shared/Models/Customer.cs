using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERPforAll.Shared.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Sells = new HashSet<Sell>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Sell> Sells { get; set; }
    }
}
