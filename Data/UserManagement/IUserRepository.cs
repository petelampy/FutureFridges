using FutureFridges.Business.UserManagement;

namespace FutureFridges.Data.UserManagement
{
    public interface IUserRepository
    {
        FridgeUser GetUser(Guid user_UID);
        List<FridgeUser> GetAll ();
    }
}