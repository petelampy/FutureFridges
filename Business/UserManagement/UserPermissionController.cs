using FutureFridges.Business.StockManagement;
using FutureFridges.Data.StockManagement;
using FutureFridges.Data.UserManagement;

namespace FutureFridges.Business.UserManagement
{
    public class UserPermissionController
    {
        private readonly IUserPermissionRepository __UserPermissionRepository;

        public UserPermissionController ()
            : this(new UserPermissionRepository())
        { }

        internal UserPermissionController (IUserPermissionRepository userPermissionRepository)
        {
            __UserPermissionRepository = userPermissionRepository;
        }

        public UserPermissions GetPermissions (Guid user_UID)
        {
            return __UserPermissionRepository.GetUserPermissions(user_UID);
        }

        public IEnumerable<UserPermissions> GetAll()
        {
            return __UserPermissionRepository.GetAll();
        }
    }
}
