using FutureFridges.Business.Enums;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FutureFridges.Data
{
    public class FridgeDBContext : IdentityDbContext<FridgeUser>
    {
        public FridgeDBContext (DbContextOptions<FridgeDBContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<StockItem> StockItems { get; set; }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            PasswordHasher<FridgeUser> _PasswordHasher = new PasswordHasher<FridgeUser>();
            
            var _User = new FridgeUser
            {
                Id = "873d6c80-2c60-4ad6-97bd-a79e576d76c3",
                UserName = "Admin",
                NormalizedUserName = "Admin".ToUpper(),
                Email = "admin@fridges.com",
                NormalizedEmail = "admin@fridges.com".ToUpper(),
                EmailConfirmed = true,
                UserType = UserType.Administrator
            };

            _User.PasswordHash = _PasswordHasher.HashPassword(_User, "Admin");

            builder.Entity<FridgeUser>().HasData(_User);
        }

    }
}