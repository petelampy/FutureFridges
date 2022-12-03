using FutureFridges.Business.UserManagement;

namespace FutureFridges.Data.UserManagement
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly IDbContextInitialiser __DbContextInitialiser;

        public UserPermissionRepository () :
            this(new DbContextInitialiser())
        { }

        internal UserPermissionRepository (IDbContextInitialiser dbContextInitialiser)
        {
            __DbContextInitialiser = dbContextInitialiser;
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public IEnumerable<UserPermissions> GetAll ()
        {
            return __DbContext.UserPermissions.ToList();
        }

        public UserPermissions GetUserPermissions (Guid user_UID)
        {
            return __DbContext.UserPermissions
               .Where(permissions => permissions.User_UID == user_UID)
               .SingleOrDefault(new UserPermissions());
        }
    }
}
