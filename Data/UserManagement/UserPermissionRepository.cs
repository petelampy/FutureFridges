using FutureFridges.Business.Enums;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;

namespace FutureFridges.Data.UserManagement
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        //DATABASE CONNECTION VARIABLES GO UP HERE

        public UserPermissionRepository()
        { }

        public UserPermissions GetUserPermissions(Guid user_UID)
        {
            //TODO: GET USER PERMISSIONS FROM DATABASE, CONVERT TO LOCAL USER PERMISSIONS CLASS AND RETURN
            //CURRENTLY RETURNING A SAMPLE USER PERMISSIONS OBJECT

            return new UserPermissions()
            {
                AddStock = true,
                RemoveStock = true,
                ViewStock = true,
                ManageHealthAndSafetyReport = false,
                ManageUser = false
            };
        }
    }
}
