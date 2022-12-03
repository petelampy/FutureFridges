using FutureFridges.Data.UserManagement;

namespace FutureFridges.Business.UserManagement
{
    public class UserController : IUserController
    {
        private readonly IUserRepository __UserRepository;

        public UserController ()
            : this(new UserRepository())
        { }

        internal UserController (IUserRepository userRepository)
        {
            __UserRepository = userRepository;
        }

        public List<FridgeUser> GetAll ()
        {
            return __UserRepository.GetAll();
        }

        public FridgeUser GetUser (Guid user_UID)
        {
            return __UserRepository.GetUser(user_UID);
        }
    }
}
