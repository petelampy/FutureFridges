namespace FutureFridges.Business.UserManagement
{
    public interface IUserController
    {
        List<FridgeUser> GetAll ();
        FridgeUser GetUser (Guid user_UID);
    }
}