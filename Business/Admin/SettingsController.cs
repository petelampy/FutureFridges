using FutureFridges.Data.Admin;

namespace FutureFridges.Business.Admin
{
    public class SettingsController : ISettingsController
    {
        private readonly ISettingsRepository __SettingsRepository;

        public SettingsController ()
            : this(new SettingsRepository())
        { }

        internal SettingsController (ISettingsRepository settingsRepository)
        {
            __SettingsRepository = settingsRepository;
        }

        public Settings Get ()
        {
            return __SettingsRepository.Get();
        }

        public void Update (Settings updatedSettings)
        {
            __SettingsRepository.Update(updatedSettings);
        }
    }
}
