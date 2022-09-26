using System;
using System.Collections.Generic;
using ERPforAll.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ERPforAll.Server.Infrastructure
{
    public partial class ErpDBContext : DbContext
    {
        public ErpDBContext()
        {
        }

        public ErpDBContext(DbContextOptions<ErpDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<GetNotFullfilledSell> GetNotFullfilledSells { get; set; } = null!;
        public virtual DbSet<GetStock> GetStocks { get; set; } = null!;
        public virtual DbSet<GetVendor> GetVendors { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<Sell> Sells { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<GetNotFullfilledSell>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetNotFullfilledSells");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .HasColumnName("Customer_Name");

                entity.Property(e => e.FullfilmentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Fullfilment_Date");

                entity.Property(e => e.Item).HasMaxLength(50);

                entity.Property(e => e.SellId).HasColumnName("Sell_Id");
            });

            modelBuilder.Entity<GetStock>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetStocks");

                entity.Property(e => e.Item).HasMaxLength(50);

                entity.Property(e => e.ItemId).HasColumnName("Item_Id");

                entity.Property(e => e.StockAmount).HasColumnName("Stock_Amount");

                entity.Property(e => e.StockId).HasColumnName("Stock_Id");

                entity.Property(e => e.Warehouse).HasMaxLength(50);

                entity.Property(e => e.WarehouseId).HasColumnName("Warehouse_Id");
            });

            modelBuilder.Entity<GetVendor>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetVendors");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Item>(entity =>
            {

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FullfilledDate).HasColumnType("datetime");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchases__ItemI__68487DD7");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchases__Vendo__693CA210");
            });

            modelBuilder.Entity<Sell>(entity =>
            {

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FullfilledDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Sells)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sells__CustomerI__5441852A");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Sells)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sells__ItemId__534D60F1");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stocks__ItemId__6477ECF3");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stocks__Warehous__6383C8BA");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
