using FutureFridges.Business.UserManagement;

namespace FutureFridges.Data.UserManagement
{
    public interface IUserRepository
    {
        User GetUser(Guid user_UID);
    }
}