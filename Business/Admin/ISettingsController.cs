namespace FutureFridges.Business.Admin
{
    public interface ISettingsController
    {
        Settings Get ();
        void Update (Settings updatedSettings);
    }
}