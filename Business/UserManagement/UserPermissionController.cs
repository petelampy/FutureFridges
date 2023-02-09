using FutureFridges.Business.Enums;
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

        public void CreatePermissions(Guid user_UID, UserType userType)
        {
            UserPermissions _UserPermissions = new UserPermissions();
            _UserPermissions.User_UID = user_UID;

            switch (userType)
            {
                case UserType.Chef:
                    _UserPermissions.ViewStock = true;
                    _UserPermissions.RemoveStock = true;
                    _UserPermissions.AddStock = true;
                    break;
                case UserType.HeadChef:
                    _UserPermissions.ViewStock = true;
                    _UserPermissions.RemoveStock = true;
                    _UserPermissions.AddStock = true;
                    _UserPermissions.ManageProduct = true;
                    _UserPermissions.ManageHealthAndSafetyReport = true;
                    _UserPermissions.CreateOrder = true;
                    _UserPermissions.ManageSuppliers = true;
                    _UserPermissions.ManageOrders = true;
                    _UserPermissions.ViewAuditLog= true;
                    break;
                case UserType.Administrator:
                    _UserPermissions.ViewStock = true;
                    _UserPermissions.RemoveStock = true;
                    _UserPermissions.AddStock = true;
                    _UserPermissions.ManageProduct = true;
                    _UserPermissions.ManageHealthAndSafetyReport = true;
                    _UserPermissions.ManageUser = true;
                    _UserPermissions.CreateOrder = true;
                    _UserPermissions.ManageSuppliers = true;
                    _UserPermissions.ManageOrders = true;
                    _UserPermissions.ViewAuditLog = true;
                    break;
                default:
                    _UserPermissions.ViewStock = true;
                    break;
            }

            __UserPermissionRepository.CreatePermissions(_UserPermissions);
        }

        public void Delete(Guid user_uid)
        {
            __UserPermissionRepository.Delete(user_uid);
        }

        public void UpdatePermissions(UserPermissions updatedUserPermissions)
        {
            __UserPermissionRepository.UpdatePermissions(updatedUserPermissions);
        }
    }
}
