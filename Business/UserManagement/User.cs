using FutureFridges.Business.Enums;

namespace FutureFridges.Business.UserManagement
{
    public class User
    {
        public Guid User_UID { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; } = UserType.Chef;
        public UserPermissions Permissions { get; set; } = new UserPermissions();
    }
}
