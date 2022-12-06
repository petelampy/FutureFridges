namespace FutureFridges.Business.UserManagement
{
    public interface IUserPermissionController
    {
        IEnumerable<UserPermissions> GetAll ();
        UserPermissions GetPermissions (Guid user_UID);
    }
}