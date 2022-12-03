using FutureFridges.Business.Enums;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace FutureFridges.Data
{
    public class FridgeDBContext : IdentityDbContext<FridgeUser>
    {
        public FridgeDBContext (DbContextOptions<FridgeDBContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FridgeUser>().HasData(SampleDataGenerator.GenerateUser());
            builder.Entity<UserPermissions>().HasData(SampleDataGenerator.GenerateUserPermissions());
            builder.Entity<Product>().HasData(SampleDataGenerator.GenerateProduct());
        }

    }
}