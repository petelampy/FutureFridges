using FutureFridges.Business.UserManagement;

namespace FutureFridges.Data.UserManagement
{
    public interface IUserPermissionRepository
    {
        IEnumerable<UserPermissions> GetAll ();
        UserPermissions GetUserPermissions (Guid user_UID);
    }
}