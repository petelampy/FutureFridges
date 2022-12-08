using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FutureFridges.Data.UserManagement
{
    public class UserRepository : IUserRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly IDbContextInitialiser __DbContextInitialiser;


        public UserRepository () :
            this(new DbContextInitialiser())
        { }

        internal UserRepository (IDbContextInitialiser dbContextInitialiser)
        {
            __DbContextInitialiser = dbContextInitialiser;
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public List<FridgeUser> GetAll ()
        {
            return __DbContext.Users.ToList();
        }

        public FridgeUser GetUser (string user_UID)
        {
            return __DbContext.Users
                .AsEnumerable()
                .Where(user => user.Id == user_UID)
                .SingleOrDefault(new FridgeUser());
        }

        public void UpdateUser (FridgeUser updatedUser)
        {
            //SET THE RETURN AS A BOOL, THROW AN ERROR IF IT FAILS TO UPDATE?

            FridgeUser _CurrentUser = GetUser(updatedUser.Id);
            _CurrentUser.UserName = updatedUser.UserName;
            _CurrentUser.NormalizedUserName = updatedUser.UserName.ToUpper();
            _CurrentUser.Email = updatedUser.Email;
            _CurrentUser.NormalizedEmail = updatedUser.Email.ToUpper();
            _CurrentUser.UserType = updatedUser.UserType;

            __DbContext.SaveChanges();
        }

        public void CreateUser(FridgeUser newUser)
        {
            //SET THE RETURN AS A BOOL, THROW AN ERROR IF IT FAILS TO CREATE?

            __DbContext.Users.Add(newUser);
            __DbContext.SaveChanges();
        }
    }
}
