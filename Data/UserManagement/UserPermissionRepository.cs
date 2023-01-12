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
               .AsEnumerable()
               .Where(permissions => permissions.User_UID == user_UID)
               .SingleOrDefault(new UserPermissions());
        }

        public void CreatePermissions(UserPermissions userPermissions)
        {
            __DbContext.UserPermissions.Add(userPermissions);
            __DbContext.SaveChanges();
        }

        public void UpdatePermissions(UserPermissions updatedUserPermissions)
        {
            UserPermissions _UserPermissions = GetUserPermissions(updatedUserPermissions.User_UID);

            _UserPermissions.ManageUser = updatedUserPermissions.ManageUser;
            _UserPermissions.ManageProduct = updatedUserPermissions.ManageProduct;
            _UserPermissions.ManageHealthAndSafetyReport = updatedUserPermissions.ManageHealthAndSafetyReport;
            _UserPermissions.AddStock = updatedUserPermissions.AddStock;
            _UserPermissions.RemoveStock = updatedUserPermissions.RemoveStock;
            _UserPermissions.ViewStock = updatedUserPermissions.ViewStock;

            __DbContext.UserPermissions.Update(_UserPermissions);
            __DbContext.SaveChanges();
        }
    }
}
