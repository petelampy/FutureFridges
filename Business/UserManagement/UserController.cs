using FutureFridges.Business.Email;
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
        private const string NEW_ACCOUNT_EMAIL_SUBJECT = "Future Fridges - Welcome to the Fridge!";
        private const string RESET_PASSWORD_EMAIL_BODY = "Hello, {0}! Your account password has been reset"
                    + "\n\nYour new temporary password to login is: {1}"
                    + "\n\nMake sure to change it on first use!"
                    + "\n\nKind Regards,\nThe Future Fridges Development Team";
        private const string RESET_PASSWORD_EMAIL_SUBJECT = "Future Fridges - Your Account Password has been reset!";

        private readonly IEmailManager __EmailManager;
        private readonly IPasswordHasher<FridgeUser> __PasswordHasher;
        private readonly UserManager<FridgeUser> __UserManager;
        private readonly IUserPermissionController __UserPermissionController;
        private readonly IUserRepository __UserRepository;

        public UserController (UserManager<FridgeUser> userManager)
            : this(new UserRepository(), new PasswordHasher<FridgeUser>(), new EmailManager(), new UserPermissionController(), userManager)
        { }

        internal UserController (IUserRepository userRepository, IPasswordHasher<FridgeUser> passwordHasher, IEmailManager emailManager, IUserPermissionController userPermissionController, UserManager<FridgeUser> userManager)
        {
            __UserRepository = userRepository;
            __PasswordHasher = passwordHasher;
            __EmailManager = emailManager;
            __UserPermissionController = userPermissionController;
            __UserManager = userManager;
        }

        public void CreateUser (FridgeUser newUser)
        {
            int _TemporaryPassword = GenerateTemporaryPassword();

            newUser.Id = Guid.NewGuid().ToString();
            newUser.PasswordHash = __PasswordHasher.HashPassword(newUser, _TemporaryPassword.ToString());
            newUser.NormalizedUserName = newUser.UserName.ToUpper();
            newUser.NormalizedEmail = newUser.Email.ToUpper();
            newUser.SecurityStamp = Guid.NewGuid().ToString();

            EmailData _Email = new EmailData
            {
                Subject = NEW_ACCOUNT_EMAIL_SUBJECT,
                Body = string.Format(NEW_ACCOUNT_EMAIL_BODY, newUser.UserName, _TemporaryPassword.ToString()),
                Recipient = newUser.Email
            };

            __EmailManager.SendEmail(_Email);

            __UserRepository.CreateUser(newUser);
            __UserPermissionController.CreatePermissions(new Guid(newUser.Id), newUser.UserType);
        }

        public void DeleteUser (string uid)
        {
            __UserRepository.DeleteUser(uid);
            __UserPermissionController.Delete(new Guid(uid));
        }

        private int GenerateTemporaryPassword ()
        {
            return new Random().Next(100000, 999999);
        }

        public List<FridgeUser> GetAll ()
        {
            return __UserRepository.GetAll();
        }

        public FridgeUser GetUser (string user_UID)
        {
            return __UserRepository.GetUser(user_UID);
        }

        public bool IsEmailInUse (string email)
        {
            List<FridgeUser> _Users = __UserRepository.GetAll();

            return _Users.Count(user => user.Email.ToUpperInvariant() == email.ToUpperInvariant()) > 0;
        }

        public bool IsUsernameInUse (string username)
        {
            List<FridgeUser> _Users = __UserRepository.GetAll();

            return _Users.Count(user => user.UserName.ToUpperInvariant() == username.ToUpperInvariant()) > 0;
        }

        public async Task ResetPassword (string uid)
        {
            FridgeUser _User = await __UserManager.FindByIdAsync(uid);

            string _PasswordResetToken = await __UserManager.GeneratePasswordResetTokenAsync(_User);

            int _TemporaryPassword = GenerateTemporaryPassword();

            List<IPasswordValidator<FridgeUser>> _PasswordValidators = __UserManager.PasswordValidators.ToList();
            __UserManager.PasswordValidators.Clear();

            await __UserManager.ResetPasswordAsync(_User, _PasswordResetToken, _TemporaryPassword.ToString());

            _PasswordValidators.ForEach(validator => __UserManager.PasswordValidators.Add(validator));

            EmailData _Email = new EmailData
            {
                Subject = RESET_PASSWORD_EMAIL_SUBJECT,
                Body = string.Format(RESET_PASSWORD_EMAIL_BODY, _User.UserName, _TemporaryPassword.ToString()),
                Recipient = _User.Email
            };

            __EmailManager.SendEmail(_Email);
        }

        public void UpdateUser (FridgeUser updatedUser)
        {
            __UserRepository.UpdateUser(updatedUser);
        }
    }
}
