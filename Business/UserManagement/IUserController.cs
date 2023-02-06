namespace FutureFridges.Business.UserManagement
{
    public interface IUserController
    {
        void CreateUser (FridgeUser newUser);
        void DeleteUser (string uid);
        List<FridgeUser> GetAll ();
        FridgeUser GetUser (string user_UID);
        bool IsEmailInUse (string email);
        bool IsUsernameInUse (string username);
        Task ResetPassword (string uid);
        void UpdateUser (FridgeUser updatedUser);
    }
}