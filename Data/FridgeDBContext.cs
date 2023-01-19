﻿using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FutureFridges.Data
{
    public class FridgeDBContext : IdentityDbContext<FridgeUser>
    {
        public FridgeDBContext (DbContextOptions<FridgeDBContext> options) : base(options)
        { }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FridgeUser>().HasData(SampleDataGenerator.GenerateUser());
            builder.Entity<UserPermissions>().HasData(SampleDataGenerator.GenerateUserPermissions());
            builder.Entity<Product>().HasData(SampleDataGenerator.GenerateProduct());
            builder.Entity<StockItem>().HasData(SampleDataGenerator.GenerateStockItem());
            builder.Entity<Order>().HasData(SampleDataGenerator.GenerateOrder());
            builder.Entity<OrderItem>().HasData(SampleDataGenerator.GenerateOrderItem());
            builder.Entity<Supplier>().HasData(SampleDataGenerator.GenerateSupplier());
        }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }
    }
}