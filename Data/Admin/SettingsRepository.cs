﻿using FutureFridges.Business.Admin;

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
    }
}
