using FutureFridges.Business.Enums;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using FutureFridges.Data.StockManagement;
using System.Linq;

namespace FutureFridges.Data.UserManagement
{
    public class UserRepository : IUserRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly IDbContextInitialiser __DbContextInitialiser;


        public UserRepository() :
            this(new DbContextInitialiser())
        {}

        internal UserRepository(IDbContextInitialiser dbContextInitialiser)
        {
            __DbContextInitialiser = dbContextInitialiser;
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public FridgeUser GetUser(Guid user_UID)
        {
            return __DbContext.Users
                .Where(user => new Guid(user.Id) == user_UID)
                .SingleOrDefault(new FridgeUser());
        }

        public List<FridgeUser> GetAll()
        {
            return __DbContext.Users.ToList();
        }
    }
}
