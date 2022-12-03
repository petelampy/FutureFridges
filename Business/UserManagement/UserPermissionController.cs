using FutureFridges.Data.UserManagement;

namespace FutureFridges.Business.UserManagement
{
    public class UserPermissionController : IUserPermissionController
    {
        private readonly IUserPermissionRepository __UserPermissionRepository;

        public UserPermissionController ()
            : this(new UserPermissionRepository())
        { }

        internal UserPermissionController (IUserPermissionRepository userPermissionRepository)
        {
            __UserPermissionRepository = userPermissionRepository;
        }

        public IEnumerable<UserPermissions> GetAll ()
        {
            return __UserPermissionRepository.GetAll();
        }

        public UserPermissions GetPermissions (Guid user_UID)
        {
            return __UserPermissionRepository.GetUserPermissions(user_UID);
        }
    }
}
