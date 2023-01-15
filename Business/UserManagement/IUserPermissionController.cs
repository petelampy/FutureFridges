using FutureFridges.Business.Enums;

namespace FutureFridges.Business.UserManagement
{
    public interface IUserPermissionController
    {
        void CreatePermissions (Guid user_UID, UserType userType);
        void Delete (Guid user_uid);
        IEnumerable<UserPermissions> GetAll ();
        UserPermissions GetPermissions (Guid user_UID);
        void UpdatePermissions (UserPermissions updatedUserPermissions);
    }
}