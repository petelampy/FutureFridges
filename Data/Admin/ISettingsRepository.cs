using FutureFridges.Business.Admin;

namespace FutureFridges.Data.Admin
{
    public interface ISettingsRepository
    {
        Settings Get ();
        void Update (Settings updatedSettings);
    }
}