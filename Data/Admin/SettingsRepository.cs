using FutureFridges.Business.Admin;

namespace FutureFridges.Data.Admin
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly FridgeDBContext __DbContext;
        private readonly IDbContextInitialiser __DbContextInitialiser;

        public SettingsRepository () :
            this(new DbContextInitialiser())
        { }

        internal SettingsRepository (IDbContextInitialiser dbContextInitialiser)
        {
            __DbContextInitialiser = dbContextInitialiser;
            __DbContext = __DbContextInitialiser.CreateNewDbContext();
        }

        public Settings Get ()
        {
            return __DbContext.Settings
                .AsEnumerable()
                .FirstOrDefault(new Settings());
        }

        public void Update (Settings updatedSettings)
        {
            Settings _Settings = Get();

            _Settings.Administrator_UID = updatedSettings.Administrator_UID;
            _Settings.NotifyAllHeadChefs = updatedSettings.NotifyAllHeadChefs;

            __DbContext.Settings.Update(_Settings);
            __DbContext.SaveChanges();
        }

    }
}
