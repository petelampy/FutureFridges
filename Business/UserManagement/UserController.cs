﻿using FutureFridges.Business.Email;
using FutureFridges.Data.UserManagement;
using Microsoft.AspNetCore.Identity;

namespace FutureFridges.Business.UserManagement
{
    public class UserController : IUserController
    {
        private const string NEW_ACCOUNT_EMAIL_BODY = "Welcome to the Fridge, {0}! We sure hope you enjoy your time inside"
                    + "\n\nYour temporary password to login is: {1}"
                    + "\n\nMake sure to change it on first use!"
                    + "\n\nKind Regards,\nThe Future Fridges Development Team";
        private const string NEW_ACCOUNT_EMAIL_SUBJECT = "Welcome to the Fridge!";

        private readonly IEmailManager __EmailManager;
        private readonly IPasswordHasher<FridgeUser> __PasswordHasher;
        private readonly IUserRepository __UserRepository;

        public UserController ()
            : this(new UserRepository(), new PasswordHasher<FridgeUser>(), new EmailManager())
        { }

        internal UserController (IUserRepository userRepository, IPasswordHasher<FridgeUser> passwordHasher, IEmailManager emailManager)
        {
            __UserRepository = userRepository;
            __PasswordHasher = passwordHasher;
            __EmailManager = emailManager;
        }

        public void CreateUser (FridgeUser newUser)
        {
            int _TemporaryPassword = new Random().Next(100000, 999999);

            newUser.PasswordHash = __PasswordHasher.HashPassword(newUser, _TemporaryPassword.ToString());


            EmailData _Email = new EmailData()
            {
                Subject = NEW_ACCOUNT_EMAIL_SUBJECT,
                Body = string.Format(NEW_ACCOUNT_EMAIL_BODY, newUser.UserName, _TemporaryPassword.ToString()),
                Recipient = newUser.Email
            };

            __EmailManager.SendEmail(_Email);

            __UserRepository.CreateUser(newUser);
        }

        public List<FridgeUser> GetAll ()
        {
            return __UserRepository.GetAll();
        }

        public FridgeUser GetUser (string user_UID)
        {
            return __UserRepository.GetUser(user_UID);
        }

        public void UpdateUser (FridgeUser updatedUser)
        {
            __UserRepository.UpdateUser(updatedUser);
        }
    }
}
