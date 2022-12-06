namespace FutureFridges.Data
{
    public interface IDbContextInitialiser
    {
        FridgeDBContext CreateNewDbContext ();
    }
}