using FutureFridges.Business.Enums;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace FutureFridges.Data.UserManagement
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly DbContextInitialiser __DbContextInitialiser;

        public UserPermissionRepository ()
        {
            __DbContextInitialiser = new DbContextInitialiser();
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public UserPermissions GetUserPermissions (Guid user_UID)
        {
            return __DbContext.UserPermissions
               .Where(permissions => permissions.User_UID == user_UID)
               .SingleOrDefault(new UserPermissions());
        }

        public IEnumerable<UserPermissions> GetAll ()
        {
            return __DbContext.UserPermissions.ToList();
        }
    }
}
