namespace FutureFridges.Business.Email
{
    public interface IEmailManager
    {
        void SendEmail (EmailData email);
    }
}