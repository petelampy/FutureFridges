namespace FutureFridges.Business.UserManagement
{
    public interface IUserController
    {
        List<FridgeUser> GetAll ();
        FridgeUser GetUser (string user_UID);
    }
}