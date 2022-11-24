using FutureFridges.Business.UserManagement;

namespace FutureFridges.Data.UserManagement
{
    public interface IUserRepository
    {
        User GetUser(Guid User_UID);
    }
}