using FutureFridges.Business.UserManagement;

namespace FutureFridges.Data.UserManagement
{
    public interface IUserRepository
    {
        List<FridgeUser> GetAll ();
        FridgeUser GetUser (Guid user_UID);
    }
}