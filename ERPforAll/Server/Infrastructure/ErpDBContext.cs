using ERPforAll.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ERPforAll.Server.Infrastructure
{
    public class ErpDBContext : DbContext
    {
        public ErpDBContext(DbContextOptions<ErpDBContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Transaction> Buys { get; set; }
        public DbSet<Transaction> Sells { get; set; }
        public DbSet<Associate> Vendors { get; set; }
        public DbSet<Associate> Customers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }
}
