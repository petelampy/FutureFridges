using Microsoft.EntityFrameworkCore;

namespace FutureFridges.Data
{
    public class DbContextInitialiser : IDbContextInitialiser
    {
        public FridgeDBContext CreateNewDbContext ()
        {
            WebApplicationBuilder _Builder = WebApplication.CreateBuilder();
            string? _ConnectionString = _Builder.Configuration.GetConnectionString("Default");

            DbContextOptionsBuilder<FridgeDBContext> _OptionsBuilder = new DbContextOptionsBuilder<FridgeDBContext>();
            _OptionsBuilder.UseSqlServer(_ConnectionString);

            return new FridgeDBContext(_OptionsBuilder.Options);
        }
    }
}
