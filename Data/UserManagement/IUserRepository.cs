using FutureFridges.Business.UserManagement;

namespace FutureFridges.Data.UserManagement
{
    public interface IUserRepository
    {
        void CreateUser (FridgeUser newUser);
        void DeleteUser (string uid);
        List<FridgeUser> GetAll ();
        FridgeUser GetUser (string user_UID);
        void UpdateUser (FridgeUser updatedUser);
    }
}