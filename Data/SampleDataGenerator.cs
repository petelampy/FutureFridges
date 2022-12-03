using FutureFridges.Business.Enums;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Identity;

namespace FutureFridges.Data
{
    public class SampleDataGenerator
    {

        private static readonly Guid __SampleProductUID = new Guid("c0c1847b-1007-4e1e-820e-86976226c158");
        private static readonly Guid __SampleUserUID = new Guid("56fada2a-2b97-43d7-99a2-c19179a28c57");

        public static FridgeUser GenerateUser() {
            PasswordHasher<FridgeUser> _PasswordHasher = new PasswordHasher<FridgeUser>();

            var _User = new FridgeUser
            {
                Id = __SampleUserUID.ToString(),
                UserName = "Admin",
                NormalizedUserName = "Admin".ToUpper(),
                Email = "admin@fridges.com",
                NormalizedEmail = "admin@fridges.com".ToUpper(),
                EmailConfirmed = true,
                UserType = UserType.Administrator
            };

            _User.PasswordHash = _PasswordHasher.HashPassword(_User, "Admin");

            return _User;
        }

        public static Product GenerateProduct ()
        {
            return new Product
            {
                Id = 1,
                Product_UID = __SampleProductUID,
                Category = StockCategory.Dairy,
                Name = "CHEESE"
            };
        }

        public static UserPermissions GenerateUserPermissions()
        {
            return new UserPermissions()
            {
                Id = -1,
                User_UID= __SampleUserUID,
                AddStock = true,
                ManageHealthAndSafetyReport= true,
                RemoveStock= true,
                ViewStock= true
            };
        }
    }
}
