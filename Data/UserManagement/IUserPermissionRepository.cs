using FutureFridges.Business.UserManagement;

namespace FutureFridges.Data.UserManagement
{
    public interface IUserPermissionRepository
    {
        void CreatePermissions (UserPermissions userPermissions);
        void Delete (Guid user_uid);
        IEnumerable<UserPermissions> GetAll ();
        UserPermissions GetUserPermissions (Guid user_UID);
        void UpdatePermissions (UserPermissions updatedUserPermissions);
    }
}