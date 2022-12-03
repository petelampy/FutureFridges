using FutureFridges.Business.Enums;
using FutureFridges.Business.UserManagement;
using FutureFridges.Data.StockManagement;

namespace FutureFridges.Data.UserManagement
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserPermissionRepository __UserPermissionRepository;
        private readonly FridgeDBContext __DbContext;
        private readonly IDbContextInitialiser __DbContextInitialiser;


        public UserRepository() :
            this(new UserPermissionRepository(), new DbContextInitialiser())
        {}

        internal UserRepository(IUserPermissionRepository userPermissionRepository, IDbContextInitialiser dbContextInitialiser)
        {
            __UserPermissionRepository = userPermissionRepository;
            __DbContextInitialiser = dbContextInitialiser;
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public User GetUser(Guid user_UID)
        {
            //TODO: GET USER FROM DATABASE, CONVERT TO LOCAL USER CLASS AND RETURN, CURRENTLY RETURNING A SAMPLE USER OBJECT

            return new User()
            {
                User_UID = user_UID,
                Forename = "EXAMPLE",
                Surname = "EXAMPLE",
                Email = "example@example.com",
                UserType = UserType.Chef,
                Permissions = __UserPermissionRepository.GetUserPermissions(user_UID)
            };
        }
    }
}
