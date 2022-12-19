﻿using System.Net;
using System.Net.Mail;

namespace FutureFridges.Business.Email
{
    public class EmailManager : IEmailManager
    {
        private const string HOST_DOMAIN = "webhost.dynadot.com";
        private const string SENDER_EMAIL = "futurefridges@lampard.dev";
        private const string SENDER_SMTP_PASS = "24154845"; //THIS SHOULDN'T BE HERE FOR SECURITY REASONS, BUT IS FINE FOR PURPOSES OF THIS PROJECT

        private SmtpClient CreateEmailClient ()
        {
            SmtpClient _SMTPClient = new SmtpClient()
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Host = HOST_DOMAIN,
                Port = 587,
                Credentials = new NetworkCredential(SENDER_EMAIL, SENDER_SMTP_PASS)
            };

            return _SMTPClient;
        }

        public void SendEmail (EmailData email)
        {
            MailMessage _Email = new MailMessage(
                SENDER_EMAIL,
                email.Recipient,
                email.Subject,
                email.Body);

            SmtpClient _EmailClient = CreateEmailClient();

            _EmailClient.Send(_Email);

            _Email.Dispose();
        }
    }
}