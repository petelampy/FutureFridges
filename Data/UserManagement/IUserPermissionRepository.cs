using FutureFridges.Business.UserManagement;

namespace FutureFridges.Data.UserManagement
{
    public interface IUserPermissionRepository
    {
        UserPermissions GetUserPermissions(Guid user_UID);
    }
}