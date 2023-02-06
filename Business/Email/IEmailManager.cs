using System.Net.Mail;

namespace FutureFridges.Business.Email
{
    public interface IEmailManager
    {
        void SendEmail (EmailData email);
        void SendEmail (EmailData email, Attachment attachment);
    }
}