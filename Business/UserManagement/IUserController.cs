namespace FutureFridges.Business.UserManagement
{
    public interface IUserController
    {
        void DeleteUser (string uid);
        List<FridgeUser> GetAll ();
        FridgeUser GetUser (string user_UID);
    }
}